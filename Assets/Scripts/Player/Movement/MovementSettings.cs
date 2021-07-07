﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement Settings", fileName = "Movement/Settings")]
public class MovementSettings : ScriptableObject
{
    public float moveSpeed;
    public float dashSpeed;
    public float onlandGravity;
    public float mouseSensitivity;
    public float turnSmooth;
    public float turnSmoothVelocity;
    public Camera camera;
}