// Miguel Rodriguez Gallego
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Visual feedback used to see the entity state visually
/// </summary>
public class VisualFeedback : MonoBehaviour
{
    [Header("General - Visual state feedback")]
    public Image stateImage_resting;

    [Header("Hunter - Visual state feedback")]
    public Image stateImage_attacking;
    public Image stateImage_following;
    public Image stateImage_searching;

    [Header("Prey - Visual state feedback")]
    public Image stateImage_escaping;
    public Image stateImage_chilling;
    public Image stateImage_lookingOut;
}
