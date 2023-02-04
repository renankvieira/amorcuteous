using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class CuteArmyWindow : EditorWindow
{
    [MenuItem("CuteArmy/CuteArmyWindow")]
    static void Init()
    {
        CuteArmyWindow window = (CuteArmyWindow)EditorWindow.GetWindow(typeof(CuteArmyWindow));
        window.Show();
    }

    bool groupEnabled_save;
    bool groupEnabled_select;
    bool groupEnabled_load;
    bool groupEnabled_etc;


    void OnGUI()
    {
        groupEnabled_save = EditorGUILayout.BeginFoldoutHeaderGroup(groupEnabled_save, "Save");
        if (groupEnabled_save)
        {
            if (GUILayout.Button("Delete Save"))
                SaveSystem.DeleteSave();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        groupEnabled_select = EditorGUILayout.BeginFoldoutHeaderGroup(groupEnabled_select, "SELECT");
        if (groupEnabled_select)
        {
            if (GUILayout.Button("AppManager"))
                Selection.activeGameObject = AppManager.Instance.gameObject;
            if (GUILayout.Button("DebugConfig"))
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_Settings/DebugConfig.asset");
            if (GUILayout.Button("GameConfig"))
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_Settings/GameConfig.asset");

            //if (GUILayout.Button("Select RoundController"))
            //    Selection.activeGameObject = RoundController.Instance.gameObject;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        groupEnabled_load = EditorGUILayout.BeginFoldoutHeaderGroup(groupEnabled_load, "LOAD");
        if (groupEnabled_load)
        {
            if (GUILayout.Button("Main Menu Scene"))
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/MainMenuScene.unity");
            if (GUILayout.Button("Game Scene"))
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/_Scenes/ToyScene.unity");

            //if (GUILayout.Button("Select RoundController"))
            //    Selection.activeGameObject = RoundController.Instance.gameObject;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        groupEnabled_etc = EditorGUILayout.BeginFoldoutHeaderGroup(groupEnabled_etc, "Etc");
        if (groupEnabled_etc)
        {
            if (GUILayout.Button("TimeScale 1 > 0.1 > 0.01"))
            {
                if (Mathf.Approximately(Time.timeScale, 1f))
                    Time.timeScale = 0.1f;
                else if (Mathf.Approximately(Time.timeScale, 0.1f))
                    Time.timeScale = 0.01f;
                else
                {
                    Time.timeScale = 1f;
                    Debug.Log("[CPW] Time.timeScale reset.");
                }
            }

            if (GUILayout.Button("TimeScale 1 > 10 > 100"))
            {
                if (Mathf.Approximately(Time.timeScale, 1f))
                    Time.timeScale = 10f;
                else if (Mathf.Approximately(Time.timeScale, 10f))
                    Time.timeScale = 100f;
                else
                {
                    Time.timeScale = 1f;
                    Debug.Log("[CPW] Time.timeScale reset.");
                }
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
#endif