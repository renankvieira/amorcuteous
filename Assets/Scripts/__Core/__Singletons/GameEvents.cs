using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : SingletonOfType<GameEvents>
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

    public Action<int> gainMoney;
    public Action<int> onMoneyGained;
    public Action<int> onMoneyOwnedChange;

}
