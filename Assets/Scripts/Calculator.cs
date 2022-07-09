using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public static Calculator Instance
    {
        get
        {
            return instance;
        }
    }
    private static Calculator instance;

    private void Awake()
    {
        if (instance!=null)
        {
            return;
        }
        instance = this;
    }


    string[] unit = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
         "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
          "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
          "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx", "cy", "cz",
          "da", "db", "dc", "dd", "de", "df", "dg", "dh", "di", "dj", "dk", "dl", "dm", "dn", "do", "dp", "dq", "dr", "ds", "dt", "du", "dv", "dw", "dx", "dy", "dz",
          "ea", "eb", "ec", "ed", "ee", "ef", "eg", "eh", "ei", "ej", "ek", "el", "em", "en", "eo", "ep", "eq", "er", "es", "et", "eu", "ev", "ew", "ex", "ey", "ez",
    };


    public string ConvertToString(double value)
    {
        if (double.IsPositiveInfinity(value))
            value = double.MaxValue;

        // Debug.Log(value);
        string result = "";
        if (value >= 1d)
        {
            int numDigit = (int)System.Math.Floor(System.Math.Log10(value));
            // Debug.Log(numDigit);
            int numDigitToChange = Mathf.FloorToInt(numDigit / 3f);
            value /= Pow(10d, numDigitToChange * 3);
            if (numDigitToChange > 0)
                result = value.ToString("F1") + unit[numDigitToChange];
            else
                result = value.ToString("N0");
        }
        else
            result = string.Format("{0:N1}", value);
        return result;
    }

    public string ConvertToString(float  value)
    {
        if (float.IsInfinity(value))
            value = float.MaxValue;

        string result = "";
        if (value >= 1f)
        {
            int numDigit = Mathf.FloorToInt(Mathf.Log10(value));
            int numDigitToChange = Mathf.FloorToInt(numDigit / 3f);
            value /= (Mathf.Pow(10, numDigitToChange * 3));
            if (numDigitToChange > 0)
                result = value.ToString("F1") + unit[numDigitToChange];
            else
                result = value.ToString("N0");
        }
        else
            result = string.Format("{0:N1}", value);
        return result;
    }
    

    public double Pow(double a, double b)
    {
        double result = 1;
        for (int i = 0; i < b; i++)
        {
            result *= a;
        }
        return result;
    }
}
