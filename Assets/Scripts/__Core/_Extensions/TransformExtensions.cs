using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void ResetLocalValues (this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    public static void ResetGlobalValues (this Transform transform)
    {
        Transform previousParent = transform.parent;
        transform.parent = null;

        transform.ResetLocalValues();

        transform.parent = previousParent;
    }
}