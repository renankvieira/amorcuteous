using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

[DefaultExecutionOrder(-2000)]
public class PersistentObjectsCreator : MonoBehaviour
{
    public PersistentObjects prefab;
    void Awake()
    {
        if (PersistentObjects.Instance)
        {
            //Debug.LogWarning("[POC] PersistentObjects already exist.", this);
            Destroy(gameObject);
            return;
        }

        Instantiate(prefab);
    }
}
