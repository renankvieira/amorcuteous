using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : GenericActionCaller
{
    public Transform rotatingTransform;
    public Transform targetTransform;

    public override void MethodToCall()
    {
        base.MethodToCall();
        DoRotate();
    }

    void DoRotate()
    {
        if (rotatingTransform == null || targetTransform == null)
            return;

        rotatingTransform.LookAt(targetTransform.transform);
    }
}
