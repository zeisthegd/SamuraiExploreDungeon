using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] SwordData swordData;

    List<ParticleSystem> chargeParticles = new List<ParticleSystem>();

    [Space]
    [Header("Spawn Positions")]
    [SerializeField] Transform chargeSpawnPos;
    public virtual void Start()
    {
        CreateChargeEffect();
    }

    public virtual void StartSlashEffect()
    {

    }

    public virtual void StartDashEffect()
    {

    }

    public virtual void StartChargeEffect(float playBackSpeed)
    {
        foreach (ParticleSystem effect in chargeParticles)
        {
            ParticleSystem.MainModule main = effect.main;
            main.simulationSpeed = playBackSpeed;
            effect.Play();
        }
    }

    public virtual void StopChargeEffect()
    {
        foreach (ParticleSystem effect in chargeParticles)
        {
            effect.Stop();
        }
    }

    #region Create Effects

    [ContextMenu("CreateSlashEffect")]
    public virtual void CreateSlashEffect()
    {
        Instantiate(swordData.SlashEffect, transform.position, Quaternion.identity, this.transform);
    }

    [ContextMenu("CreateDashEffect")]
    public virtual void CreateDashEffect()
    {
        Instantiate(swordData.DashEffect, transform.position, Quaternion.identity, this.transform);
    }

    [ContextMenu("CreateChargeEffect")]
    public virtual void CreateChargeEffect()
    {
        var chargeEffect = Instantiate(swordData.ChargeEffect, chargeSpawnPos.position, Quaternion.identity, chargeSpawnPos.transform);
        for (int i = 0; i < chargeEffect.transform.childCount; i++)
            if (chargeEffect.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>())
                chargeParticles?.Add(chargeEffect.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>());
    }

    #endregion

    public SwordData SwordData { get => swordData; }
}
