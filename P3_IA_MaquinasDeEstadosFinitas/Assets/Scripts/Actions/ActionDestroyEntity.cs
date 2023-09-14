// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Destroy entity if is in attack range
/// </summary>
public class ActionDestroyEntity : IAction
{
    TargetDetector targetDetector;
    EntityStats entityStats;
    Rigidbody rb;
    VisualFeedback visualFeedback;

    void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
        rb = GetComponentInParent<Rigidbody>();
        visualFeedback = GetComponentInParent<VisualFeedback>();
        targetDetector = GetComponentInParent<TargetDetector>();
    }

    /// <summary>
    ///     Kill enemy action logic & visual feedback image change
    /// </summary>
    public override void Act()
    {
        Kill();
        rb.velocity = Vector3.zero;

        visualFeedback.stateImage_attacking.gameObject.SetActive(true);
        visualFeedback.stateImage_following.gameObject.SetActive(false);
        visualFeedback.stateImage_searching.gameObject.SetActive(false);
        visualFeedback.stateImage_resting.gameObject.SetActive(false);
    }

    public void Kill()
    {
        if (targetDetector.target == null) return;

        targetDetector.target.SetActive(false);
    }
}
