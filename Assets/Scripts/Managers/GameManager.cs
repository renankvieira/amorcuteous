using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonOfType<GameManager>
{
    [Header("Control")]
    public LevelConfig currentLevel;
    public float timeOnRound = 0;
    public bool roundIsOn = true;
    public bool roundWon = false;
    public Player player;
    public int killCount = 100;

    public float NormalizedTimeOnRound=> timeOnRound / currentLevel.spawnProgressionLength;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        timeOnRound += Time.deltaTime;
    }

    public void OnEnemyDeath()
    {
        if (!roundIsOn)
            return;

        killCount--;
        RoundEvents.Instance.onKillCountChange.SafeInvoke(killCount);
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
