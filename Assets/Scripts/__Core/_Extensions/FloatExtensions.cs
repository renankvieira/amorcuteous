using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public static class FloatExtensions
{
    public static string ToTimeString(this float time, bool useDay = false, bool useMilisseconds = false)
    {
        float milisseconds = time % 1;
        int seconds = Mathf.FloorToInt(time % 60);
        int minutes = Mathf.FloorToInt(time % 3600);
        int hours = Mathf.FloorToInt(time % (3600 * 24));
        int days = Mathf.FloorToInt(time / 24);

        if (useDay)
        {
            if (useMilisseconds)
                return string.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:000}", days, hours, minutes, seconds, milisseconds);
            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", days, hours, minutes, seconds);
        }
        else
        {
            if (useMilisseconds)
                return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", hours, minutes, seconds, milisseconds);
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }

    public static string ToTimeString_MinutesAndSeconds(this float time, bool useMilisseconds = false)
    {
        float milisseconds = time % 1;
        int seconds = Mathf.FloorToInt(time % 60);
        int minutes = Mathf.FloorToInt(time / 60);

        if (useMilisseconds)
            return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milisseconds);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
