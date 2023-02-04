using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Config")]
    public bool destroyOnParentKill = false;
    public float followSpeed = float.PositiveInfinity;

    [Header("References")]
    public Transform followTarget;

    [Header("Control")]
    public Vector3 initialDelta;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        initialDelta = followTarget.position - transform.position;
    }

    void LateUpdate()
    {
        if (followTarget != null)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, followTarget.position - initialDelta, followSpeed * Time.deltaTime);
        }
        else
        {
            if (destroyOnParentKill)
                Destroy(gameObject);
        }
    }
}
