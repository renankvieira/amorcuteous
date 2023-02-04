using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public static class StringExtensions
{
    public static string FirstLetterToUpper(this string str)
    {
        char[] charArray = str.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);

        for (int i = 1; i < charArray.Length; i++)
            charArray[i] = char.ToLower(charArray[i]);

        return new string(charArray);

        //if (str.Length > 1)
        //    return char.ToUpper(str[0]) + str.Substring(1);
        //return str.ToUpper();
    }

    public static string ToTitleCase(this string str)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }
}
