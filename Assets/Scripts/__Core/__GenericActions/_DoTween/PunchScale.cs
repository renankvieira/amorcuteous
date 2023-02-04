using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

public class PunchScale : MonoBehaviour
{
    /*
    [Header("Config")]
    public bool autorun = true;
    public bool repeat = true;
    public Vector3 punch = Vector3.one;
    public float duration = 1;
    public int vibrato = 0;
    public float elasticity = 0;
    //public bool resetScaleOnEnable = true;

    [Header("Timers")]
    public float timeBeforeFirst = 0f;
    public float timeBetween = 3f;

    [Header("Debug")]
    public Vector3 initialScale = Vector3.one; 

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        if (autorun)
            StartCoroutine(PunchCoroutine());
    }

    public void Punch()
    {
        transform.DOKill();
        transform.localScale = initialScale;
        StartCoroutine(PunchCoroutine());
    }

    IEnumerator PunchCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeFirst);

        do {
            DoPunch();
            yield return new WaitForSeconds(timeBetween);
        } while (true && repeat);
    }

    public void DoPunch()
    {
        transform.DOPunchScale(punch, duration, vibrato, elasticity);
    }

    void OnDisable()
    {
        if (transform != null)
            transform.DOKill();
        transform.localScale = initialScale;
    }

    [Button]
    public void DebugPunch()
    {
        Punch();
    }
    */
}
