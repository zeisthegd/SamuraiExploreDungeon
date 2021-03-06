using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class PlayerMovementController : MonoBehaviour
{
    private Transform objectTrf;
    private Rigidbody rgBody;
    private PlayerStat playerStat;

    [SerializeField] MovementSettingsSO settings;
    [SerializeField] InputReader inputReader;
    [SerializeField] float dashPowerThreshold;
    [SerializeField] float smoothTime;
    float smoothVelocity;
    float horInp = 0;
    float vertInp = 0;
    float dashSpeed = 0;

    void Start()
    {
        objectTrf = GetComponent<Transform>();
        rgBody = GetComponent<Rigidbody>();
        playerStat = GetComponentInParent<Player>().Stats;
        inputReader.moveEvent += OnMove;
        settings.camera = Camera.main;
    }

    public void Run()
    {
        Vector3 moveDirection = new Vector3(horInp, 0f, vertInp);
        if (moveDirection.magnitude != 0)
        {
            RotateModel(ref moveDirection);
            moveDirection.Normalize();
            rgBody.AddForce(moveDirection * settings.moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void Charge()
    {
        if (playerStat.CurrentStamina > 0)
            dashSpeed += (settings.dashSpeedIncrementRate * Time.unscaledDeltaTime);
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

    private void RotateModel(ref Vector3 direction)
    {
        if (direction.magnitude >= 0.01f)
        {
            float rotateOffset = 45F * Mathf.Deg2Rad;
            float horAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + settings.camera.transform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, horAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
            direction = Quaternion.Euler(0F, horAngle - rotateOffset, 0F) * Vector3.forward;
        }
    }

    private void DepleteStamina()
    {
        SubtractStamina();
        StopAllCoroutines();
        StartCoroutine(RefillStamina());
        playerStat.CurrentStamina = Mathf.Clamp(playerStat.CurrentStamina, 0, playerStat.MaxStamina);
    }

    public void RefillStaminaImmediately()
    {
        while (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {
            AddStamina();
        }
    }

    public IEnumerator RefillStamina()
    {
        yield return new WaitForSeconds(0.75F);
        while (playerStat.CurrentStamina <= playerStat.MaxStamina)
        {
            AddStamina();
            yield return new WaitForSeconds(0.005F);
        }
    }

    private void AddStamina()
    {
        playerStat.CurrentStamina += (settings.staminaDepletionRate * Time.unscaledDeltaTime);
    }

    private void SubtractStamina()
    {
        playerStat.CurrentStamina -= (settings.staminaDepletionRate * Time.unscaledDeltaTime);
    }

    void OnMove(Vector2 axis)
    {
        horInp = axis.x;
        vertInp = axis.y;
    }
    public System.Action UnableToDash;

    public float RemainingStaminaPercent()
    {
        return playerStat.CurrentStamina / playerStat.MaxStamina;
    }
    public bool HasRunInput { get { return horInp != 0 || vertInp != 0; } }
    public bool InsufficientStamina { get => playerStat.CurrentStamina <= 0; }


}
