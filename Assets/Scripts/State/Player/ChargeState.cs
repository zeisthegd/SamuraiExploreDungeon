using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAndDashState : PlayerState
{
    TimeManager timeManager;
    bool canDash;
    public ChargeAndDashState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        movementController.UnableToDash += UnableToDash;
        timeManager = player.TimeManager;
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
    public override void Enter()
    {
        base.Enter();
        if (HasStamina())
        {
            timeManager.BeginSlowMotion();
            animationHandler.SetCharge(true);
        }
        else stateMachine.ChangeStateToIdle();
    }
    public override void Exit()
    {
        base.Exit();
        timeManager.EndSlowMotion();
        animationHandler.SetCharge(false);
    }

    private bool HasStamina()
    {
        return !movementController.InsufficientStamina;
    }

    public void ChargeTheAttack()
    {
        if (Input.GetButton("Charge"))
        {
            movementController.Charge();
        }
        if (Input.GetButtonUp("Charge"))
        {
            DashAndChangeState();
        }
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
