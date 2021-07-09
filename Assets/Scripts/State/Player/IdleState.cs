using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player,stateMachine)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        StateChangeLogic();
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
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            stateMachine.ChangeStateToRun();
        }
        if (Input.GetButton("Charge"))
        {
            stateMachine.ChangeStateToCharge();
        }
    }

    public override string ToString()
    {
        return "Idle";
    }
}
