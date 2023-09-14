// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Entity resting when stamina <= 0, action logic
/// </summary>
public class ActionRest : IAction
{
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;

    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
    }
    /// <summary>
    /// The function sets the hiding variable to false, sets the velocity of the rigidbody to zero & activates visual feedback
    /// </summary>
    public override void Act()
    {
        if (entityStats.gameObject.tag == "Hunter")
        {
            visualFeedback.stateImage_attacking.gameObject.SetActive(false);
            visualFeedback.stateImage_following.gameObject.SetActive(false);
            visualFeedback.stateImage_searching.gameObject.SetActive(false);
            visualFeedback.stateImage_resting.gameObject.SetActive(true);
        }
        if (entityStats.gameObject.tag == "Prey")
        {
            entityStats.isPaniquing = false;
            entityStats.isEscaping = false;

            visualFeedback.stateImage_escaping.gameObject.SetActive(false);
            visualFeedback.stateImage_chilling.gameObject.SetActive(false);
            visualFeedback.stateImage_lookingOut.gameObject.SetActive(false);
            visualFeedback.stateImage_resting.gameObject.SetActive(true);
        }

        rb.velocity = Vector3.zero;
        entityStats.RegenStamina();
    }
}
