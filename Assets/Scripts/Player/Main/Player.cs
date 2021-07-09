using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a state machine
public class Player : MonoBehaviour
{
    [SerializeField] Text playerState;
    Rigidbody rgBody;
    Animator animator;
    MovementController movementController;

    PlayerStateMachine stateMachine;

    void Awake()
    {
        GetComponents();
        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        stateMachine.Initialize(stateMachine.IdleState);
    }


    void Update()
    {
        stateMachine.CurrentState.Update();
        playerState.text = stateMachine.CurrentState.ToString();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }

    private void GetComponents()
    {
        rgBody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    public Rigidbody RigidBody { get => rgBody; }
    public Animator Animator { get => animator; }
    public MovementController MovementController { get => movementController; }
}
