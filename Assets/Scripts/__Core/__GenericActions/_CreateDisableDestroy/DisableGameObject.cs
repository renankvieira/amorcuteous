using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : GenericActionCaller
{
    public bool targetSelf = false;
    public GameObject targetGameObject;

    public override void MethodToCall()
    {
        base.MethodToCall();
        DisableObject();
    }

    public void DisableObject()
    {
        if (targetSelf)
            targetGameObject = gameObject;
        targetGameObject.SetActive(false);
    }
}
