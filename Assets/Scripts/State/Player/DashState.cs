using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        DashClipEnd();
    }
    public override void Enter()
    {
        base.Enter();
        //play vfx
    }


    public override void Exit()
    {
        base.Exit();
        animationHandler.SetCanDash(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void StateChangeLogic()
    {
        base.StateChangeLogic();
    }

    public override string ToString()
    {
        return "Dash";
    }
    private void DashClipEnd()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;
        if (clipName.Contains("Idle"))
        {
            stateMachine.ChangeStateToIdle();
        }
    }
}
