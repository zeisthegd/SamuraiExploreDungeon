using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{
    [SerializeField] Stat stamina;
    [SerializeField] float currentStamina;

    void Start()
    {
        currentStamina = stamina.BaseValue;
    }

    public override void Die()
    {
        base.Die();
        //Die sfx
    }

    public float MaxStamina { get => stamina.BaseValue; }
    public float CurrentStamina
    {
        get => currentStamina;
        set
        {
            currentStamina = (value >= 0) ? value : 0;
        }
    }

}
