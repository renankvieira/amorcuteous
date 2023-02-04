using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringedActionCaller : MonoBehaviour
{
    public bool callOnStart = false;
    public string stringedActionParam = "";

    void Start()
    {
        if (callOnStart)
            CallAction();
    }

    public void CallAction() => RoundEvents.Instance.stringedAction.SafeInvoke(stringedActionParam);
    public void CallActionWithCustomParam(string param) => RoundEvents.Instance.stringedAction.SafeInvoke(param);
}
