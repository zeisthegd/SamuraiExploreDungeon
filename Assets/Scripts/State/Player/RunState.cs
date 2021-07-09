﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    public RunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
        animationHandler.SetRunnning(movementController.HasRunInput);
        StateChangeLogic();
        Debug.Log(movementController.HasRunInput);
    }

    public override void StateChangeLogic()
    {
        if (!movementController.HasRunInput)
            stateMachine.ChangeStateToIdle();
        if (Input.GetButton("Charge"))
            stateMachine.ChangeStateToCharge();
    }
    public override void Enter()
    {
        base.Enter();
        animationHandler.SetRunnning(true);
    }

    public override void Exit()
    {
        base.Exit();
        animationHandler.SetRunnning(false);
    }


    public override string ToString()
    {
        return "Run";
    }

}
