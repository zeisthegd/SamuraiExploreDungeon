using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    public RunState(Player player, Animator animator, MovementController movementController, PlayerAnimationHandler playerAnimationHandler) : base(player, animator, movementController, playerAnimationHandler)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movementController.Run();
    }

    public override void Update()
    {
        base.Update();
        playerAnimationHandler.SetRunnning(movementController.HasRunInput);

    }

    public override void StateChangeLogic()
    {
        if (Input.GetButton("Charge"))
        {
            player.ChangeStateToCharge();
        }
    }
    public override void Enter()
    {
        base.Enter();
        playerAnimationHandler.SetRunnning(true);
    }

    public override void Exit()
    {
        base.Exit();
        playerAnimationHandler.SetRunnning(false);
    }

}
