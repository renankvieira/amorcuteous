using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCamera : MonoBehaviour
{
    [Header("Config")]
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public bool useSmoothDamp = false;
    [Range(0f,1f)] public float smoothDampFactor = 0.5f;

    [Header("Control")]
    Transform playerTransform;
    Vector3 initialDistance;

    void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        initialDistance = playerTransform.position - transform.position;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position - initialDistance;
            desiredPosition = desiredPosition.Clamp(minPosition, maxPosition);

            if (useSmoothDamp)
            {
                Vector3 outV3 = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref outV3, smoothDampFactor);
            }
            else
            {
                transform.position = desiredPosition;
            }
        }
    }
}
