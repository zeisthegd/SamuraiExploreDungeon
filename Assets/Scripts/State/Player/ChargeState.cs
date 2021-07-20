using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : PlayerState
{
    TimeManager timeManager;
    bool canDash;
    public ChargeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        timeManager = player.TimeManager;
        movementController.UnableToDash += UnableToDash;
        player.InputReader.chargeEnd += DashAndChangeState;
    }
    public override void Enter()
    {
        base.Enter();
        if (!movementController.InsufficientStamina)
        {
            timeManager.BeginSlowMotion();
            player.Sword.StartChargeEffect(1 / movementController.RemainingStaminaPercent());
            animationHandler.SetCharge(true);
        }
        else stateMachine.ChangeStateToIdle();
    }

    public override void Update()
    {
        base.Update();
        ChargeTheAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void Exit()
    {
        base.Exit();
        player.InputReader.chargeBegin += stateMachine.ChangeStateToCharge;
        player.Sword.SpeedUpChargeEffect();
        timeManager.EndSlowMotion();
        animationHandler.SetCharge(false);
    }

    public void ChargeTheAttack()
    {
        movementController.Charge();
    }

    private void DashAndChangeState()
    {
        canDash = true;
        animationHandler.SetCanDash(canDash);
        movementController.Dash();
        stateMachine.ChangeStateToDash();
    }

    private void UnableToDash()
    {
        canDash = false;
        animationHandler.SetCanDash(canDash);
        stateMachine.ChangeStateToIdle();
    }

    private void BeginSlowMotion()
    {

    }

    public override string ToString()
    {
        return "Charge";
    }

}
