using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GameDuo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LocalizeDataManager : MonoBehaviour
{
    static LocalizeDataManager instance;
    public static LocalizeDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static int StaticDataVersion
    {
        get { return 0; }
    }

    #region Localize Datas
    public List<SystemLanguage> usedLanguages;
    
    #if UNITY_EDITOR
    public SystemLanguage testLanguage;
    #endif
    
    public static SystemLanguage userLanguage
    {
        get
        {
#if UNITY_EDITOR
            return instance.testLanguage;
#endif
            switch (PlayerPrefs.GetString("UserLanguage", "Korean"))
            {
                case "English" :
                    return SystemLanguage.English;
                case "Korean" :
                    return SystemLanguage.Korean;
                case "ChineseSimplified" :
                    return SystemLanguage.ChineseSimplified;
                case "Chinese" :
                case "ChineseTraditional" :
                    return SystemLanguage.ChineseTraditional;
                case "Japanese" :
                    return SystemLanguage.Japanese;
            }
            return SystemLanguage.English;
        }
        set
        {
            PlayerPrefs.SetString("UserLanguage", value.ToString());
            PlayerPrefs.Save();
        }
    }

    private Dictionary<string, Sprite> spriteDic;
    public static Sprite GetSprite(string keyString)
    {
        if (!instance.spriteDic.ContainsKey(keyString))
        {
            instance.spriteDic.Add(keyString,
                Resources.Load<Sprite>("LocalizeSprites/" + userLanguage + "/" + keyString));
        }
        return instance.spriteDic[keyString];
    }
    
    public StaticLocalizeData staticLocalizeData, archerData, mapData, questData, bossData, noticeData, nicknameData,petData,clanData;
    public StaticLocalizeData2 skillData, upgradeData, cardData, runeData, totemData;
    public static string GetLocalizeString(int index)
    {
        return instance.staticLocalizeData.GetLocalizeString(userLanguage, index);
    }
    public static void SetLocalizeString(LocalizeData localizeData)
    {
        instance.staticLocalizeData.AddLanguageData(localizeData);
    }
    public static string GetArcherName(int index)
    {
        return instance.archerData.GetLocalizeString(userLanguage, index);
    }
    public static string GetMapName(int index)
    {
        return instance.mapData.GetLocalizeString(userLanguage, index);
    }
    public static string GetQuestTitle(int index)
    {
        return instance.questData.GetLocalizeString(userLanguage, index);
    }
    public static string GetBossName(int index)
    {
        return instance.bossData.GetLocalizeString(userLanguage, index);
    }
    public static string GetSkillName(int index)
    {
        return instance.skillData.GetLocalizeString(userLanguage, index);
    }
    public static string GetSkillInfo(int index)
    {
        return instance.skillData.GetLocalizeString2(userLanguage, index);
    }
    public static string GetRuneName(int index)
    {
        return instance.runeData.GetLocalizeString(userLanguage, index);
    }
    public static string GetRuneInfo(int index)
    {
        return instance.runeData.GetLocalizeString2(userLanguage, index);
    }
    public static string GetUpgradeTitle(int index)
    {
        return instance.upgradeData.GetLocalizeString(userLanguage, index);
    }
    public static string GetUpgradeInfo(int index)
    {
        return instance.upgradeData.GetLocalizeString2(userLanguage, index);
    }
    public static string GetCardTitle(int index)
    {
        return instance.cardData.GetLocalizeString(userLanguage, index);
    }
    public static string GetCardInfo(int index)
    {
        return instance.cardData.GetLocalizeString2(userLanguage, index);
    }
    public static string GetNoticeString(int index)
    {
        return instance.noticeData.GetLocalizeString(userLanguage, index);
    }
    public static string GetToTemName(int index)
    {
        return instance.totemData.GetLocalizeString(userLanguage, index);
    }
    public static string GetToTemInfo(int index)
    {
        return instance.totemData.GetLocalizeString2(userLanguage, index);
    }

    public static string GetNicknameInfo(int index)
    {
        return instance.nicknameData.GetLocalizeString(userLanguage, index);
    }

    public static string GetPetName(int index)
    {
        return instance.petData.GetLocalizeString(userLanguage, index);
    }
    public static string GetClanLocalize(int index)
    {
        return instance.clanData.GetLocalizeString(userLanguage, index);
    }
    #endregion
    public bool IsLoaded { get; set; }
    private List<TextDataBase> staticLocalizeDatas;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        spriteDic = new Dictionary<string, Sprite>();
        staticLocalizeData.Load();
        InitLanguage();
    }
    public static void InitLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
            case SystemLanguage.Japanese:
            case SystemLanguage.English:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                PlayerPrefs.SetString("UserLanguage", Application.systemLanguage.ToString());
                break;
            case SystemLanguage.Chinese:
                PlayerPrefs.SetString("UserLanguage", "ChineseTraditional");
                break;
            default:
                PlayerPrefs.SetString("UserLanguage", "English");
                break;
        }
        PlayerPrefs.Save();
    }
    
    IEnumerator Start()
    {
        staticLocalizeDatas = new List<TextDataBase>
        {
            archerData,
            mapData, 
            questData,
            bossData,
            noticeData,
            skillData,
            upgradeData,
            cardData, 
            runeData,       
            totemData,
            nicknameData,
			petData,
            clanData,
        };
        for (int i = 0; i < staticLocalizeDatas.Count; i++)
        {
            staticLocalizeDatas[i].Load();
            yield return null;
        }
        IsLoaded = true;
    }
}
