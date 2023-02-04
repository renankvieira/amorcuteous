using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    [System.Serializable]
    public struct MinMaxColor
    {
        [ColorUsage(true, true)] public Color Min;
        [ColorUsage(true, true)] public Color Max;

        public Color GetValueFromRatio(float ratio)
        {
            return Color.Lerp(Min, Max, ratio);
        }
    }

[System.Serializable]
public struct MinMaxVector3
{
    public Vector3 Min;
    public Vector3 Max;

    public Vector3 GetValueFromRatio(float ratio)
    {
        return Vector3.Lerp(Min, Max, ratio);
    }
}
*/


[System.Serializable]
public class MinMax
{
    public float min = 0f;
    public float max = 1f;

    bool draftRan = false;
    float draftedNumber = 0f;

    public float ClampToMin(float v)
    {
        bool crescent = min < max;
        if (crescent)
        {
            if (v < min) return min;
            else return v;
        }
        else
        {
            if (v < max) return max;
            else return v;
        }
    }

    public float ClampToMax(float v)
    {
        bool crescent = min < max;
        if (crescent)
        {
            if (v < max) return max;
            else return v;
        }
        else
        {
            if (v < min) return min;
            else return v;
        }

    }

    public bool IsValueBetween(float v)
    {
        if (v < min) return false;
        if (v > max) return false;
        return true;
    }

    public float NewRandomValueInside()
    {
        return Random.Range(min, max);
    }

    public float GetMappedValueFromPercentile(float percentile, bool clampToMin = true, bool clampToMax = true)
    {
        float range = max - min;
        float mappedValue = min + (percentile * range);

        if (clampToMin)
            ClampToMin(mappedValue);
        if (clampToMax)
            ClampToMax(mappedValue);

        //Debug.LogFormat("Percentile {0}, Range {1}, final {2}", percentile, range, mappedValue);
        return mappedValue;
    }

    public float SameValueInside()
    {
        if (!draftRan)
        {
            draftedNumber = NewRandomValueInside();
            draftRan = true;
        }
        return draftedNumber;
    }
}
