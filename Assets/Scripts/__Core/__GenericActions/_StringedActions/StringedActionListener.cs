using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringedActionListener : MonoBehaviour
{
    public string stringedActionParam = "";
    public UnityEngine.Events.UnityEvent eventToCall;

    void Awake()
    {
        RoundEvents.Instance.stringedAction += ListenToStringedAction;
    }

    void ListenToStringedAction(string param0)
    {
        if (param0 == stringedActionParam)
            CallEvent();
    }

    void CallEvent()
    {
        eventToCall.Invoke();
    }
}
