using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyUiReloadButton : MonoBehaviour
{
    //[Header("Config")]
    //[Header("References")]
    //[Header("Control")]
    //[Header("Debug")]

    void Awake()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Utils.ReloadCurrentScene();
    }
}
