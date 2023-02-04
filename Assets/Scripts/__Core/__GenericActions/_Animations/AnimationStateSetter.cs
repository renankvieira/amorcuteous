using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateSetter : GenericActionCaller
{
    [Header("ASS")]
    public string stateName = "notSet";
    public Animator animator;

    public override void MethodToCall()
    {
        base.MethodToCall();
        SetTrigger();
    }

    public void SetTrigger()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        animator.Play(stateName);
    }
}
