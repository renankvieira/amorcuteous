using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotateAxis : MonoBehaviour
{
    [Header("Config")]
    public Vector3 rotationSpeed;
    public Vector3 maxSpeed;
    public Vector3 acel;
    public bool rotateRigidbody;

    [Header("References")]
    public Transform rotatingTransform;
    public Rigidbody rotatingRigidbody;

    [Header("Control")]
    public bool isActive = true;
    public Vector3 addedRotation;


    private void Update()
    {
        if (!isActive) return;
        if (rotateRigidbody) return;

        rotationSpeed.x += acel.x * Time.deltaTime;
        rotationSpeed.y += acel.y * Time.deltaTime;
        rotationSpeed.z += acel.z * Time.deltaTime;

        if (rotationSpeed.x > maxSpeed.x) rotationSpeed.x = maxSpeed.x;
        if (rotationSpeed.y > maxSpeed.y) rotationSpeed.y = maxSpeed.y;
        if (rotationSpeed.z > maxSpeed.z) rotationSpeed.z = maxSpeed.z;


        addedRotation = rotationSpeed * Time.deltaTime;
        rotatingTransform.Rotate(addedRotation);
    }

    void FixedUpdate()
    {
        if (!isActive) return;
        if (!rotateRigidbody) return;

        rotationSpeed.x += acel.x * Time.deltaTime;
        rotationSpeed.y += acel.y * Time.deltaTime;
        rotationSpeed.z += acel.z * Time.deltaTime;

        if (rotationSpeed.x > maxSpeed.x) rotationSpeed.x = maxSpeed.x;
        if (rotationSpeed.y > maxSpeed.y) rotationSpeed.y = maxSpeed.y;
        if (rotationSpeed.z > maxSpeed.z) rotationSpeed.z = maxSpeed.z;


        addedRotation = Time.deltaTime * rotationSpeed;

        //verificar esse script para rotar!
        //https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html

        //rotatingRigidbody.AddTorque(rotationSpeed * Time.deltaTime);



        //rb.AddTorque(transform.up * torque * turn);
        //rotatingRigidbody.MoveRotation(rotatingRigidbody.rotation + Quaternion.Euler(rotationSpeed * Time.deltaTime));
        //rotatingRigidbody.rotation = ;

    }

}
