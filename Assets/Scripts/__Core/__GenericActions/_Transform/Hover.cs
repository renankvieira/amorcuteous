using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Vector3 RotateSpeed = new Vector3(10.0F, 10.0F, 10.0F);
    public Vector3 WobbleAmount = new Vector3(0.1F, 0.1F, 0.1F);
    public Vector3 WobbleSpeed = new Vector3(0.5F, 0.5F, 0.5F);

    private Transform tr;
    private Vector3 BasePosition;
    private Vector3 NoiseIndex = new Vector3();

    void Start()
    {

        // https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
        tr = GetComponent("Transform") as Transform;

        BasePosition = tr.position;

        NoiseIndex.x = Random.value;
        NoiseIndex.y = Random.value;
        NoiseIndex.z = Random.value;
    }

    void Update()
    {
        tr.Rotate(Time.deltaTime * RotateSpeed);

        Vector3 offset = new Vector3();
        offset.x = Mathf.PerlinNoise(NoiseIndex.x, 0) - 0.5F;
        offset.y = Mathf.PerlinNoise(NoiseIndex.y, 0) - 0.5F;
        offset.z = Mathf.PerlinNoise(NoiseIndex.z, 0) - 0.5F;

        offset.Scale(WobbleAmount);

        transform.position = BasePosition + offset;

        NoiseIndex += WobbleSpeed * Time.deltaTime;
    }
}