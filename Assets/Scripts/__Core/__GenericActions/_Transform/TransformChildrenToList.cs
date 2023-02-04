using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChildrenToList : GenericActionCaller
{
    [Header("ChildrenToList")]
    public List<Transform> children;
    public bool addInactive = false;

    public override void MethodToCall()
    {
        base.MethodToCall();
        AddChildrenToList();
    }

    public void AddChildrenToList()
    {
        if (children == null)
            children = new List<Transform>();
        else
            children.Clear();

        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).gameObject.activeInHierarchy || addInactive)
                children.Add(transform.GetChild(i));
    }

    public int GetIndexByTransform(Transform transform_)
    {
        for (int i = 0; i < children.Count; i++)
            if (children[i] == transform_)
                return i;

        Debug.LogWarning("[IRBPP] patrolPoint not found: " + transform_.name, gameObject);
        return -1;
    }

    public Transform GetTransformByIndex(int index)
    {
        return children[index];
    }

    public Transform GetNextTransform(int currentIndex)
    {
        int nextIndex = currentIndex + 1;
        nextIndex %= children.Count;
        return children[nextIndex];
    }

    public int GetNextIndex(int currentIndex)
    {
        int nextIndex = currentIndex + 1;
        nextIndex %= children.Count;
        return nextIndex;
    }



}
