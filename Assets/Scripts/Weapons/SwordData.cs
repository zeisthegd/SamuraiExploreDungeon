using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Sword", fileName = "Sword")]
public class SwordData : ScriptableObject
{
    public string Name;
    public GameObject ChargeEffect;
    public GameObject DashEffect;
    public GameObject SlashEffect;

}
