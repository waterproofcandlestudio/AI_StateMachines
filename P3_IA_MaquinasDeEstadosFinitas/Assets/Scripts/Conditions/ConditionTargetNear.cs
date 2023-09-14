// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
/// Checks if this transform is within "range" distance from "target"
/// </summary>
public class ConditionTargetNear : ICondition
{
    TargetDetector targetDetector;

    [SerializeField] GameObject target;
    [SerializeField] GameObject closeTarget;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector>();
    }

    /// <summary>
    ///     Tests if target is visible / near
    /// </summary>
    public override bool Test()
    {
        target = targetDetector.target;
        closeTarget = targetDetector.veryCloseTarget;

        if (target != null || closeTarget != null)
        {
            return true;
        }

        else
            return false;
    }
}
