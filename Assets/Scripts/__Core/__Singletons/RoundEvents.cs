using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundEvents : SingletonOfType<RoundEvents>
{

    public Action<int> onKillCountChange;

    public Action<bool> onRoundOver;


    //Debug
    public static void LogEventToJson<T>(T t) => Debug.Log(JsonUtility.ToJson(t));
    public static void TestAction(string param0) => Instance.stringedAction.SafeInvoke(param0);

    public Action<string> stringedAction;

    public Action<float> onPlayerHpChange;
    public Action<float> onPlayerHpRatioChange;

    //public Action<Enemy> OnEnemyKilled;
    //public Action<int> OnEnemyKilledCountChange;

    //public Action<XpPellet> onXpPelletCollected;
}
