using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerSetter : MonoBehaviour
{
    public string triggerName = "notSet";
    public Animator animator;

    public void SetTrigger()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(triggerName);
    }
}
