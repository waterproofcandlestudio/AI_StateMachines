// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Entity checking if has stamina >= maxStamina to be able to move, action logic
/// </summary>
public class ConditionHasStamina : ICondition
{
    EntityStats stats;

    void Awake()
    {
        stats = GetComponentInParent<EntityStats>();
    }

    /// <summary>
    ///     Test if has stamina >= maxStamina to be able to move
    /// </summary>
    public override bool Test()
    {
        if (stats.GetStamina() >= stats.maxStamina)
            return true;

        else
            return false;
    }
}
