using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MoveToCanvasPosition : GenericActionCaller
{
    [Header("Config")]
    public bool invertCoordinates = false;
    public Vector2 canvasPositionToUse = new Vector2(0.20f, 0.13f);
    public Vector3 additionalVector = new Vector3(0f, 0f, 10f);

    [Header("Refs")]
    public Camera cam;

    [Header("Control")]
    public Vector3 worldPosition;

    public override void MethodToCall()
    {
        base.MethodToCall();
        SetPosition();
    }

    public void SetPosition()
    {
        if (cam == null)
            cam = Camera.main;

        Vector2 pixelPosition = new Vector2();
        pixelPosition.x = canvasPositionToUse.x * Screen.width;
        pixelPosition.y = canvasPositionToUse.y * Screen.height;

        if (invertCoordinates)
        {
            pixelPosition.x = (1f - canvasPositionToUse.x) * Screen.width;
            pixelPosition.y = (1f - canvasPositionToUse.y) * Screen.height;
        }

        worldPosition = cam.ScreenToWorldPoint(pixelPosition);
        transform.position = worldPosition + additionalVector;
    }
}
