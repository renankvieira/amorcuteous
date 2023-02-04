using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToScreen : GenericActionCaller
{
    [Header("FitToScreen")]
    public bool isZRotated = false;

    public override void MethodToCall()
    {
        base.MethodToCall();
        ResizeSpriteToScreen();
    }

    public void ResizeSpriteToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (!isZRotated)
        {
            float worldScreenHeight = Camera.main.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            transform.localScale = new Vector3(
                worldScreenWidth / sr.sprite.bounds.size.x,
                worldScreenHeight / sr.sprite.bounds.size.y,
                transform.localScale.z);
        }
        if (isZRotated)
        {
            float worldScreenHeight = Camera.main.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            transform.localScale = new Vector3(
                worldScreenWidth / sr.sprite.bounds.size.y,
                worldScreenHeight / sr.sprite.bounds.size.x,
                transform.localScale.z);
        }

    }



    //PerspectiveCamera
    //public SpriteRenderer spriteRenderer;
    //public Camera camera;

    //void OnGUI()
    //{
    //    float spriteHeight = spriteRenderer.sprite.bounds.size.y;
    //    float spriteWidth = spriteRenderer.sprite.bounds.size.x;
    //    float distance = transform.position.z - camera.transform.position.z;
    //    float screenHeight = 2 * Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * distance;
    //    float screenWidth = screenHeight * camera.aspect;
    //    transform.localScale = new Vector3(screenWidth / spriteWidth, screenHeight / spriteWidth, 1f);
    //}






}
