using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleScaleIn : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool resetToZeroOnEnable = true;
    [SerializeField] private bool transitionInOnEnable = true;
    public bool useEasedCurve = true;
    public AnimationCurve curveLinear = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    public AnimationCurve curveEased = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private float inDelay = .5f;
    [SerializeField] private float inSpeed = 2f;

    [Header("Control")]
    public float progress = 0f;
    public Vector3 initialScale;
    public AnimationCurve usedCurve => useEasedCurve ? curveEased : curveLinear;

    void Awake()
    {
        initialScale = transform.localScale;
        //if (resetToZeroOnAwake)
        //    transform.localScale = Vector3.zero;
    }

    void OnEnable()
    {
        if (resetToZeroOnEnable)
            transform.localScale = Vector3.zero;
        if (transitionInOnEnable)
            ScaleIn();
    }

    public void ScaleIn()
    {
        StopCoroutine(TransitionIn());
        StartCoroutine(TransitionIn());
    }

    IEnumerator TransitionIn()
    {
        progress = 0f;

        Vector3 tempScale = Vector3.zero;
        //tempScale.y = initialScale.y * curve.Evaluate(progress);
        transform.localScale = tempScale;

        yield return new WaitForSeconds(inDelay);
        while (progress < 1f)
        {

            progress += Time.deltaTime * inSpeed;
            //tempScale = initialScale;
            tempScale.x = initialScale.x * usedCurve.Evaluate(progress);
            tempScale.y = initialScale.y * usedCurve.Evaluate(progress);
            tempScale.z = initialScale.z * usedCurve.Evaluate(progress);
            transform.localScale = tempScale;
            yield return new WaitForEndOfFrame();
        }
        progress = 1f;
        transform.localScale = initialScale;
    }
}