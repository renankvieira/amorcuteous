using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

public class DebugManager : SingletonOfType<DebugManager>
{
    [Header("Config")]
    public bool isActive = true;

    void OnEnable()
    {
        if (!AppManager.Instance.IsOnEditor)
            isActive = false;
    }


    void Start()
    {
#if UNITY_EDITOR
        Debug.Log("[DM] Reading DEBUG settings...", DebugConfig.Instance);

        foreach (var field in DebugConfig.Instance.GetType().GetFields())
        {
            if (field.FieldType == typeof(bool))
                if ((bool)field.GetValue(DebugConfig.Instance) == true)
                    Debug.LogWarning("[DM] DEBUG FLAG SET: " + field.Name, DebugConfig.Instance);
            if (field.FieldType == typeof(int))
                if ((int)field.GetValue(DebugConfig.Instance) != 0)
                    Debug.LogWarning("[DM] DEBUG FLAG SET: " + field.Name + " = " + (int)field.GetValue(DebugConfig.Instance), DebugConfig.Instance);
        }

#endif
    }

    void Update()
    {
        if (!isActive)
            return;
#if UNITY_EDITOR
        bool shiftIsPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shiftIsPressed && Input.GetKeyDown(KeyCode.R))
            Utils.ReloadCurrentScene();

        if (shiftIsPressed && Input.GetKeyDown(KeyCode.P))
            Time.timeScale = Time.timeScale > 0f ? 0f : 1f;

        if (shiftIsPressed && Input.GetKeyDown(KeyCode.T))
            Time.timeScale = Mathf.Approximately(Time.timeScale, 1f) ? 0.05f : 1f;

        if (shiftIsPressed && Input.GetKeyDown(KeyCode.T))
            Time.timeScale = Mathf.Approximately(Time.timeScale, 1f) ? 10f : 1f;
#endif
    }

#if UNITY_EDITOR
    [MenuItem("CuteArmy/Select AppManager")]
    public static void SelectMSC()
    {
        Selection.activeGameObject = AppManager.Instance.gameObject;
    }

    //[MenuItem("CuteArmy/Select DebugConfig", priority = 10)]
    //public static void SelectDebugConfig()
    //{
    //    Selection.activeObject = DebugConfig.Instance;
    //}

#endif

}
