// Miguel Rodriguez Gallego
using UnityEngine;

/// <summary>
///     Used to establish conditions to reach a target state and make a transition to it
/// </summary>
public class Transition : MonoBehaviour
{
    /// <summary>
    ///     The state this transition leads to.
    /// </summary>
    public State targetState;

    /// <summary>
    ///     The condition that triggers the transition
    /// </summary>
    public ICondition[] conditions;

    /// <summary>
    ///     Gets conditions that are in gameobject at the beginning
    /// </summary>
    void Awake()
    {
        conditions = GetComponents<ICondition>();
    }
}
