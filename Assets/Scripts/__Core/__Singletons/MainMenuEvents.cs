using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuEvents : SingletonOfType<MainMenuEvents>
{
    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public Action<string> stringedAction;

    public void LogEventToJson<T>(T t) => Debug.Log(JsonUtility.ToJson(t));
    public static void TestAction(string param0) => Instance.stringedAction.SafeInvoke(param0);
}
