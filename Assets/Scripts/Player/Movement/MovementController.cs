using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class MovementController : MonoBehaviour
{
    private Transform objectTrf;
    private Rigidbody rb2d;
    [SerializeField] MovementSettings settings;

    float horInp = 0;
    float vertInp = 0;



    void Start()
    {
        objectTrf = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody>();

        settings.camera = Camera.main;
    }


    public void Run()
    {
        horInp = Input.GetAxisRaw("Horizontal");
        vertInp = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horInp, 0f, vertInp);

        if (moveDirection.magnitude != 0)
        {
            RotateDirection45Deg(ref moveDirection);
            moveDirection.Normalize();
            rb2d.AddForce(moveDirection * settings.moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void Charge()
    {
        if (Input.GetButton("Charge"))
        {
            rb2d.velocity = Vector3.zero;
        }

    }

    public void Dash()
    {
        rb2d.AddForce(transform.forward * settings.dashSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    public void Slash()
    {

    }

    private void HandleAnimationTransition()
    {

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

    public bool HasRunInput { get { return horInp != 0 || vertInp != 0; } }

}
