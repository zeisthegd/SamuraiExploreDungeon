﻿using System;
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
    [SerializeField] float dashPowerThreshold;
    [SerializeField] MovementSettings settings;

    float horInp = 0;
    float vertInp = 0;
    float dashSpeed = 0;
    bool canRefill = false;




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
        if (playerStat.CurrentStamina > 0)
            dashSpeed += (settings.dashSpeedIncrementRate * Time.deltaTime);
        DepleteStamina();
    }


    public void Dash()
    {
        if (dashSpeed >= dashPowerThreshold)
        {
            rgBody.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        }
        else UnableToDash?.Invoke();
        dashSpeed = 0;
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

    private void DepleteStamina()
    {
        playerStat.CurrentStamina -= (settings.staminaDepletionRate * Time.deltaTime);
        StopAllCoroutines();
        StartCoroutine(RefillStamina());
        playerStat.CurrentStamina = Mathf.Clamp(playerStat.CurrentStamina, 0, playerStat.MaxStamina);
    }

    public void RefillStaminaImmediately()
    {
        while (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {
            playerStat.CurrentStamina += (settings.staminaDepletionRate * Time.deltaTime);
        }
    }

    public IEnumerator RefillStamina()
    {
        yield return new WaitForSeconds(1);
        while (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {
            playerStat.CurrentStamina += (settings.staminaDepletionRate * Time.deltaTime);
            yield return new WaitForSeconds(0.02F);
        }
    }

    public System.Action UnableToDash;

    public bool HasRunInput { get { return horInp != 0 || vertInp != 0; } }
    public bool InsufficientStamina { get => playerStat.CurrentStamina <= 0; }

}
