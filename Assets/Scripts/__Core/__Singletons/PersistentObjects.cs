using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : SingletonOfType<PersistentObjects>
{
    public GameObject feedbackInput;

    override protected void Awake()
    {
        if (Instance == this)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (feedbackInput != null)
            feedbackInput.SetActive(DebugConfig.Instance.showFeedbackInput);
    }
}
