using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : UserInterface
{
    DisplayUI displayUI;
    Slider slider;
    public FloatEventChannelSO OnPlayerStaminaChanged;
    public override void Start()
    {
        base.Start();
        displayUI = GetComponent<DisplayUI>();
        slider = GetComponent<Slider>();

        slider.maxValue = player.Stats.MaxStamina;
        slider.value = slider.maxValue;

    }

    void DisplayStamina(float stamina)
    {
        displayUI.Show();
        slider.value = stamina;
    }

    void OnEnable()
    {
        OnPlayerStaminaChanged.OnEventRaised += DisplayStamina;

    }

    void OnDisable()
    {
        OnPlayerStaminaChanged.OnEventRaised -= DisplayStamina;
    }
}
