using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player", fileName = "Player Stats")]
public class PlayerStat : ScriptableObject
{
    [SerializeField] Stat baseStamina;
    [SerializeField] Stat baseHP;

    float currentStamina;
    int currentHP;

    public FloatEventChannelSO OnStaminaChanged;
    public FloatEventChannelSO OnHPChanged;
    void OnEnable()
    {
        currentStamina = baseStamina.BaseValue;
        currentHP = baseHP.BaseValue;
    }

    public int CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP = (value >= 0) ? value : 0;
            currentHP = (value <= MaxHP) ? value : MaxHP;
            OnHPChanged.RaiseEvent(currentHP);
        }
    }
    public float CurrentStamina
    {
        get => currentStamina;
        set
        {
            currentStamina = (value >= 0) ? value : 0;
            currentStamina = (value <= MaxStamina) ? value : MaxStamina;
            OnStaminaChanged.RaiseEvent(currentStamina);
        }
    }

    public int MaxStamina { get => baseStamina.BaseValue; }
    public int MaxHP { get => baseHP.BaseValue; }
}
