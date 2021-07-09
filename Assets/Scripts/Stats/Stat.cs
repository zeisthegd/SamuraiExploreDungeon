using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    List<int> modifiers = new List<int>();

    bool modifiersApplied = false;
    public int BaseValue { get => baseValue; }
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }

    public void ApplyModifiers()
    {
        if (!modifiersApplied)
        {
            foreach (int modifiers in modifiers)
            {
                baseValue += modifiers;
            }
        }
    }
}
