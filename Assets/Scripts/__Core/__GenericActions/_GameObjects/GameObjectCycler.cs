using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCycler : GenericActionCaller
{
    [Header("Cycler")]
    public Behavior behavior = Behavior.INSTANTIATE;
    public GameObject[] possibleGameObjects;
    public bool disableOthers = true;
    public int startIndex = -1;

    public override void MethodToCall()
    {
        Debug.LogWarning("[GOC] Component not ready! DO NOT USE!", gameObject);

        base.MethodToCall();

        GameObject chosenObject = possibleGameObjects.GetRandom();

        foreach (GameObject go in possibleGameObjects)
            go.SetActive(chosenObject == go);
    }

    public enum Behavior
    {
        INSTANTIATE = 10,
        ACTIVATE = 20
    }
}
