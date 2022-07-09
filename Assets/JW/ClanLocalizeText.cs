using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClanLocalizeText : MonoBehaviour
{
    public int keyIndex;
    private void Start()
    {
       GetComponent<Text>().text = LocalizeDataManager.GetClanLocalize(keyIndex).Replace("\\n", "\n");
    }
}
