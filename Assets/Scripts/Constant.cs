using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Constant : MonoBehaviour
{
    
    public static float FloatParse(string data)
    {
        return float.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
    }
    public static int IntParse(string data)
    {
        return int.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
    }
    
    public static T EnumParse<T>(string data) where T : Enum
    {
        return (T)Enum.Parse(typeof(T),data);
    }

    public static float[] FloatArrayParse(string data)
    {
        var words = data.Split(',');
        int amount = words.Length;
        var array = new float[amount];
        for (int i = 0; i < amount; i++)
        {
            array[i] = FloatParse(words[i]);
        }

        return array;
    }

    
    public static int[] IntArrayParse(string data)
    {
        var words = data.Split(',');
        int amount = words.Length;
        var array = new int[amount];
        for (int i = 0; i < amount; i++)
        {
            array[i] = IntParse(words[i]);
        }

        return array;
    }


}
