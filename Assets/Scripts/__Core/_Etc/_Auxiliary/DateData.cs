using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateData
{
    public int day = 1, month = 1, year = 1980;
    public int hour = 0, minute = 0, second = 0;

    //public void SetTime(int day_, int month_, int year_, int hour_, int minute_, int second_)
    //{
    //    day = day_;
    //    month = month_;
    //    year = year_;
    //    hour = hour_;
    //    minute = minute_;
    //    second = second_;
    //}

    public void SetTime(System.DateTime time)
    {
        year = time.Year;
        month = time.Month;
        day = time.Day;
        hour = time.Hour;
        minute = time.Minute;
        second = time.Second;
    }

    public System.DateTime ToDateTime()
    {
        return new System.DateTime(year, month, day, hour, minute, second);
    }

    public bool IsAfter(DateData other)
    {
        //System.DateTime dt1;
        //System.DateTime dt2;
        //dt1 = new System.DateTime(year, month, day, hour, minute, second);
        //dt2 = new System.DateTime(other.year, other.month, other.day, other.hour, other.minute, other.second);

        //int comparisonResult = dt1.CompareTo(dt2);
        //System.TimeSpan difference = dt2 - dt1;

        if ((year > other.year)) return true;
        if ((year == other.year) && (month > other.month)) return true;
        if ((year == other.year) && (month == other.month) && (day > other.day)) return true;
        if ((year == other.year) && (month == other.month) && (day == other.day) && (hour > other.hour)) return true;
        if ((year == other.year) && (month == other.month) && (day == other.day) && (hour == other.hour) && (minute > other.minute)) return true;
        if ((year == other.year) && (month == other.month) && (day == other.day) && (hour == other.hour) && (minute == other.minute) && (second > other.second)) return true;
        return false;
    }

    public bool IsTheSame(DateData other)
    {
        if ((year == other.year) && (month == other.month) && (day == other.day) && (hour == other.hour) && (minute == other.minute) && (second == other.second))
            return true;
        return false;
    }

    //public void AddDateData(DateData additional)
    //{
    //    year += additional.year;
    //    month += additional.year;
    //}

}