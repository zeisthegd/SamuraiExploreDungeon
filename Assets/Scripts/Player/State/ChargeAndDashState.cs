using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAndDashState : PlayerState
{
    private bool releaseCharge = false;
    public ChargeAndDashState(Player player, Animator animator, MovementController movementController, PlayerAnimationHandler playerAnimationHandler) : base(player, animator, movementController, playerAnimationHandler)
    {
    }

    public override void Update()
    {
        base.Update();
        movementController.Charge();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Dash();
    }
    public override void Enter()
    {
        base.Enter();
        playerAnimationHandler.SetCharge(true);
        //VFX Charging
    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void StateChangeLogic()
    {
        if (Input.GetButtonUp("Charge"))
        {
            releaseCharge = true;
            playerAnimationHandler.SetCharge(false);
        }
    }

    private void Dash()
    {
        if (releaseCharge)
        {
            movementController.Dash();
            releaseCharge = false;
            player.ChangeStateToIdle();
        }
    }
}
