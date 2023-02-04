using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ImageFillFollowUpDown : MonoBehaviour
{
    [Header("Config")]
    public float lerpUpSpeed = 2f;
    public float lerpDownSpeed = 8f;

    [Header("References")]
    public Image sourceImage;
    public Image followerImage;

    void Update()
    {
        if (!Mathf.Approximately(sourceImage.fillAmount, followerImage.fillAmount))
        {
            if (sourceImage.fillAmount > followerImage.fillAmount)
                followerImage.fillAmount = Mathf.MoveTowards(followerImage.fillAmount, sourceImage.fillAmount, lerpUpSpeed * Time.deltaTime);
            else
                followerImage.fillAmount = Mathf.MoveTowards(followerImage.fillAmount, sourceImage.fillAmount, lerpDownSpeed * Time.deltaTime);

        }
    }
}
