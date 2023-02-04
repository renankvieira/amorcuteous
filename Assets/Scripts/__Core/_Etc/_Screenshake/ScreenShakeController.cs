using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : SingletonOfType<ScreenShakeController>
{
    protected override void Awake()
    {
        base.Awake();

        //RoundEvents.Instance.onLandWithTurbo += () => land.Play();
        //RoundEvents.Instance.onRingPassed += () => ring.Play();
        //RoundEvents.Instance.onCoinCollected += () => coin.Play();
        //RoundEvents.Instance.onRoundOver += (won) => { if (won) win.Play(); };
        //RoundEvents.Instance.onRoundOver += (won) => { if (!won) death.Play(); };
    }
}
