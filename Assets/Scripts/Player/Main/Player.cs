using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a state machine
public class Player : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    [SerializeField] Sword sword;
    Rigidbody rgBody;
    Animator animator;
    MovementController movementController;
    PlayerStateMachine stateMachine;

    void Awake()
    {
        GetComponents();
        FindEssentialGameObjects();
    }
    void Start()
    {
        CreateStateMachine();
    }


    void Update()
    {
        stateMachine.CurrentState.Update();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }

    private void CreateStateMachine()
    {
        stateMachine = new PlayerStateMachine(this);
        stateMachine.Initialize(stateMachine.IdleState);
    }

    private void GetComponents()
    {
        rgBody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FindEssentialGameObjects()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }


    public Rigidbody RigidBody { get => rgBody; }
    public Animator Animator { get => animator; }
    public MovementController MovementController { get => movementController; }
    public TimeManager TimeManager { get => timeManager; }
    public PlayerStateMachine StateMachine { get => stateMachine;}
    public Sword Sword { get => sword;}
}
