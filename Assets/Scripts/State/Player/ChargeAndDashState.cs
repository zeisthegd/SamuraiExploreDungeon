using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAndDashState : PlayerState
{
    public ChargeAndDashState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        movementController.OnPlayerDash += OnPlayerJustDash;
    }

    public override void Update()
    {
        base.Update();
        movementController.Charge();
        StateChangeLogic();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movementController.Dash();

    }
    public override void Enter()
    {
        base.Enter();
        if (HasStamina())
        {
            animationHandler.SetCharge(true);
        }
    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void StateChangeLogic()
    {
        DashClipEnd();
    }

    private bool HasStamina()
    {
        if (movementController.InsufficientStamina)
        {
            animationHandler.SetCharge(false);
            stateMachine.ChangeStateToIdle();
            return false;
        }
        return true;
    }

    private void OnPlayerJustDash()
    {
        animationHandler.SetCharge(false);
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

    public override string ToString()
    {
        return "Charge";
    }

}
