using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCamera : MonoBehaviour
{
    Transform playerTransform;

    Vector3 initialDistance;

    public Vector3 minPosition;
    public Vector3 maxPosition;

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

            transform.position = desiredPosition;
            //SmoothDamp
        }
    }
}
