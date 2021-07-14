using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSword : Sword
{
    public override void Start()
    {
        base.Start();
    }
    public override void CreateChargeEffect()
    {
        base.CreateChargeEffect();
    }

    public override void CreateDashEffect()
    {
        base.CreateDashEffect();
    }

    public override void CreateSlashEffect()
    {
        base.CreateSlashEffect();
    } 

    public override void StartChargeEffect(float playBackSpeed)
    {
        base.StartChargeEffect(playBackSpeed);
    }

    public override void StartDashEffect()
    {
        base.StartDashEffect();
    }

    public override void StartSlashEffect()
    {
        base.StartSlashEffect();
    }

    public override void SpeedUpChargeEffect()
    {
        base.SpeedUpChargeEffect();
        
    }

    public override string ToString()
    {
        return base.ToString();
    }

    //Some fire sword attack: shoot fire ball
}
