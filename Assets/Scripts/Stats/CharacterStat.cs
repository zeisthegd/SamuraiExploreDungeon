using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] Stat maxHealth;
    int currentHealth = 0;

    void Awake()
    {
        currentHealth = maxHealth.BaseValue;
    }


    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " just DIED!");
    }

    void OnTriggerEnter(Collider other)
    {
        var objTag = other.tag;
        if (objTag == "PlayerWeapon" || objTag == "EnemyWeapon")
            TakeDamage();
    }
}
