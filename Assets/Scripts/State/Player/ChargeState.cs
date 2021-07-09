using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAndDashState : PlayerState
{
    bool canDash;
    public ChargeAndDashState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        movementController.UnableToDash += UnableToDash;
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
            animationHandler.SetCharge(true);
        }
        else stateMachine.ChangeStateToIdle();
    }
    public override void Exit()
    {
        base.Exit();
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

    public override string ToString()
    {
        return "Charge";
    }

}
