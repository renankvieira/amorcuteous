using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : GenericActionCaller
{
    public bool destroySelf = false;
    public GameObject gameObjectToDestroy;

    public override void MethodToCall()
    {
        base.MethodToCall();
        DestroyObject();
    }

    public void DestroyObject()
    {
        if (destroySelf)
            Destroy(gameObject);
        else
            Destroy(gameObjectToDestroy);
    }
}
