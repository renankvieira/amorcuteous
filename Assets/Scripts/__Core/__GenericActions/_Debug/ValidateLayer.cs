using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateLayer : MonoBehaviour
{
    //Unchanged.
    public string desiredLayer;

    void Awake()
    {
        Transform[] test = transform.GetComponentsInChildren<Transform>();

        foreach(Transform t in test)
        {
            if (t.gameObject.layer != LayerMask.NameToLayer(desiredLayer))
                Debug.LogWarningFormat(t.gameObject, "[VL] Warning! Different layer: {0} not on {1}", t.gameObject.name, desiredLayer);
        }
    }
}
