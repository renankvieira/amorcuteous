using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonOfType<GameManager>
{
    [Header("Control")]
    public bool roundIsOn = true;
    public bool roundWon = false;
    public Player player;

    protected override void Awake()
    {
        base.Awake();
    }

    public void FinishRound(bool roundWon)
    {
        if (!roundIsOn)
            return;

        roundIsOn = false;
        this.roundWon = roundWon;
        RoundEvents.Instance.onRoundOver.SafeInvoke(roundWon);
    }
}
