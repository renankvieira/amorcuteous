using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMaxInt
{
    public int min;
    public int max;

    bool draftRan = false;
    int draftedNumber = 0;

    public int ClampToMin(int v)
    {
        if (v < min) return min;
        else return v;
    }

    public float ClampToMax(int v)
    {
        if (v > max) return max;
        else return v;
    }

    public bool IsValueBetween(float v)
    {
        if (v < min) return false;
        if (v > max) return false;
        return true;
    }

    public int RandomValueInside()
    {
        return Random.Range(min, max);
    }

    public int GetDraftedNumber()
    {
        if (!draftRan)
        {
            draftedNumber = RandomValueInside();
            draftRan = true;
        }
        return draftedNumber;
    }

}
