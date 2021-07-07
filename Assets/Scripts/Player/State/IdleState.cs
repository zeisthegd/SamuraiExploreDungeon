using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, Animator animator, MovementController movementController, PlayerAnimationHandler playerAnimationHandler) : base(player, animator, movementController, playerAnimationHandler)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void StateChangeLogic()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            player.ChangeStateToRun();
        }
        if (Input.GetButton("Charge"))
        {
            player.ChangeStateToCharge();
        }
    }


}
