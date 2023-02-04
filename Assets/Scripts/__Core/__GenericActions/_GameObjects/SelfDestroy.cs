using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public bool autoDestroy = false;
    public float timeForAutoDestroy = 3f;

    void OnEnable()
    {
        if (autoDestroy)
        {
            Invoke("DestroySelf", timeForAutoDestroy);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
