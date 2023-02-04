using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardReset : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ResetApp());
    }

    IEnumerator ResetApp()
    {
        yield return null;
        yield return null;
        System.GC.Collect();
        yield return null;
        yield return null;
        Resources.UnloadUnusedAssets();
        yield return null;
        yield return null;

        SceneManager.LoadScene(SceneNames.Instance.GetSceneNameByID(SceneNames.ScenesEnum.INGAME));
    }
}
