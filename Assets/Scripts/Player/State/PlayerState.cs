using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected Animator animator;
    protected MovementController movementController;
    protected PlayerAnimationHandler playerAnimationHandler;

    public PlayerState(Player player, Animator animator, MovementController movementController, PlayerAnimationHandler playerAnimationHandler)
    {
        this.player = player;
        this.animator = animator;
        this.movementController = movementController;
        this.playerAnimationHandler = playerAnimationHandler;
    }

    public virtual void Update()
    {
        StateChangeLogic();
    }
    public virtual void FixedUpdate() { }
    public virtual void StateChangeLogic() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
}
