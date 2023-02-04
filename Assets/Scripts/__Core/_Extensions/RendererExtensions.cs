using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//forum.unity.com/threads/test-if-ui-element-is-visible-on-screen.276549/
//forum.unity.com/threads/overlay-canvas-and-world-space-coordinates.291108/

public static class RendererExtensions
{
    // Camera null for overlay canvases.
    private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera = null)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        Vector3 tempScreenSpaceCorner; // Cached
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            if (camera != null)
                tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            else
            {
                //Debug.Log(rectTransform.gameObject.name + " :: " + objectCorners[i].ToString("F2"));
                tempScreenSpaceCorner = objectCorners[i]; // If no camera is provided we assume the canvas is Overlay and world space == screen space
            }

            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
        }
        return visibleCorners;
    }

    // Camera null for overlay canvases.
    public static bool IsFullyVisibleFromV1(this RectTransform rectTransform, Camera camera = null)
    {
        if (!rectTransform.gameObject.activeInHierarchy)
            return false;

        return CountCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
    }

    // Camera null for overlay canvases.
    public static bool IsVisibleFromV1(this RectTransform rectTransform, Camera camera = null)
    {
        if (!rectTransform.gameObject.activeInHierarchy)
            return false;

        return CountCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
    }



    private static Vector3 GetGUIElementOffset(this RectTransform rect)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
        Vector3[] objectCorners = new Vector3[4];

        rect.GetWorldCorners(objectCorners);

        var xnew = 0f;
        var ynew = 0f;
        var znew = 0f;

        for (int i = 0; i < objectCorners.Length; i++)
        {
            if (objectCorners[i].x < screenBounds.xMin)
                xnew = screenBounds.xMin - objectCorners[i].x;
            if (objectCorners[i].x > screenBounds.xMax)
                xnew = screenBounds.xMax - objectCorners[i].x;
            if (objectCorners[i].y < screenBounds.yMin)
                ynew = screenBounds.yMin - objectCorners[i].y;
            if (objectCorners[i].y > screenBounds.yMax)
                ynew = screenBounds.yMax - objectCorners[i].y;
        }

        return new Vector3(xnew, ynew, znew);
    }

    public static bool IsFullyVisibleFromV2(this RectTransform rectTransform)
    {
        return rectTransform.GetGUIElementOffset().sqrMagnitude == 0f;
    }

    // Mine, temp
    public static bool IsRectTransformPositionVisible(this RectTransform rt, Camera camera)
    {
        Vector3 rectTransformPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, camera.nearClipPlane);
        return camera.ScreenToWorldPoint(rectTransformPosition).IsInsideCameraViewPort(camera);
    }

}
