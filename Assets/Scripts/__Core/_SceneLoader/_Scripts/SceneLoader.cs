using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : SingletonOfType<SceneLoader>
{
    [Header("Config")]
    public bool transitionOnAwake = true;

    [Header("References")]
    public GameObject canvasParent;
    public Animator anim;

    [Header("Control")]
    public bool transitioning = false;
    public string desiredScene;

    [Header("Debug")]
    float sceneLoadStart = 0f;

    override protected void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (transitionOnAwake)
        {
            transitioning = true;
            OnSceneIsLoaded();
        }
        canvasParent.SetActive(true);
    }

    public void LoadSceneByName(string sceneName)
    {
        if (transitioning)
        {
            Debug.Log("[SL] Already transitioning. Cannot load scene.");
            return;
        }
        transitioning = true;

        desiredScene = sceneName;

        anim.SetTrigger("ExittingScene");

        sceneLoadStart = Time.realtimeSinceStartup;
    }

    public void OnEnteringIsOver()
    {
        transitioning = false;

        //Debug.Log("[SL] Load over: " + Time.realtimeSinceStartup);
        //this.DelayedInvoke(() =>
        //    Debug.LogFormat("[SL] Scene load duration: {0}, {1}", desiredScene, (Time.realtimeSinceStartup - sceneLoadStart - .25f).ToString("N2")),
        //    .25f);
        //this.DelayedInvoke(() => {
        //    GameAnalyticsManager.Instance.OnSceneTransitionOver(desiredScene, (Time.realtimeSinceStartup - sceneLoadStart - .25f).ToString("N2"));
        //}, .25f);
    }

    public void OnExittingIsOver()
    {
        //Debug.Log("[SceneLoader] Exitting over.");
        if (desiredScene == "")
        {
            Debug.LogWarning("[SL] No scene to load.");
            return;
        }

        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        float startTime = Time.time;
        //GameAnalyticsManager.Instance.RegisterDesignEvent("SceneTransition", "01 - Start", desiredScene);
        yield return null;
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(desiredScene);
        while (!asyncOperation.isDone)
            yield return null;

        OnSceneIsLoaded();
    }

    void OnSceneIsLoaded()
    {
        anim.SetTrigger("EnteringScene");

        desiredScene = "";
    }
}