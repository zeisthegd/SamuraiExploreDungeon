using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        player.InputReader.chargeBegin += stateMachine.ChangeStateToCharge;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        StateChangeLogic();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void StateChangeLogic()
    {
        if (movementController.HasRunInput)
        {
            stateMachine.ChangeStateToRun();
        }
    }

    public override string ToString()
    {
        return "Idle";
    }
}
