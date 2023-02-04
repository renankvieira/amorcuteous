using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : SingletonOfType<RoundController>
{
    //[Header("Control")]
    //public int levelIndexPlayed = 0;
    //public bool gameOn = true;
    //public bool roundOver = false;
    //public bool roundWon = false;

    //public LevelConfig currentLevelConfig;

    //[Header("Debug")]
    //public bool skipLevelCreation = false;
    //public float timeOfLevelStart = 0f;

    //protected override void Awake()
    //{
    //    base.Awake();

    //    RoundEvents.Instance.finishRound += FinishRound;
    //}

    //void Start()
    //{
    //    Debug.Log("Not initializing round.");
    //    //Initialize();
    //}

    //public void Initialize()
    //{
    //    levelIndexPlayed = AppManager.Instance.playerProfile.levelIndex;
    //    currentLevelConfig = AppManager.Instance.gameConfig.GetLevelConfig(PlayerProfileHandler.Profile.levelIndex);

    //    if (!skipLevelCreation)
    //        currentLevelConfig.InstantiateLevelObjects();

    //    //GuiReferences.Instance.DisableAll(GuiReferences.Instance.GetStartingMenu());

    //    timeOfLevelStart = Time.time;

    //    RoundEvents.Instance.onRoundGameplayStart.SafeInvoke();
    //}

    //void FinishRound(bool gameWon)
    //{
    //    if (!gameOn)
    //        return;
    //    gameOn = false;
    //    roundOver = true;
    //    roundWon = gameWon;
    //    float timeTaken = Time.time - timeOfLevelStart;

    //    Debug.LogWarningFormat("[RC] Round over: {0}. Time taken: {1}", gameWon ? "won" : "lost", timeTaken.ToString("F2"));

    //    if (gameWon)
    //    {
    //        AppManager.Instance.playerProfile.levelIndex++;
    //    }
    //    else
    //    {
    //    }

    //    PlayerProfileHandler.SaveProfile();
    //    RoundEvents.Instance.onRoundOver.SafeInvoke(gameWon);
    //}
}
