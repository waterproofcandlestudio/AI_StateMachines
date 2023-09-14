// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Checks if target is very close from entity
/// </summary>
public class ConditionTargetVeryClose : ICondition
{
    TargetDetector targetDetector;

    [SerializeField] GameObject target;
    [SerializeField] GameObject closeTarget;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector>();
    }

    /// <summary>
    ///     Test if target is close
    /// </summary>
    public override bool Test()
    {
        target = targetDetector.veryCloseTarget;
        closeTarget = targetDetector.veryCloseTarget;

        if (closeTarget != null && target != null)
            return true;

        else
            return false;
    }
}
