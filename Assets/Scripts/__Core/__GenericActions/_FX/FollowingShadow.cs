using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class FollowingShadow : MonoBehaviour
{
    static Transform shadowsParent;
    static Vector3 nextPosition;

    [Header("Config")]
    public HeightSetupMethod followMethod = HeightSetupMethod.ONLY_ADDITIONAL_Y;
    public float additionalY = 0.25f;

    [Header("References")]
    public Transform followAnchor;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        if (shadowsParent == null)
            shadowsParent = (new GameObject("ShadowsParent")).transform;
        transform.parent = shadowsParent;
    }

    void LateUpdate()
    {
        nextPosition = followAnchor.position;
        if (followMethod == HeightSetupMethod.ONLY_ADDITIONAL_Y)
            nextPosition.y = additionalY;
        if (followMethod == HeightSetupMethod.FIXED_GROUND_HEIGHT)
            nextPosition.y = 0f + additionalY;
        transform.position = nextPosition;

        if (followAnchor.position.y < transform.position.y)
            transform.position = followAnchor.position;
    }

    public enum HeightSetupMethod
    {
        DEFAULT = 0,
        ONLY_ADDITIONAL_Y = 10,
        FIXED_GROUND_HEIGHT = 20,
        RAYCAST_FROM_ANCHOR = 30
    }
}
