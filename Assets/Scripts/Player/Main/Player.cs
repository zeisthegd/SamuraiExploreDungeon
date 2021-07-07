using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a state machine
public class Player : MonoBehaviour
{

    Rigidbody rgBody;
    Animator animator;
    MovementController movementController;
    PlayerAnimationHandler playerAnimationHandler;


    PlayerState currentState;
    PlayerState idleState;
    PlayerState runState;
    PlayerState slashState;
    PlayerState chargeState;


    void Awake()
    {

    }
    void Start()
    {
        GetComponents();
        InitStateMachine();
    }


    void Update()
    {
        currentState.Update();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void GetComponents()
    {
        rgBody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        playerAnimationHandler = new PlayerAnimationHandler(animator);
    }


    #region State Managing

    public void ChangeStateToIdle()
    {
        currentState = idleState;
    }
    public void ChangeStateToRun()
    {
        currentState = runState;
    }
    public void ChangeStateToSlash()
    {
        currentState = slashState;
    }
    public void ChangeStateToCharge()
    {
        currentState = chargeState;
    }

    private void InitStateMachine()
    {
        idleState = new IdleState(this, animator, movementController, playerAnimationHandler);
        runState = new RunState(this, animator, movementController, playerAnimationHandler);
        slashState = new SlashState(this, animator, movementController, playerAnimationHandler);
        chargeState = new ChargeAndDashState(this, animator, movementController, playerAnimationHandler);
        currentState = idleState;
    }

    private void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }


    #endregion

    public PlayerState CurrentState { get => currentState; }

}
