using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipByMovement : MonoBehaviour {

    public bool useThreshold = true;
    public float movementThreshold = 0.1f;

    Transform myTransform;
    Vector3 initialScale;

    Vector3 tempV3;

    float previousX;
    float currentX;

    private void Awake()
    {
        myTransform = transform;
        initialScale = myTransform.localScale;
        tempV3 = initialScale;
    }

    private void Update()
    {
        if (Mathf.Abs(currentX - previousX) < movementThreshold && useThreshold)
        {
            return;
        }

        currentX = myTransform.position.x;

        if (currentX > previousX)
        {
            myTransform.localScale = initialScale;
        }
        else if (currentX < previousX)
        {
            tempV3.x = initialScale.x * -1;
            myTransform.localScale = tempV3;
        }
    }

    private void LateUpdate()
    {
        previousX = myTransform.position.x;
    }
}
