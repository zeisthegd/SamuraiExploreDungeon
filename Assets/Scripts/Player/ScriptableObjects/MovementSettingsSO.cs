using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Settings", menuName = "Settings/Movement")]
public class MovementSettingsSO : ScriptableObject
{
    public float moveSpeed;
    public float staminaDepletionRate;//per frame
    public float dashSpeedIncrementRate;//per frame
    public float onlandGravity;
    public float mouseSensitivity;
    public float turnSmooth;
    public float turnSmoothVelocity;
    public Camera camera;
}