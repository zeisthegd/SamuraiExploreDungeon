using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : AnimationHandler
{
    public PlayerAnimationHandler(Animator animator) : base(animator)
    {
    }

    public void SetRunnning(bool isRunningCondition)
    {
        SetBool("isRunning", isRunningCondition);
    }

    public void SetCharge(bool isCharing)
    {
        SetBool("isCharging", isCharing);
    }

    public void SetCanDash(bool canDash)
    {
        SetBool("canDash", canDash);
    }


}
