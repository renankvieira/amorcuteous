#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveableData
{
    [Header("System")]
    [SerializeField] string app_Version = "999";
    [SerializeField] string save_GUID = "#notSet";
    [SerializeField] string player_GUID = "#notSet";
    [SerializeField] public SystemLanguage languageChoice = SystemLanguage.Unknown;
    [SerializeField] public float options_globalVolume = 0.7f;
    [SerializeField] public bool options_isGameMuted = false;

    [Header("Player")]
    [SerializeField] public int tutorialComplete = -1;
    [SerializeField] public int moneyOwned = 0;
    [SerializeField] public int starsOwned = 0;
    [SerializeField] public int starsOnBounsChest = 0;

    [Header("Monetization")]
    [SerializeField] public bool hasRedeemedFreeStarChest = false;

    [Header("Progress")]
    [SerializeField] public int levelIndex = 0;
    [SerializeField] public int maxLevelBeaten = -1;
    [SerializeField] List<ExampleSaveData> example;

    public List<ExampleSaveData> Example
    {
        get
        {
            if (example == null)
                example = new List<ExampleSaveData>();
            return example;
        }
    }

    public void PrepareForSave(bool wipeProfile = false)
    {
        if (player_GUID == "#notSet")
            player_GUID = System.Guid.NewGuid().ToString();
        save_GUID = System.Guid.NewGuid().ToString();
        app_Version = Application.version;
    }
}

// How to use: 
// 1. ScriptableObject inherits from Saveable<T>
// 2. ScriptableObject overrides GetSaveList() and InitializeSaveData()
// 3. The save data inherits from SaveData
// 4. FetchSaveData() must be called in the ScriptableObject.

public class ExampleSaveable : Saveable<ExampleSaveData>
{
    public override List<ExampleSaveData> GetSaveList() => PlayerProfileHandler.Profile.Example;
    public override void InitializeSaveData(ExampleSaveData saveData)
    {
        base.InitializeSaveData(saveData);
        saveData.saveId = saveId;
    }
}

public class ExampleSaveData : SaveData
{
    public bool unlocked;
    public int levelReached;
}

//

public class Saveable<T> : ScriptableObject where T : SaveData, new()
{
    public int saveId;

    public virtual List<T> GetSaveList()
    {
        Debug.LogWarning("Warning: Using base method. Override this method with relevant list on child class.");
        return null;
    }

    public virtual void InitializeSaveData(T saveData)
    {
        saveData.saveId = saveId;
    }

    public T FetchSaveData()
    {
        T correspondingData = null;

        foreach (T savedData in GetSaveList())
        {
            bool isCorrespondingSaveData = (saveId == savedData.saveId);
            if (isCorrespondingSaveData)
            {
                correspondingData = savedData;
                return correspondingData;
            }
        }

        if (correspondingData == null)
        {
            correspondingData = new T();
            InitializeSaveData(correspondingData);
            GetSaveList().Add(correspondingData);
            return correspondingData;
        }

        return correspondingData;
    }
}

[System.Serializable]
public class SaveData
{
    public int saveId = -1;
}