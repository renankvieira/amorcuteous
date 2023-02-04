using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRParallax : MonoBehaviour
{
    [Header("Config")]
    public bool getSpriteRenderersOnAwake = false;
    public bool runInUpdate = false;
    public bool computeEachChildenIndividually = false;

    public Transform groundAnchor;
    public float zGain = 0;

    [Header("Watch")]
    public SpriteRenderer[] spriteRenderers;
    public List<RendererData> rendererDatas;


    [System.Serializable]
    public class RendererData
    {
        public SpriteRenderer spriteRenderer;
        public int originalOrder = 0;
    }

    private void Awake()
    {
        if (getSpriteRenderersOnAwake)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            RendererData newRendererData = new RendererData();
            newRendererData.spriteRenderer = spriteRenderers[i];
            newRendererData.originalOrder = spriteRenderers[i].sortingOrder;
            rendererDatas.Add(newRendererData);
        }

        if (groundAnchor == null)
        {
            groundAnchor = transform;
        }

        SetOrder();

        if (!runInUpdate)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        SetOrder();
    }

    void SetOrder()
    {
        //int order = groundAnchor != null ? (int)(groundAnchor.position.z * 100) : 0;
        //int order = -(int)((groundAnchor.position.z + zGain) * 100);

        for (int i = 0; i < rendererDatas.Count; i++)
        {
            int order = -(int)((groundAnchor.position.z + zGain) * 1000);

            if (computeEachChildenIndividually)
                order = -(int)((rendererDatas[i].spriteRenderer.transform.position.z + zGain) * 1000);

            order += rendererDatas[i].originalOrder;

            rendererDatas[i].spriteRenderer.sortingOrder = order;
        }
    }
}
