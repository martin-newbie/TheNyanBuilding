using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDuo;
using System;
using UnityEngine.Events;
using System.IO;

[CreateAssetMenu(fileName = "LocalizeData", menuName = "GameDuo/StaticData/Create LocalizeData")]
[Serializable]
public class StaticLocalizeData : TextDataBase
{
    public List<LocalizeData> datas;
    public override void LoadForTextData()
    {
        datas = new List<LocalizeData>();
        TextAsset textAsset = Resources.Load<TextAsset>(TextDataPath);
        string[] lines = textAsset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            datas.Add(new LocalizeData(lines[i].Split('\t')));
        }
        JsonSave();
    }
    public void JsonSave()
    {
        FirebaseLocalizeData firebaseLocalizeData = new FirebaseLocalizeData();
        firebaseLocalizeData.datas = this.datas;
        string path = Path.Combine(Application.streamingAssetsPath, "LocalizeData.json");

        string json = JsonUtility.ToJson(firebaseLocalizeData);
        File.WriteAllText(path, json);
    }

    public Dictionary<SystemLanguage, Dictionary<int, string>> LDIC;
    public void SettingDictionary()
    {
        LDIC = new Dictionary<SystemLanguage, Dictionary<int, string>>();
        LDIC.Add(SystemLanguage.Korean, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.English, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.Japanese, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.ChineseTraditional, new Dictionary<int, string>());
        LDIC.Add(SystemLanguage.ChineseSimplified, new Dictionary<int, string>());

        for (int i = 0; i < datas.Count; i++)
        {
            LDIC[SystemLanguage.Korean].Add(datas[i].key_index, datas[i].localize_value_kr.Replace(';', '\n'));
            LDIC[SystemLanguage.English].Add(datas[i].key_index, datas[i].localize_value_en.Replace(';', '\n'));
            LDIC[SystemLanguage.Japanese].Add(datas[i].key_index, datas[i].localize_value_ja.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseTraditional].Add(datas[i].key_index, datas[i].localize_value_tc.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseSimplified].Add(datas[i].key_index, datas[i].localize_value_sc.Replace(';', '\n'));
        }
    }

    public void AddLanguageData(LocalizeData localizeData)
    {
        if(!LDIC[SystemLanguage.Korean].ContainsKey(localizeData.key_index))
        {
            LDIC[SystemLanguage.Korean].Add(localizeData.key_index, localizeData.localize_value_kr.Replace(';', '\n'));
            LDIC[SystemLanguage.English].Add(localizeData.key_index, localizeData.localize_value_en.Replace(';', '\n'));
            LDIC[SystemLanguage.Japanese].Add(localizeData.key_index, localizeData.localize_value_ja.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseTraditional].Add(localizeData.key_index, localizeData.localize_value_tc.Replace(';', '\n'));
            LDIC[SystemLanguage.ChineseSimplified].Add(localizeData.key_index, localizeData.localize_value_sc.Replace(';', '\n'));

        }
    }
    public string resourceName;
    public override void Load()
    {
        string[] lines = Resources.Load<TextAsset>(resourceName).text.Split('\n');
        datas = new List<LocalizeData>();
        for (int i = 0; i < lines.Length; i++)
        {
            datas.Add(new LocalizeData(lines[i].Split('\t')));
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
}
[Serializable]
public class FirebaseLocalizeData
{
    public List<LocalizeData> datas;

}
[Serializable]
public class LocalizeData
{
    public int key_index;
    public string localize_value_kr;
    public string localize_value_en;
    public string localize_value_ja;
    public string localize_value_tc;
    public string localize_value_sc;

    public LocalizeData(string[] words)
    {
        int index = 0;
        key_index = int.Parse(words[index++]);
        localize_value_kr = words[index++];
        localize_value_en = words[index++];
        localize_value_ja = words[index++];
        localize_value_tc = words[index++];
        localize_value_sc = words[index++];
    }

}