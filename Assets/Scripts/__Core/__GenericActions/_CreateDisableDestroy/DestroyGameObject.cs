using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : GenericActionCaller
{
    public bool targetSelf = false;
    public GameObject targetGameObject;

    public override void MethodToCall()
    {
        base.MethodToCall();
        DestroyObject();
    }

    public void DestroyObject()
    {
        if (targetSelf)
            targetGameObject = gameObject;
        Destroy(targetGameObject);
    }
}
