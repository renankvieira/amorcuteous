using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Config")]
    [Range(0f, 10f)] public float lerpFactor = 0.8f;
    //public bool followCharacterRoot = true;
    public bool useLookAt = false;

    [Header("Control")]
    public Vector3 initialDelta;
    public Transform followTarget;
    public Vector3 desiredPosition;

    void Start()
    {
        if (followTarget == null)
        {
            followTarget = GameSceneContents.Instance.playerTransform;
            initialDelta = followTarget.position - transform.position;
        }
    }

    void LateUpdate()
    {
        if (followTarget == null)
        {
            followTarget = GameSceneContents.Instance.playerTransform;
            initialDelta = followTarget.position - transform.position;
        }

        if (followTarget != null)
        {
            desiredPosition = followTarget.position - initialDelta;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, lerpFactor * Time.deltaTime);

            Vector3 newPosition = transform.position;
            transform.position = newPosition;

            if (useLookAt)
                transform.LookAt(followTarget);
        }
    }
}
