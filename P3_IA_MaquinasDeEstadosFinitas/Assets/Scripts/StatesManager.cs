// Miguel Rodriguez Gallego
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Manages states logic, the conditions check
/// </summary>
public class StatesManager : MonoBehaviour
{
    EntityStats entityStats;
    TargetDetector targetDetector;
    TargetDetector_Colleague targetDetector_Colleague;

    [Header("Management variables (Visualization)")]
    public State activeState;
    State initialState;

    [Space]

    [Header("Main states")]
    [SerializeField] List<State> states;


    void Awake()
    {
        entityStats = GetComponentInParent<EntityStats>();
        targetDetector = GetComponentInParent<TargetDetector>();
        targetDetector_Colleague = GetComponentInParent<TargetDetector_Colleague>();

        states.Clear();

        // Fill states list
        State[] state = gameObject.GetComponentsInChildren<State>();
        states.AddRange(state);

        // Define states at the beginning
        if (initialState != null)
            activeState = initialState;
        else
            if (states[0] != null)
                activeState = states[0];
    }

    void Update()
    {
        CheckConditions();
        activeState.action.Act();
    }

    /// <summary>
    ///     Check conditions in "State" of active state & if it has to transition
    ///     [Main logic about transitioning]
    /// </summary>
    void CheckConditions()
    {
        for (int i = 0; i < activeState.transitions.Count; i++)
        {
            int positiveConditionCount = 0;

            for (int x = 0; x < activeState.transitions[i].conditions.Length; x++)
            {
                if (activeState.transitions[i].conditions[x].Test() == true)
                {
                    positiveConditionCount++;
                }
                if (positiveConditionCount == activeState.transitions[i].conditions.Length)
                {
                    activeState.isActive = false;
                    activeState = activeState.transitions[i].targetState;
                    activeState.isActive = true;
                    return;
                }
            }
        }
    }
}
