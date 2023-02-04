using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : GenericActionCaller
{
    public Transform rotatingTransform;
    public Camera targetCamera;
    public bool autoSetMainCamera;
    public bool thenRotate180 = false;

    public override void MethodToCall()
    {
        base.MethodToCall();
        DoRotate();
    }

    void DoRotate()
    {
        if (rotatingTransform == null)
            return;
        if (autoSetMainCamera)
            if (targetCamera == null)
                targetCamera = Camera.main;

        rotatingTransform.LookAt(targetCamera.transform);

        if (thenRotate180)
            rotatingTransform.Rotate(0, 180, 0);
    }
}
