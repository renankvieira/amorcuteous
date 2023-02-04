using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHardCopy : MonoBehaviour
{
    /// <summary>
    /// How to use:
    /// 1. Create the new object (the Copy) and attach this component to it.
    /// 2. In the inspector, set the transform to be copied (the originalTransform) and press CopyNow.
    /// 3. If desired, manually move the children from the originalTransform to the Copy.
    /// </summary>

    [Header("References")]
    public Transform originalTransform;

    [Header("Config")]
    public bool copyPosition = true;
    public bool copyRotation = true;
    public bool copyScale = true;
    public bool copyName = true;
    public bool justCopyLocalValues = false;

    void LateUpdate()
    {
        Copy();
    }

    public void Copy()
    {
        // Another option is to, in each frame, (1) make the Target child to the Original, (2) reset the transform components and (3) put the Target back where it was.
        //if (copyPosition)
        //    transform.position = originalTransform.position;
        //if (copyRotation)
        //    transform.rotation = originalTransform.rotation;
        //if (copyScale)
        //{
        //    transform.localScale = Vector3.one;
        //    var m = transform.worldToLocalMatrix;
        //    m.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
        //    transform.localScale = m.MultiplyPoint(originalTransform.lossyScale);
        //    //targetTransform.lossyScale = originalTransform.lossyScale;

        //    trans.localScale = Vector3.one;
        //    var m = trans.worldToLocalMatrix;
        //    m.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
        //    trans.localScale = m.MultiplyPoint(Scale);
        //}

        if (justCopyLocalValues)
        {
            if (copyPosition)
                transform.localPosition = originalTransform.localPosition;
            if (copyRotation)
                transform.localRotation = originalTransform.localRotation;
            if (copyScale)
                transform.localScale = originalTransform.localScale;
        }
        else
        {
            int previousSiblingIndex = transform.GetSiblingIndex();
            Transform previousParent = transform.parent;
            transform.parent = originalTransform;

            if (copyPosition)
                transform.localPosition = Vector3.zero;
            if (copyRotation)
                transform.localRotation = Quaternion.identity;
            if (copyScale)
                transform.localScale = Vector3.one;

            transform.parent = previousParent;
            transform.SetSiblingIndex(previousSiblingIndex);
        }
    }

    [Button]
    public void CopyNow()
    {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(transform, "[THC] Transform copy: " + gameObject.name);
        Copy();

        if (copyName)
        {
            UnityEditor.Undo.RecordObject(gameObject, "[THC] Name copy: " + gameObject.name);
            gameObject.name = originalTransform.gameObject.name + " (copy)";
        }
#endif
    }

}
