using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDuo;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LocalizeData2", menuName = "GameDuo/StaticData/Create LocalizeData2")]
[Serializable]
public class StaticLocalizeData2 : TextDataBase
{
    public List<LocalizeData2> datas;
    public override void LoadForTextData()
    {
        datas = new List<LocalizeData2>();
        TextAsset textAsset = Resources.Load<TextAsset>(TextDataPath);
        string[] lines = textAsset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            datas.Add(new LocalizeData2(lines[i].Split('\t')));
        }
    }
    public Dictionary<SystemLanguage, Dictionary<int, string>> LDIC, LDIC2;
    public void SettingDictionary()
    {
        LDIC = new Dictionary<SystemLanguage, Dictionary<int, string>>();
        LDIC.Add(SystemLanguage.Korean, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.English, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.Japanese, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.ChineseTraditional, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.ChineseSimplified, new Dictionary<int, string>());
        
        LDIC2 = new Dictionary<SystemLanguage, Dictionary<int, string>>();
        LDIC2.Add(SystemLanguage.Korean, new Dictionary<int, string>());
        LDIC2.Add(SystemLanguage.English, new Dictionary<int, string>());
        LDIC2.Add(SystemLanguage.Japanese, new Dictionary<int, string>());
        LDIC2.Add(SystemLanguage.ChineseTraditional, new Dictionary<int, string>());
        LDIC2.Add(SystemLanguage.ChineseSimplified, new Dictionary<int, string>());
        
        for (int i = 0; i < datas.Count; i++)
        {
            LDIC[SystemLanguage.Korean].Add(datas[i].key_index, datas[i].localize_value_kr.Replace(';', '\n'));
            LDIC[SystemLanguage.English].Add(datas[i].key_index, datas[i].localize_value_en.Replace(';', '\n'));
            LDIC[SystemLanguage.Japanese].Add(datas[i].key_index, datas[i].localize_value_ja.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseTraditional].Add(datas[i].key_index, datas[i].localize_value_tc.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseSimplified].Add(datas[i].key_index, datas[i].localize_value_sc.Replace(';', '\n'));

            LDIC2[SystemLanguage.Korean].Add(datas[i].key_index, datas[i].localize_value_kr2.Replace(';', '\n'));
            LDIC2[SystemLanguage.English].Add(datas[i].key_index, datas[i].localize_value_en2.Replace(';', '\n'));
            LDIC2[SystemLanguage.Japanese].Add(datas[i].key_index, datas[i].localize_value_ja2.Replace(';', '\n'));
            LDIC2[SystemLanguage.ChineseTraditional].Add(datas[i].key_index, datas[i].localize_value_tc2.Replace(';', '\n'));
            LDIC2[SystemLanguage.ChineseSimplified].Add(datas[i].key_index, datas[i].localize_value_sc2.Replace(';', '\n'));
        }
    }
    public string resourceName;
    public override void Load()
    {
        string[] lines = Resources.Load<TextAsset>(resourceName).text.Split('\n');
        datas = new List<LocalizeData2>();
        for (int i = 0; i < lines.Length; i++)
        {
            datas.Add(new LocalizeData2(lines[i].Split('\t')));
        }
        SettingDictionary();
    }
    public string GetLocalizeString(SystemLanguage language, int index)
    {
        try
        {
            if (LDIC.ContainsKey(language))
            {
                if (LDIC[language].ContainsKey(index))
                {
                    return LDIC[language][index];
                }
                else
                {
                    Debug.LogWarning("INDEX : " +index);
                }
            }
            else
            {
                Debug.LogWarning("LANGUAGE : " +language.ToString());
            }
            return string.Empty;
        }
        catch (Exception e)
        {
            Debug.LogWarning("LOCAL ERROR INDEX : " + language.ToString() + index);
            throw;
        }
    }
    public string GetLocalizeString2(SystemLanguage language, int index)
    {
        try
        {
            if (LDIC2.ContainsKey(language))
            {
                if (LDIC2[language].ContainsKey(index))
                {
                    return LDIC2[language][index];
                }
                else
                {
                    Debug.LogWarning("INDEX : " +index);
                }
            }
            else
            {
                Debug.LogWarning("LANGUAGE : " +language.ToString());
            }
            return string.Empty;
        }
        catch (Exception e)
        {
            Debug.LogWarning("LOCAL ERROR INDEX : " + language.ToString() + index);
            throw;
        }
    }
}

[Serializable]
public class LocalizeData2
{
    public int key_index;
    public string localize_value_kr;
    public string localize_value_kr2;
    public string localize_value_en;
    public string localize_value_en2;
    public string localize_value_ja;
    public string localize_value_ja2;
    public string localize_value_tc;
    public string localize_value_tc2;
    public string localize_value_sc;
    public string localize_value_sc2;

    public LocalizeData2(string[] words)
    {
        int index = 0;
        key_index = int.Parse(words[index++]);
        localize_value_kr = words[index++];
        localize_value_kr2 = words[index++];
        localize_value_en = words[index++];
        localize_value_en2 = words[index++];
        localize_value_ja = words[index++];
        localize_value_ja2 = words[index++];
        localize_value_tc = words[index++];
        localize_value_tc2 = words[index++];
        localize_value_sc = words[index++];
        localize_value_sc2 = words[index++];
    }
}