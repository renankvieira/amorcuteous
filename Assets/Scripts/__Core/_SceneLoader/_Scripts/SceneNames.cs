using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNames : SingletonOfType<SceneNames>
{
    public List<SceneByID> sceneByIds;

    public enum ScenesEnum
    {
        NONE = 0,

        RESET_SCENE = 5,

        MAIN_MENU = 30,
        INGAME = 40
    }

    public string GetSceneNameByID(ScenesEnum id)
    {
        foreach (SceneByID sbid in sceneByIds)
            if (sbid.id == id)
                return sbid.sceneName;

        Debug.LogWarning("[SceneNames] Scene not found: " + id);
        return null;
    }

    [System.Serializable]
    public class SceneByID
    {
        public ScenesEnum id;
        public string sceneName = "nameNotSet";
    }
}
