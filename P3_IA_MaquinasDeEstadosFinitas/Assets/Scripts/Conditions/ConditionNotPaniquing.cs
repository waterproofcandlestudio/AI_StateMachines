// Miguel Rodríguez Gallego
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
///     Detects if entity is not paniquing
/// </summary>
public class ConditionNotPaniquing : ICondition
{
    EntityStats entityStats;

    void Awake()
    {
        entityStats = GetComponentInParent<EntityStats>();
    }

    /// <summary>
    ///     Tests if entity is not paniquing
    /// </summary>
    public override bool Test()
    {
        if (!entityStats.isPaniquing)
        {
            return true;
        }

        else
            return false;
    }
}
