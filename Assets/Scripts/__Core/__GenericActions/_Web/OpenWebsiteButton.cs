using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsiteButton : MonoBehaviour
{
    public enum SocialMediaName
    {
        DEFAULT = 0,
        INSTAGRAM = 10,
        TWITTER = 20,
        DISCORD = 30
    }

    public SocialMediaName socialNetworkName; 
    string linkToOpen;

    void Awake()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenWebsite);
    }

    public void OpenWebsite()
    {
        if (socialNetworkName == SocialMediaName.INSTAGRAM)
        {
            linkToOpen = "https://www.instagram.com/cutearmygame/";
            //linkToOpen = "https://bit.ly/CA_ACS_FB";
        }
        if (socialNetworkName == SocialMediaName.TWITTER)
        {
            linkToOpen = "https://twitter.com/CuteArmyGame";
        }
        if (socialNetworkName == SocialMediaName.DISCORD)
        {
            linkToOpen = "https://bit.ly/cutearmy-discord";
        }

        Application.OpenURL(linkToOpen);

    }
}


