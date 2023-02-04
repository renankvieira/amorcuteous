using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomAnimatorSpeed : GenericActionCaller
{
    [Header("AnimatorSpeed")]
    public bool randomizeEveryCall = false;
    public MinMax speed = new MinMax() { min = 0.9f, max = 1.1f };

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

        if (randomizeEveryCall)
            _animator.speed = speed.NewRandomValueInside();
        else
            _animator.speed = speed.SameValueInside();

    }


}
