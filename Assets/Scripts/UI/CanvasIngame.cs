using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasIngame : SingletonOfType<CanvasIngame>
{
    public GameObject gameOver_Lost;

    protected override void Awake()
    {
        base.Awake();

        RoundEvents.Instance.onRoundOver += OnRoundOver;
    }


    void OnRoundOver(bool gameWon)
    {
        if (!gameWon)
            gameOver_Lost.SetActive(true);
    }
}
