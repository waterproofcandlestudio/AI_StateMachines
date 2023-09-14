// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Entity seeking other entity when near, action logic
/// </summary>
public class ActionSeek : IAction
{
    GameObject entity_gameObject;
    TargetDetector targetDetector;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;

    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float followSpeed = 500;
    public float maxSpeed = 10f;

    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
        targetDetector = GetComponentInParent<TargetDetector>();
    }
    /// <summary>
    /// The function sets the speed to 5, and then sets the velocity of the rigidbody to the normalized
    /// vector between the target and the agent, multiplied by the speed
    /// </summary>
    public override void Act()
    {
        visualFeedback.stateImage_attacking.gameObject.SetActive(false);
        visualFeedback.stateImage_following.gameObject.SetActive(true);
        visualFeedback.stateImage_searching.gameObject.SetActive(false);
        visualFeedback.stateImage_resting.gameObject.SetActive(false);

        MoveTowards(targetDetector.target, followSpeed);
    }

    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    void MoveTowards(GameObject target, float speed)
    {
        entity_gameObject = rb.gameObject;

        if (targetDetector.target == null)
            return;

        Vector3 direction = (target.transform.position - entity_gameObject.transform.position).normalized;
        direction.y = 0;
        Vector3 acceleration = direction * speed * Time.fixedDeltaTime;

        entityStats.UseStamina();

        if (acceleration.magnitude > maxSpeed)
        {
            acceleration.Normalize();
            acceleration *= maxSpeed;
        }

        entity_gameObject.transform.LookAt(direction * rotationSpeed * Time.fixedDeltaTime);
        rb.velocity = acceleration;
    }
}
