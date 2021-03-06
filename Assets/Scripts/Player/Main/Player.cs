using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a state machine
public class Player : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    [SerializeField] Sword sword;
    [SerializeField] PlayerStat stats;
    [SerializeField] InputReader inputReader;

    Rigidbody rgBody;
    Animator animator;
    PlayerMovementController movementController;
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
        movementController = GetComponent<PlayerMovementController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FindEssentialGameObjects()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void OnCollisionEnter(Collision other)
    {

    }

    public Rigidbody RigidBody { get => rgBody; }
    public Animator Animator { get => animator; }
    public PlayerMovementController MovementController { get => movementController; }
    public TimeManager TimeManager { get => timeManager; }
    public PlayerStateMachine StateMachine { get => stateMachine; }
    public Sword Sword { get => sword; }
    public PlayerStat Stats { get => stats; }
    public InputReader InputReader { get => inputReader; set => inputReader = value;}
}
