using System.Collections;
using System.Collections.Generic;
using System.Text;
using GameDuo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticDataManager : MonoBehaviour
{
    private static StaticDataManager instance;
    public static StaticDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public static int StaticDataVersion
    {
        get { return 0; }
    }

    private List<TextDataBase> staticDatas;

    public StaticDataUpgradeData upgradeData;
    public static UpgradeData GetUpgradeData(int index)
    {
        return Instance.upgradeData.datas[index];
    }
    public StaticDataCatData catData;
    public static CatData GetCatData(int index)
    {
        return Instance.catData.datas[index];
    }



    private int staticDataLoadCount = 0;
    private int TotalStaticDatasCount;
    public int StaticDataLoadCount
    {
        get { return staticDataLoadCount; }
        set
        {
            staticDataLoadCount = value;
            #if UNITY_EDITOR
            Debug.Log(string.Format("데이터 검사 중 ({0}/{1})", value, TotalStaticDatasCount));
            #endif
        }
    }


    public bool dataCheckComplete = false;

    void Start()
    {
        staticDatas = new List<TextDataBase>
        {
            upgradeData,catData,
        };

        for (int i = 0; i < staticDatas.Count; i++)
        {
            //staticDatas[i].LocalLoadForEditor();
            staticDatas[i].LoadForTextData();
            StaticDataLoadCount++;
        }
        dataCheckComplete = true;
    }
}
