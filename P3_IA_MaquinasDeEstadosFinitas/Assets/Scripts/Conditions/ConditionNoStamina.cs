using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNoStamina : ICondition
{
    EntityStats stats;

    void Awake()
    {
        stats = GetComponentInParent<EntityStats>();
    }

    /// <summary>
    ///     Test if entity has no stamina
    /// </summary>
    public override bool Test()
    {
        if (stats.GetStamina() <= 0)
            return true;

        else
            return false;
    }
}
