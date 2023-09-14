// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Entity action when paniquing (when other entity of same type is near escaping)
/// </summary>
public class ActionPanic : IAction
{
    GameObject entity_gameObject;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;
    TargetDetector_Colleague targetDetector;

    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float escapeSpeed = 500;
    public float maxSpeed = 10f;

    Vector3 targetLastPosition;

    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
        targetDetector = GetComponentInParent<TargetDetector_Colleague>();
    }
    /// <summary>
    /// The function sets the hiding variable to false, sets the velocity of the rigidbody to zero & activates visual feedback
    /// </summary>
    public override void Act()
    {
        if (targetDetector.target == null)
            entityStats.isPaniquing = false;

        EscapeOpositeDirection(targetDetector.target, escapeSpeed);

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

        entityStats.isPaniquing = true;
        entityStats.isEscaping = false;


        if (target != null)
            targetLastPosition = target.transform.position;

        Vector3 direction = -(targetLastPosition - entity_gameObject.transform.position).normalized;
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
