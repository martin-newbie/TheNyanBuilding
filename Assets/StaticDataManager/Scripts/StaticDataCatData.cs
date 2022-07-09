using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDuo;
using System;
using UnityEditor;
using System.Text;

[CreateAssetMenu(fileName = "CatData", menuName = "GameDuo/StaticData/Create CatData")]
[Serializable]
public class StaticDataCatData : TextDataBase
{
    public List<CatData> datas;

    public override void LoadForTextData()
    {

        TextAsset textAsset = Resources.Load<TextAsset>(TextDataPath);
        string[] lines = textAsset.text.Split('\n');

        datas = new List<CatData>();
        for (int i = 0; i < lines.Length; i++)
        {
            //Debug.Log(lines[i]);
            datas.Add(new CatData(lines[i].Split('\t')));
        }
    }

}
[Serializable]
public class CatData
{
    public int key_index;
    public string name;
    public int type;
    public float[] devRates;
    public float[] rewardRates;
    public float speedRate;
    public int passive;
    public int[] passive_ranges;
    public float value;
    public float valueIncrease;
    public int grade;
    public float summonRate;
    public string content;
    public CatData(string[] words)
    {
        int tIndex = 0;
        key_index = int.Parse(words[tIndex++]);
        name = words[tIndex++];
        type = int.Parse(words[tIndex++]);
        devRates = Constant.FloatArrayParse(words[tIndex++]);
        rewardRates = Constant.FloatArrayParse(words[tIndex++]);
        speedRate = float.Parse(words[tIndex++]);
        passive = int.Parse(words[tIndex++]);
        passive_ranges = Constant.IntArrayParse(words[tIndex++]);
        value = float.Parse(words[tIndex++]);
        valueIncrease = float.Parse(words[tIndex++]);
        grade = int.Parse(words[tIndex++]);
        summonRate = float.Parse(words[tIndex++]);
        content = words[tIndex++];

    }
}
