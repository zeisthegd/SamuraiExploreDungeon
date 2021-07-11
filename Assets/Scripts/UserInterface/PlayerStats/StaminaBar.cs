using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : UserInterface
{
    DisplayUI displayUI;
    Slider slider;
    MovementController movementController;
    public override void Start()
    {
        base.Start();
        displayUI = GetComponent<DisplayUI>();
        slider = GetComponent<Slider>();
        slider.maxValue = player.GetComponent<PlayerStat>().MaxStamina;
        slider.value = slider.maxValue;
        movementController = player.MovementController;
        movementController.OnStaminaChanged += DisplayStamina;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    void DisplayStamina(float stamina)
    {
        displayUI.Show();
        slider.value = stamina;
    }

}
