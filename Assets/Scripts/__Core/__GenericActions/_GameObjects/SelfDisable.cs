using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : GenericActionCaller
{
    public override void MethodToCall()
    {
        base.MethodToCall();

        DisableSelf();
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

}