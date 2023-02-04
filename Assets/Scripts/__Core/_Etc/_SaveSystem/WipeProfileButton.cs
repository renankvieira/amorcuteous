using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeProfileButton : MonoBehaviour
{   
    public void Wipe()
    {
        SaveSystem.WipeSave();
        Utils.ReloadCurrentScene();
    }
}
