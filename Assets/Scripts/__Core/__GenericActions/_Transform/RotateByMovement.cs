using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMovement : MonoBehaviour
{
    [Header("Config")]
    public bool isActive = true;
    public float rotationSpeed = 1080;
    public bool ignoreY = true;
    public float deltaMagnitudeSquaredThreshold = 0.001f;
    public Vector3 upwards = Vector3.up;

    [Header("References")]
    public Transform rotatable;

    Vector3 previousPosition = Vector3.zero;

    void Start()
    {
        if (rotatable == null)
            rotatable = transform;
        previousPosition = transform.position;
    }

    void Update()
    {
        if (!isActive)
            return;

        Vector3 deltaPosition = transform.position - previousPosition;
        if (ignoreY)
            deltaPosition.y = 0f;

        if (deltaPosition.sqrMagnitude < deltaMagnitudeSquaredThreshold)
            return;

        if (deltaPosition.sqrMagnitude != 0f)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(deltaPosition, upwards);
            Quaternion newRotation = Quaternion.RotateTowards(rotatable.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            rotatable.rotation = newRotation;
        }

        previousPosition = transform.position;
    }

    //void LateUpdate()
    //{
    //}
}
