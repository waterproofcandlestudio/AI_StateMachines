// Miguel Rodriguez Gallego
using UnityEditor;
using UnityEngine;

/// <summary>
///     Entity escaping from another entity when near, action logic
/// </summary>
public class ActionEscape : IAction
{
    GameObject entity_gameObject;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;
    TargetDetector targetDetector;


    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float escapeSpeed = 500;
    public float maxSpeed = 10f;

    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
        targetDetector = GetComponentInParent<TargetDetector>();
    }
    /// <summary>
    /// The function sets the hiding variable to false, sets the velocity of the rigidbody to zero & activates visual feedback
    /// </summary>
    public override void Act()
    {
        EscapeOpositeDirection(targetDetector.target, escapeSpeed);
        entityStats.isPaniquing = false;
        entityStats.isEscaping = true;

        visualFeedback.stateImage_escaping.gameObject.SetActive(true);
        visualFeedback.stateImage_chilling.gameObject.SetActive(false);
        visualFeedback.stateImage_lookingOut.gameObject.SetActive(false);
        visualFeedback.stateImage_resting.gameObject.SetActive(false);
    }
    /// <summary>
    ///     Escape opposite direction of target logic
    /// </summary>
    void EscapeOpositeDirection(GameObject target, float speed)
    {
        entity_gameObject = rb.gameObject;

        Vector3 direction = -(target.transform.position - entity_gameObject.transform.position).normalized;
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
