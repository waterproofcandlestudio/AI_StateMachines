// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Checks if an entity of same type is escaping from another entity
/// </summary>
public class ConditionTargetNear_ColleagueEscaping : ICondition
{
    TargetDetector_Colleague targetDetector;

    //[Header("Functionality")]
    //[SerializeField] State escapingState;

    [Header("Visualizers")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject closeTarget;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Colleague>();
    }

    /// <summary>
    ///     Check if colleague is escaping from another entity
    /// </summary>
    public override bool Test()
    {
        target = targetDetector.target;
        closeTarget = targetDetector.veryCloseTarget;

        if ((target != null && closeTarget != null) || closeTarget != null)
        {
            if 
            (
                //(target.GetComponent<EntityStats>().isPaniquing == true) || 
                //(closeTarget.GetComponent<EntityStats>().isPaniquing == true)
                (target.GetComponent<EntityStats>().isEscaping == true) ||
                (closeTarget.GetComponent<EntityStats>().isEscaping == true)
            )
            {
                targetDetector.gameObject.GetComponent<EntityStats>().isPaniquing = true;
                return true;
            }

            else
                return false;
        }

        else
            return false;
    }
}
