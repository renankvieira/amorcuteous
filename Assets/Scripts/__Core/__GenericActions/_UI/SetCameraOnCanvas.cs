using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraOnCanvas : GenericActionCaller
{
    [Header("SetCamera")]
    public Canvas canvasReference;
    public bool useMainCamera = true;
    public Camera cameraReference;

    public override void MethodToCall()
    {
        base.MethodToCall();
        SetReference();
    }

    public void SetReference()
    {
        if (useMainCamera)
            cameraReference = Camera.main;
        canvasReference.worldCamera = cameraReference;
    }

}
