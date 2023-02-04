using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SetVersionToText : MonoBehaviour
{
    [Header("Config")]
    public bool usePrepend = true;
    public string prependText = "Game Version ";

    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = usePrepend ? prependText + Application.version : Application.version;
    }
}
