// MIguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Checks if an entity of same type is not escaping from another entity
/// </summary>
public class ConditionTargetNear_ColleagueNotEscaping : ICondition
{
    TargetDetector_Colleague targetDetector;

    [Header("Visualizers")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject closeTarget;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Colleague>();
    }

    /// <summary>
    ///     Check if colleague is not escaping from another entity
    /// </summary>
    public override bool Test()
    {
        target = targetDetector.target;
        closeTarget = targetDetector.veryCloseTarget;

        if (target != null || closeTarget != null)
        {
            if
            (
                (target.GetComponent<EntityStats>().isPaniquing == false)
            )
            {
                return true;
            }

            else
                return false;
        }

        else
            return false;
    }
}
