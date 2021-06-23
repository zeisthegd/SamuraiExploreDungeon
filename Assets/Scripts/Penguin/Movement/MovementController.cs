using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    protected Animator animator;
    protected Transform objectTrf;
    protected Rigidbody rb2d;
    protected MovementSettings settings;

    float horInp = 0;
    float vertInp = 0;

    public MovementController() { }
    public MovementController(Transform objectTransform, Rigidbody rb2d, Animator animator, MovementSettings settings)
    {
        this.objectTrf = objectTransform;
        this.rb2d = rb2d;
        this.settings = settings;
        this.animator = animator;
        ResetState();
    }

    public void HandleInput()
    {
        ApplyGroundMovement();
        ApplyGravity();
        HandleAnimationTransition();
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

    private void ApplyGravity()
    {
        Vector3 jumpForce = Vector3.down * settings.onlandGravity;
        rb2d.AddForce(jumpForce, ForceMode.Force);
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
    public void ResetState()
    {
        Physics.gravity = Vector3.down * settings.swimmingGravity;
    }

}
