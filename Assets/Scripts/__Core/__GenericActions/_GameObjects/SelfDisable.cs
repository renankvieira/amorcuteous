using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : GenericActionCaller
{
    public override void MethodToCall()
    {
        base.MethodToCall();

        gameObject.SetActive(false);
    }

}