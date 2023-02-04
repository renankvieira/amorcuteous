using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHolder : MonoBehaviour
{
    public Animator animatorComponent;

    void Awake()
    {
        //RoundEvents.Instance.onRoundOver += OnRoundOver;
    }

    void OnRoundOver(bool gameWon)
    {
        if (gameWon)
        {
            animatorComponent.SetTrigger("onGameWon");
        }
        else
        {
            animatorComponent.SetTrigger("onGameLost");
        }
    }

}
