
#define GAMEANALYTICS_IMPORTED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using GameAnalyticsSDK;

[DefaultExecutionOrder(-1000)]
public class AppManager : SingletonOfType<AppManager>
{
    [Header("Config")]
    [Expandable] public GameConfig gameConfig;
    [Expandable] public DebugConfig debugConfig;

    [Header("Control")]
    public AppSession appSession;
    [SerializeField] SaveableData playerProfile;
    public Vector3 startingGravity;

    bool appIsQuitting;

    override protected void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        InitializeApp();
        //InitializeSave();
        //InitializeGameAnalytics();
    }

    void InitializeApp()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        Application.targetFrameRate = 60;
        appSession = new AppSession();
        appSession.Initialize();

        startingGravity = Physics.gravity;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void InitializeSave()
    {
        playerProfile = PlayerProfileHandler.Profile;// .Initialize();

        if (DebugConfig.Instance.wipeSaveOnStartup)
        {
            SaveSystem.WipeSave();
            playerProfile = PlayerProfileHandler.Profile;

            int startingLevel = DebugConfig.Instance.startingLevelIndexOnWipe;
            if (startingLevel != 0)
            {
                playerProfile.levelIndex = startingLevel;
                playerProfile.maxLevelBeaten = startingLevel-1;
            }

            if (playerProfile.levelIndex > 0)
                playerProfile.tutorialComplete = 1;

            playerProfile.starsOwned = DebugConfig.Instance.startingStarsOnWipe;
            playerProfile.starsOnBounsChest = DebugConfig.Instance.startingStarsOnWipe;
            //if (playerProfile.starsOnBounsChest > GameConfig.Instance.starsRequiredForBonusChest)
            //    playerProfile.starsOnBounsChest = GameConfig.Instance.starsRequiredForBonusChest;

            playerProfile.moneyOwned = DebugConfig.Instance.startingMoneyOnWipe;
        }

        if (PlayerProfileHandler.Profile.options_isGameMuted)
            PlayerProfileHandler.Profile.options_globalVolume = 0f;
        AudioListener.volume = PlayerProfileHandler.Profile.options_globalVolume;

        //ProgressionManager.Instance.Initialize();
    }

    void InitializeGameAnalytics()
    {
#if GAMEANALYTICS_IMPORTED
        this.DelayOneFrameAndInvoke(() =>
        {
            //GameAnalytics.Initialize();
            //GameAnalyticsManager.Instance.RegisterDesignEvent("General", "AppStart");
    });
#endif
    }

    void OnApplicationQuit()
    {
        appIsQuitting = true;
    }


    public bool IsOnEditor
    {
        get
        {
            if (Application.isEditor)
                return true;
            return false;
        }
    }

    public static bool AppIsQuitting
    {
        get
        {
            if (Instance == null)
                return true;
            return Instance.appIsQuitting;
        }
    }

}

[System.Serializable]
public class AppSession
{
    public long sessionID = 0;

    public void Initialize()
    {
        var now = System.DateTime.Now;
        long year = (now.Year - 2000) * 10000000000000L;
        long month = now.Month * 100000000000L;
        long day = now.Day * 1000000000L;
        long hour = now.Hour * 10000000L;
        long minute = now.Minute * 100000L;
        long seconds = now.Second * 1000L;
        long miliseconds = now.Millisecond;
        sessionID = year + month + day + hour + minute + seconds + miliseconds;
    }
}