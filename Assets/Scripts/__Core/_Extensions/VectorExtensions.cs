using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public static class VectorExtensions
{
    public static Vector3 XYtoXZ(this Vector3 v3)
    {
        v3.z = v3.y;
        v3.y = 0f;
        return v3;
    }

    public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        return vector;
    }

    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        vector.z = Mathf.Clamp(vector.z, min.z, max.z);
        return vector;
    }

    public static Vector3 AddRandom(this Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x += UnityEngine.Random.Range(min.x, max.x);
        vector.y += UnityEngine.Random.Range(min.y, max.y);
        vector.z += UnityEngine.Random.Range(min.z, max.z);
        return vector;
    }
}
