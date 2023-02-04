using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectGroup
{
    public GameObject[] gameObjects;

    public void ToggleAll(bool activate)
    {
        gameObjects.ToggleAll(activate);
    }
}

