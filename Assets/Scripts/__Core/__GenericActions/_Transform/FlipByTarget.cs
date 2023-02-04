using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipByTarget : MonoBehaviour
{
    Transform myTransform;
    Vector3 originalScale;
    Vector3 currentScale;
    public Transform watchedObject;

    private void Awake()
    {
        myTransform = transform;
        originalScale = transform.localScale;
        currentScale = transform.localScale;
    }

    private void Update()
    {
        if (watchedObject.position.x > transform.position.x)
        {
            currentScale.x = originalScale.x;
        }
        else
        {
            currentScale.x = -originalScale.x;
        }
        transform.localScale = currentScale;
    }
}
