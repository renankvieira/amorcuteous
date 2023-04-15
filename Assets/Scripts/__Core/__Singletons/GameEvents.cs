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

    /// <summary>
    /// Game Won
    /// </summary>
}
