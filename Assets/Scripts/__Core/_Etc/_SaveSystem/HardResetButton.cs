using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardResetButton : MonoBehaviour
{
    public void GoToResetScene()
    {
        Transform[] foundObjects = PersistentObjects.Instance.transform.GetComponentsInChildren<Transform>(true);// FindObjectsOfType<Transform>();

        List<Transform> rootDestructableObjects = new List<Transform>();

        foreach (Transform t in foundObjects)
            if (t.parent == null && t != transform)
                if (t.gameObject == PersistentObjects.Instance.gameObject || t.gameObject.scene.buildIndex != -1)
                    rootDestructableObjects.Add(t);

        foreach (Transform t in rootDestructableObjects)
            Destroy(t.gameObject);

        SaveSystem.WipeSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Instance.GetSceneNameByID(SceneNames.ScenesEnum.INGAME));

        //GameObject resetter = new GameObject();
        //resetter.AddComponent<HardReset>();
    }
}
