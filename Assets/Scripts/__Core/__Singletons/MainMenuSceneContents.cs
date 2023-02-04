using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneContents : SingletonOfType<MainMenuSceneContents>
{
    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

}
