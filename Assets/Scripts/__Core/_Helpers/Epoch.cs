 using UnityEngine;
 using System.Collections;
 using System;
 
 public static class Epoch
{

    public static int Current()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;

        return currentEpochTime;
    }

    //public static double CurrentDouble()
    //{
    //    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    //    double currentEpochTime = (DateTime.UtcNow - epochStart).TotalMilliseconds;

    //    return currentEpochTime;
    //}

    //public static void AsASD()
    //{
    //    DateTime departure = new DateTime(2010, 6, 12, 18, 32, 0);
    //    DateTime arrival = new DateTime(2010, 6, 13, 22, 47, 0);
    //    TimeSpan travelTime = arrival - departure;
    //    Debug.Log("travelTime: " + travelTime);
    //}


    public static int SecondsElapsedUntilNow(int t1)
    {
        int difference = Current() - t1;

        return Mathf.Abs(difference);
    }

    public static int SecondsElapsed(int t1, int t2)
    {
        int difference = t1 - t2;

        return Mathf.Abs(difference);
    }

}
