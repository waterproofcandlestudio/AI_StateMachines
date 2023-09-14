// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Logic of prey when has a hunter in vision range, that makes him stop and wait for him to leave
/// </summary>
public class ActionLookOut : IAction
{
    GameObject entity_gameObject;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;
    TargetDetector targetDetector;

    [Header("Movement values")]
    public float rotationSpeed = 500;

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
        entityStats.isPaniquing = false;
        entityStats.isEscaping = false;

        entity_gameObject = rb.gameObject;

        if (targetDetector.target == null)
            return;

        Vector3 direction = -(targetDetector.target.transform.position - entity_gameObject.transform.position).normalized;
        direction.y = 0;
        entity_gameObject.transform.LookAt(direction * rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        visualFeedback.stateImage_escaping.gameObject.SetActive(false);
        visualFeedback.stateImage_chilling.gameObject.SetActive(false);
        visualFeedback.stateImage_lookingOut.gameObject.SetActive(true);
        visualFeedback.stateImage_resting.gameObject.SetActive(false);
    }
}
