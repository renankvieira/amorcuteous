using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DebugSaveData : SingletonOfType<DebugSaveData>
{
    [Header("Debug")]
    public SaveableData saveDataWatch;

    void Start()
    {
        saveDataWatch = PlayerProfileHandler.Profile;
    }
}
