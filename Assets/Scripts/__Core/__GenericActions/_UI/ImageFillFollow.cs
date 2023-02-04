using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ImageFillFollow : MonoBehaviour
{
    [Header("Config")]
    public float lerpSpeed = 2f;

    [Header("References")]
    public Image sourceImage;
    public Image followerImage;

    void Update()
    {
        if (!Mathf.Approximately(sourceImage.fillAmount, followerImage.fillAmount))
            followerImage.fillAmount = Mathf.MoveTowards(followerImage.fillAmount, sourceImage.fillAmount, lerpSpeed * Time.deltaTime);
    }
}