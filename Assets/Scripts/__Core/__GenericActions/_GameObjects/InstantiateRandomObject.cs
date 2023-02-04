using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRandomObject : GenericActionCaller
{
    [Header("Instantiate")]
    public GameObject[] possibleGameObjects;
    public Transform optionalParent;

    void Reset()
    {
        optionalParent = transform;
    }

    public override void MethodToCall()
    {
        base.MethodToCall();

        if (optionalParent)
            Instantiate(possibleGameObjects.GetRandom(), optionalParent);
        else
            Instantiate(possibleGameObjects.GetRandom());
    }
}
