using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler
{
    protected Animator animator;
    public AnimationHandler(Animator animator)
    {
        this.animator = animator;
    }

    protected void SetBool(string paramName, bool value)
    {
        animator.SetBool(paramName, value);
    }
    protected void SetInt(string paramName, int value)
    {
        animator.SetInteger(paramName, value);

    }
    protected void SetFloat(string paramName, float value)
    {
        animator.SetFloat(paramName, value);

    }
    protected void SetTrigger(string paramName)
    {
        animator.SetTrigger(paramName);

    }
}
