using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.ShortcutManagement;

[CreateAssetMenu(fileName = "DebugConfig_", menuName = "General/New Debug Config")]
public class DebugConfig : ScriptableObject
{
    public static DebugConfig Instance => AppManager.Instance.debugConfig;

    [Header("Game Flow")]
    public bool neverShowTutorial = false;
    public bool skipLevelIntro = false;

    [Header("Player")]
    public bool ultraDamage = false;
    public bool ultraTime = false;
    public bool zeroDamage = false;
    public bool skipKillCam = false;
    public bool autoWin = false;
	public bool autoLose = false;
    public bool holdToShoot = false;

    [Header("Logs")]
    public bool logEnemyDamage = false;
    public bool logEnemyBuildingAttacks = false;
    public bool logAllSFX = false;

    [Header("Etc")]
    public bool showFeedbackInput = false;
    public SystemLanguage debugLanguage = SystemLanguage.Unknown;

    [Header("Save")]
    public bool wipeSaveOnStartup = true;
    public bool wipeSaveEvenOnBuild = false;
    [Range(0, 40)] public int startingLevelIndexOnWipe = 0;
    [Range(0, 100)] public int startingStarsOnWipe = 0;
    [Range(0, 1000)] public int startingMoneyOnWipe = 0;
    //public bool useDebugSave;
    //public SaveableData debugSave;
    //public SaveableData watchSave;

#if UNITY_EDITOR
    [ButtonSO]
    public void LoadMainMenu()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/MainMenuScene.unity");
    }

    [ButtonSO]
    public void LoadToyScene()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/ToyScene.unity");
    }

    [UnityEditor.MenuItem("CuteArmy/Load Main Menu", priority = 1)]
    public static void LoadMainMenuShortcut()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/MainMenuScene.unity");
    }

    [UnityEditor.MenuItem("CuteArmy/Load Toy Scene", priority = 2)]
    public static void LoaToySceneShortcut()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/ToyScene.unity");
    }
#endif
}