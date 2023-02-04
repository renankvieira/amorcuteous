using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : SingletonOfType<SimpleRotate>
{
    public float rotSpeed = 0f;
    public float maxSpeed = 60f;
    public float acel = 6f;


    public float rotationValue = 0f;

    public bool rotateSelf = false;

    private void Update()
    {
        rotSpeed += acel * Time.deltaTime;
        if (rotSpeed > maxSpeed)
            rotSpeed = maxSpeed;

        rotationValue += Time.deltaTime * rotSpeed;

        if (rotateSelf)
        {
            transform.Rotate(0f, 0, rotSpeed * Time.deltaTime);
        }
    }
}
