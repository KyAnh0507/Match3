using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculater
{
    public static string CalculaterTime(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);

        string t = "";
        t += minute.ToString("D2") + ":";
        t += second.ToString("D2");
        return t;
    }
}
