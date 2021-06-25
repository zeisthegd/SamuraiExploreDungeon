using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class MovementController : MonoBehaviour
{
    protected Animator animator;
    protected Transform objectTrf;
    protected Rigidbody rb2d;
    [SerializeField] MovementSettings settings;

    float horInp = 0;
    float vertInp = 0;

    void Awake()
    {

    }
    void Start()
    {
        objectTrf = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        settings.camera = Camera.main;
    }

    void Update()
    {
        Dash();
    }

    void FixedUpdate()
    {
        HandleInput();
        HandleAnimationTransition();
    }

    public void HandleInput()
    {
        ApplyGroundMovement();
    }


    private void ApplyGroundMovement()
    {

        horInp = Input.GetAxisRaw("Horizontal");
        vertInp = Input.GetAxisRaw("Vertical");
        float speedMultiplier = (Input.GetButton("Sprint")) ? settings.sprintMultiplier : 1;
        Vector3 moveDirection = new Vector3(horInp, 0f, vertInp);

        if (moveDirection.magnitude != 0)
        {
            RotateDirection45Deg(ref moveDirection);
            moveDirection.Normalize();
            rb2d.AddForce(moveDirection * settings.moveSpeed * speedMultiplier * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            Vector3 worldMousePos = GetComponent<MouseLook>().WorldMousePosition;
            Vector3 dashDir = (worldMousePos - transform.position).normalized;
            dashDir = new Vector3(dashDir.x, 0, dashDir.z);

            rb2d.AddForce(dashDir * 5000 * Time.deltaTime, ForceMode.Impulse);

        }
    }

    private void Slash()
    {

    }

    private void HandleAnimationTransition()
    {
        animator.SetBool("isRunning", horInp != 0 || vertInp != 0);
    }

    private void RotateDirection45Deg(ref Vector3 direction)
    {
        if (direction.magnitude >= 0.01f)
        {
            float rotateOffset = 45F * Mathf.Deg2Rad;
            float horAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + settings.camera.transform.eulerAngles.y;

            direction = Quaternion.Euler(0F, horAngle - rotateOffset, 0F) * Vector3.forward;
        }
    }

}
