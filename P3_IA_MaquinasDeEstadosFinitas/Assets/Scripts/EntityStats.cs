// Miguel Rodríguez Gallego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Stamina state stats 
/// </summary>
public class EntityStats : MonoBehaviour
{
    TargetDetector_Colleague targetDetector_Colleague;

    public bool isPaniquing;
    public bool isEscaping;

    [Header("Stamina")]
    public float stamina;
    public float maxStamina;
    [SerializeField] float staminaWaste = 15f;
    [SerializeField] float staminaRegen = 10f;
    [SerializeField] float staminaRegenStartTime = 2f;
    [SerializeField] float staminaRegenPerSecond = 20f;
    public static bool staminaResetCooldown;
    public static bool staminaRegenerated = true;
    float staminaCountdown = 0f;
    public bool canRegenStamina;

    [SerializeField] Slider staminaProgressBar; /// Health

    void Start()
    {
        targetDetector_Colleague = GetComponent<TargetDetector_Colleague>();
        UpdateStamina_UISlider();
        CheckStaminaState();
    }
    void Update()
    {
        UpdateStamina();
    }

    void UpdateStamina()
    {
        CheckStaminaState();
    }
    /// <summary>
    ///     Checks stamina state to make an action or another
    /// </summary>
    void CheckStaminaState()
    {
        if (stamina <= 0)
            canRegenStamina = true;

        if (stamina >= maxStamina)
            canRegenStamina = false;
    }

    /// <summary>
    ///     Lower stamina automatically with time
    /// </summary>
    public void UseStamina()
    {
        stamina -= staminaWaste * Time.deltaTime;
        UpdateStamina_UISlider();
    }
    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    public void RegenStamina()
    {
        stamina += staminaRegen * Time.deltaTime;
        UpdateStamina_UISlider();
    }
    /// <summary>
    ///     HUD Methods to show correctly stamina in slider bar
    /// </summary>
    void UpdateStamina_UISlider()
    {
        float fillAmount = stamina / maxStamina;
        staminaProgressBar.value = fillAmount;
    }
    /// <summary>
    ///     Get stamina private value
    /// </summary>
    public float GetStamina() => stamina;
    /// <summary>
    ///     Get stamina can regen private value
    /// </summary>
    public bool GetCanRegenStamina() => canRegenStamina;
}
