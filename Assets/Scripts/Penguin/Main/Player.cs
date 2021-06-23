using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    MovementSettings movementSettings;
    MovementController movementController;
    Rigidbody rb2d;
    Animator animator;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        GetAllComponents();
    }
    void Start()
    {

    }


    void Update()
    {
        Debug.DrawRay(transform.position, Camera.main.transform.eulerAngles * 1000, Color.blue);
    }

    void FixedUpdate()
    {
        movementController.HandleInput();

    }

    void GetAllComponents()
    {
        rb2d = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementController = new MovementController(this.transform, rb2d, animator, movementSettings);
        movementSettings.camera = Camera.main;
    }


    void OnCollisionEnter(Collision col)
    {

    }

    void OnCollisionExit(Collision col)
    {

    }


}
