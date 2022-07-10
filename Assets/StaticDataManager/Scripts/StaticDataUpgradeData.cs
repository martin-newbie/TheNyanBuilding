using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDuo;
using System;
using UnityEditor;
using System.Text;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "GameDuo/StaticData/Create UpgradeData")]
[Serializable]
public class StaticDataUpgradeData : TextDataBase
{
    public List<UpgradeData> datas;

    public override void LoadForTextData()
    {

        TextAsset textAsset = Resources.Load<TextAsset>(TextDataPath);
        string[] lines = textAsset.text.Split('\n');

        datas = new List<UpgradeData>();
        for (int i = 0; i < lines.Length; i++)
        {
            //Debug.Log(lines[i]);
            datas.Add(new UpgradeData(lines[i].Split('\t')));
        }
    }

}
[Serializable]
public class UpgradeData
{
    public int key_index;
    public string name;
    public float value;
    public float valueIncrease;
    public int limitCount;
    public float cost;
    public float costIncrease;
    public UpgradeType upgradeType;
    public string upgradeTypeString;

    public UpgradeData(string[] words)
    {
        int tIndex = 0;
        key_index = int.Parse(words[tIndex++]);
        name = words[tIndex++];
        value = float.Parse(words[tIndex++]);
        valueIncrease = float.Parse(words[tIndex++]);
        limitCount = int.Parse(words[tIndex++]);
        cost = float.Parse(words[tIndex++]);
        costIncrease = float.Parse(words[tIndex++]);
        upgradeTypeString = words[tIndex++].TrimEnd();
        switch (upgradeTypeString)
        {
            case "rewardUP":
                upgradeType = UpgradeType.rewardUP;
                break;
            case "touchGaugeUP":
                upgradeType = UpgradeType.touchGaugeUP;
                break;
            case "tenGaugeUP":
                upgradeType = UpgradeType.tenGaugeUP;
                break;
            case "successRate":
                upgradeType = UpgradeType.successRate;
                break;
            case "failedRate":
                upgradeType = UpgradeType.failedRate;
                break;
            default:
                //Debug.Log("Input Error ( " + upgradeTypeString + " )");
                break;
        }
    }
}
