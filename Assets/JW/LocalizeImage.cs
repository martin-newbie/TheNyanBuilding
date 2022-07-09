using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeImage : MonoBehaviour
{
    public string key_string;
    private void Start()
    {
       GetComponent<Image>().sprite = LocalizeDataManager.GetSprite(key_string);
    }
}
