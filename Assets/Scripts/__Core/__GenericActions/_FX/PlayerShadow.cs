using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [Header("Config")]
    public bool isActive = true;
    public Vector3 raycastDirection = Vector3.down;
    public LayerMask projectionLayerMask;
    public float groundSkinWidth = 0.05f;

    public bool changeShadowRotation = true;

    [Header("References")]
    public Transform raycastOrigin;
    public Transform shadowParent;

    [Header("Control")]

    Ray ray;
    RaycastHit hit;
    public RaycastHit Hit => hit;

    void Awake()
    {
        RoundEvents.Instance.onRoundOver += OnRoundOver;
    }

    void Update()
    {
        if (isActive)
            RunShadowProjection();
    }

    void RunShadowProjection()
    {
        ray = new Ray(raycastOrigin.position, raycastDirection);
        if (Physics.Raycast(ray, out hit, 100f, projectionLayerMask))
        {
            if (!shadowParent.gameObject.activeSelf)
                shadowParent.gameObject.SetActive(true);
            shadowParent.position = hit.point;
            shadowParent.position = Vector3.MoveTowards(shadowParent.position, raycastOrigin.position, groundSkinWidth);

            if (changeShadowRotation)
                shadowParent.transform.LookAt(shadowParent.transform.position + hit.normal);
        }
        else
        {
            if (shadowParent.gameObject.activeSelf)
                shadowParent.gameObject.SetActive(false);
        }
    }

    void OnRoundOver(bool gameWon)
    {
        if (!gameWon)
        {
            this.enabled = false;
            shadowParent.gameObject.SetActive(false);
        }
    }

}
