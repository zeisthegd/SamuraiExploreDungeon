using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class MovementController : MonoBehaviour
{
    private Transform objectTrf;
    private Rigidbody rgBody;
    private PlayerStat playerStat;
    [SerializeField] MovementSettings settings;

    float horInp = 0;
    float vertInp = 0;
    bool haveToDash = false;
    float dashSpeed = 0;




    void Start()
    {
        objectTrf = GetComponent<Transform>();
        rgBody = GetComponent<Rigidbody>();
        playerStat = GetComponent<PlayerStat>();

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
            rgBody.AddForce(moveDirection * settings.moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void Charge()
    {
        if (Input.GetButton("Charge"))
        {
            haveToDash = false;
            playerStat.CurrentStamina -= (settings.staminaDepletionRate * Time.deltaTime);
            dashSpeed += (settings.dashSpeedIncrementRate * Time.deltaTime);
        }
        if (Input.GetButtonUp("Charge") || playerStat.CurrentStamina <= 0)
        {
            haveToDash = true;
        }

    }


    public void Dash()
    {
        if (haveToDash)
        {
            rgBody.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            haveToDash = false;
            dashSpeed = 0;
            OnPlayerDash?.Invoke();
        }
    }

    public void Slash()
    {

    }

    public void CheckToRefillStamina()
    {
        if (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {
            StartCoroutine(nameof(WaitTillRefill));
            StartCoroutine(nameof(RefillStamina));
        }
    }

    IEnumerator RefillStamina()
    {
        while (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {

        }
        yield return new WaitForSeconds(5);
    }
    IEnumerator WaitTillRefill()
    {
        yield return new WaitForSeconds(2);
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
    public System.Action OnPlayerDash;


    public bool HasRunInput { get { return horInp != 0 || vertInp != 0; } }
    public bool InsufficientStamina { get => playerStat.CurrentStamina <= 0; }

}
