using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorSpeed : GenericActionCaller
{
    [Header("AnimatorSpeed")]
    public float speed = 1f;

    Animator _animator;

    public override void MethodToCall()
    {
        base.MethodToCall();
        SetSpeed();
    }

    public void SetSpeed()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        _animator.speed = speed;
    }


}
