using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveSystem : MonoBehaviour {
    private static string config_SaveFileName = "saveData.txt";
    private static string config_SaveFileName_EDITOR = "saveData.txt"; //.json
    private static string config_WebDirectoryName = "RKV_TestSave";

    [DllImport("__Internal")] private static extern void SyncFiles();
    [DllImport("__Internal")] private static extern void WindowAlert(string message);

    private static bool initialized = false;
    private static bool IsWebPlayer
    {
        get
        {
            return Application.platform == RuntimePlatform.WebGLPlayer;
        }
    }

    private static string web_fullSavePath = "";
    private static string web_folderPath = "";
    private static string standAlone_fullSavePath = "";

    private static SaveableData _playerProfile;
    public static SaveableData PlayerProfile {
        get {
            if (!initialized)
                Initialize();

            //if (DebugConfig.Instance.useDebugSave)
            //    return DebugConfig.Instance.debugSave;
            return _playerProfile;
        }
        private set {
            _playerProfile = value;
        }
    }


    //TODO: The profile is being loaded twice on "LoadProfile" initial call. Once on Initialize(), once on LoadProfile().
    public static void Initialize()
    {
        if (initialized)
        {
            Debug.Log("[SaveSystem] Save system already initialized.");
            return;
        }

        PlatformSafeMessage("[SaveSystem] Initializing.");
        initialized = true;

        //PlatformSafeMessage("[SaveSystem] Is web player: " + IsWebPlayer);
        if (IsWebPlayer)
        {
            //web_fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + config_SaveFileName;
            web_folderPath = Path.DirectorySeparatorChar + "idbfs" + Path.DirectorySeparatorChar + config_WebDirectoryName + Path.DirectorySeparatorChar;
            web_fullSavePath = web_folderPath + config_SaveFileName;

#if UNITY_EDITOR
            web_fullSavePath = web_folderPath + config_SaveFileName_EDITOR;
#endif

            if (!Directory.Exists(web_folderPath))
            {
                PlatformSafeMessage("[SaveSystem] Creating idbfs directory.");
                Directory.CreateDirectory(web_folderPath);
            }
            LoadProfile();
        }
        else
        {
            standAlone_fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "saveData.txt";

#if UNITY_EDITOR
            standAlone_fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + config_SaveFileName_EDITOR;
#endif

            if (!File.Exists(standAlone_fullSavePath))
            {
                PlatformSafeMessage("[SaveSystem] File does not exists. Creating.");
                PlayerProfile = new SaveableData();
                SaveProfile();
            }
            else
            {
                LoadProfile();
            }
        }

    }

    static void LoadWebProfile()
    {
        if (!initialized)
            Initialize();

        try
        {
            PlatformSafeMessage("[SaveSystem] Starting file loading.");
            if (!File.Exists(web_fullSavePath))
            {
                PlatformSafeMessage("[SaveSystem] File does not exists. Creating.");
                PlayerProfile = new SaveableData();
                SaveProfile();
            }
            else
            {
                PlatformSafeMessage("[SaveSystem] Loading file.");
                PlayerProfile = JsonUtility.FromJson<SaveableData>(File.ReadAllText(web_fullSavePath));
            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("[SaveSystem] Failed to Load Profile: " + e.Message);
        }
    }

    static void LoadStandAloneProfile()
    {
        if (!initialized)
            Initialize();

        try
        {
            //PlatformSafeMessage("[SaveSystem] Loading file.");
            PlayerProfile = JsonUtility.FromJson<SaveableData>(File.ReadAllText(standAlone_fullSavePath));
        }
        catch (Exception e)
        {
            PlatformSafeMessage("[SaveSystem] Failed to Load Profile: " + e.Message);
        }

        /*
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(dataPath, FileMode.Open);

        gameDetails = (PlayerInfo)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        saveData = gameDetails;
        */
    }

    static void SaveWebProfile(bool wipeProfile = false)
    {
        try
        {
            PlatformSafeMessage("[SaveSystem] Saving profile.");

            PlayerProfile.PrepareForSave(wipeProfile);

            File.WriteAllText(web_fullSavePath, JsonUtility.ToJson(PlayerProfile, true));
            if (IsWebPlayer)
            {
                SyncFiles();
                //Debug.LogWarning("[SaveSystem] Saving disabled!");
            }
        }
        catch(Exception e)
        {
            PlatformSafeMessage("[SaveSystem] Failed to Save Profile: " + e.Message);
        }
    }

    static void SaveStandAloneProfile(bool wipeProfile = false)
    {
        PlatformSafeMessage("[SaveSystem] Saving profile.");
        //PlatformSafeMessage("[SaveSystem] Full save path: " + standAlone_fullSavePath);
        PlayerProfile.PrepareForSave(wipeProfile);
        File.WriteAllText(standAlone_fullSavePath, JsonUtility.ToJson(PlayerProfile, true));
    }

    static void LoadProfile()
    {
        if (IsWebPlayer)
        {
            LoadWebProfile();
        }
        else
        {
            LoadStandAloneProfile();
        }
    }

    public static void SaveProfile(bool wipeProfile = false)
    {
        if (IsWebPlayer)
        {
            SaveWebProfile(wipeProfile);
        }
        else
        {
            SaveStandAloneProfile(wipeProfile);
        }
    }

#if UNITY_EDITOR
    [MenuItem("CuteArmy/Wipe Save Data")]
#endif
    public static void WipeSave()
    {
        Debug.LogWarning("[SS] Wiping save.");

        PlayerProfile = new SaveableData();
        SaveProfile(true);
        initialized = false;
    }

#if UNITY_EDITOR
    [MenuItem("CuteArmy/Print Save Data")]
#endif
    public static void PrintSave()
    {
        PlatformSafeMessage(JsonUtility.ToJson(PlayerProfile));
    }

#if UNITY_EDITOR
    [MenuItem("CuteArmy/Delete Save Data")]
#endif
    public static void DeleteSave()
    {
        try
        {
            WipeSave();

            standAlone_fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "saveData.txt";

#if UNITY_EDITOR
            standAlone_fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + config_SaveFileName_EDITOR;
#endif

            File.Delete(standAlone_fullSavePath);
            Debug.LogWarning("[SaveSystem] Trying to delete save file.");
        }
        catch (Exception e)
        {
            Debug.LogWarning("[SaveSystem] Save file deletion fail: " + e.Message);
        }
        finally
        {
            Debug.LogWarning("[SaveSystem] Save file successfully deleted.");
        }
    }

#if UNITY_EDITOR
    [MenuItem("CuteArmy/Save Profile")]
#endif
    public static void SaveProfileOnEditor()
    {
        if (IsWebPlayer)
        {
            SaveWebProfile(false);
        }
        else
        {
            SaveStandAloneProfile(false);
        }
    }



    public static void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //WindowAlert(message);
            //Debug.LogWarning(message);
        }
        else
        {
            //Debug.LogWarning(message);
        }
    }
}