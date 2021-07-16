using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Animator animator;
    protected PlayerAnimationHandler animationHandler;
    protected PlayerMovementController movementController;

    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.animator = player.Animator;
        this.movementController = player.MovementController;
        this.stateMachine = stateMachine;
        this.animationHandler = new PlayerAnimationHandler(animator);
    }

    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
    public virtual void StateChangeLogic()
    {

    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }


}
