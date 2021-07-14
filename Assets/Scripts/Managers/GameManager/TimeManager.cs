using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeManager : MonoBehaviour
{
    [SerializeField] float fixedDeltaTime;
    [SerializeField] [Range(0.01f, 0.2f)] float slowMotionTimeScale;
    [SerializeField] [Range(1, 10)] float slowDownTime;

    bool slowMoBegun = false;
    void Start()
    {

    }

    void Update()
    {
        ReturnTimeToNormal();
    }
    public void BeginSlowMotion()
    {
        if (!slowMoBegun)
        {
            slowMoBegun = true;
            fixedDeltaTime = Time.fixedDeltaTime;
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }
    }

    public void EndSlowMotion()
    {
        if (slowMoBegun)
        {
            slowMoBegun = false;
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedDeltaTime;
        }
    }

    void ReturnTimeToNormal()
    {
        Time.timeScale += (1f / slowDownTime * Time.unscaledDeltaTime);
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
    }
}