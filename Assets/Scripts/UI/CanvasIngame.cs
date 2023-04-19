using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasIngame : SingletonOfType<CanvasIngame>
{
    public GameObject gameOver_Lost;
    public TMPro.TextMeshProUGUI killCount;

    protected override void Awake()
    {
        base.Awake();

        RoundEvents.Instance.onKillCountChange += UpdateKillCount;
        RoundEvents.Instance.onRoundOver += OnRoundOver;
    }

    void UpdateKillCount(int killCount)
    {
        this.killCount.text = killCount.ToString();
    }

    void OnRoundOver(bool gameWon)
    {
        if (!gameWon)
            gameOver_Lost.SetActive(true);
    }

}
