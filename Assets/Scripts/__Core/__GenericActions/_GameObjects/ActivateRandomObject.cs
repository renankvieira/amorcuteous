using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomObject : GenericActionCaller
{
    [Header("Activate")]
    public GameObject[] possibleGameObjects;
    public bool disableOthers = true;

    public override void MethodToCall()
    {
        base.MethodToCall();

        GameObject chosenObject = possibleGameObjects.GetRandom();

        foreach (GameObject go in possibleGameObjects)
            go.SetActive(chosenObject == go);
    }
}
