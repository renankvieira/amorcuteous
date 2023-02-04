using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpriteAlphaInOut : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool includeChildren = true;
    [SerializeField] private bool includeInative = true;

    [Header("In")]
    [SerializeField] private float inSpeed = 2f;
    [SerializeField] private bool resetToZeroOnAwake = true;
    [SerializeField] private bool transitionInOnAwake = false;

    [Header("Out")]
    [SerializeField] private float outSpeed = 2f;
    [SerializeField] private bool hasExitTime = false;
    [SerializeField] private float exitTimeDelay = 2f;

    [Header("Control")]
    public float progress = 0f;
    public bool goingIn = false;
    public bool goingOut = false;
    List<SpriteData> datas;

    void Awake()
    {
        if (resetToZeroOnAwake)
        {
            SetAllToZero();
        }
        if (transitionInOnAwake)
        {
            StartCoroutine(AlphaIn());
        }
    }

    public void SetAllToZero()
    {
        progress = 0f;
        TryFetchComponents();
        SetColorOnComponents(0f);
    }

    void TryFetchComponents()
    {
        if (datas == null)
        {
            datas = new List<SpriteData>();

            if (includeChildren)
            {
                SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(includeInative);

                foreach (SpriteRenderer sr in spriteRenderers)
                {
                    SpriteData data = new SpriteData();
                    data.Initialize(sr);
                    datas.Add(data);
                }
            }
            else
            {
                SpriteData data = new SpriteData();
                data.Initialize(GetComponent<SpriteRenderer>());
                datas.Add(data);
            }
        }
    }

    void SetColorOnComponents(float progress_)
    {
        foreach (SpriteData data in datas)
        {
            data.currentColor = data.spriteRenderer.color;
            data.currentColor.a = data.startAlpha * progress_;
            data.spriteRenderer.color = data.currentColor;
        }
    }

    public IEnumerator AlphaIn()
    {
        if (goingIn) yield break;
        StopCoroutine(AlphaOut());

        gameObject.SetActive(true);

        goingIn = true;
        goingOut = false;
        progress = 0f;

        TryFetchComponents();
        SetColorOnComponents(0f);

        while (progress < 1f && goingIn)
        {
            progress += Time.deltaTime * inSpeed;
            SetColorOnComponents(progress);
            yield return new WaitForEndOfFrame();
        }
        progress = 1f;

        if (goingIn)
            SetColorOnComponents(1f);

        goingIn = false;

        if (hasExitTime)
        {
            yield return new WaitForSeconds(exitTimeDelay);
            StartCoroutine(AlphaOut());
        }
    }

    public IEnumerator AlphaOut(bool disableObjectOnEnd = true, Action callback = null)
    {
        if (goingOut) yield break;
        StopCoroutine(AlphaIn());

        goingIn = false;
        goingOut = true;
        progress = 1f;

        TryFetchComponents();
        SetColorOnComponents(1f);

        while (progress > 0f && goingOut)
        {
            progress -= Time.deltaTime * outSpeed;
            SetColorOnComponents(progress);
            yield return new WaitForEndOfFrame();
        }
        progress = 0f;

        if (goingOut)
            SetColorOnComponents(0f);

        goingOut = false;

        callback?.Invoke();

        if (disableObjectOnEnd)
            gameObject.SetActive(false);
    }

    [System.Serializable]
    public class SpriteData
    {
        public float startAlpha;
        public Color currentColor;
        public SpriteRenderer spriteRenderer;

        public void Initialize(SpriteRenderer sr)
        {
            startAlpha = sr.color.a;
            currentColor = sr.color;
            spriteRenderer = sr;
        }
    }

}
