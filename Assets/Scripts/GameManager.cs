using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Property { Can, Churu, Gold };

public enum UpgradeType
{
    rewardUP, touchGaugeUP, tenGaugeUP, successRate, failedRate

}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Init();
        can = 1000000000;
        churu = 1000000000;

        SetGoods();

    }
    public Text dayText;
    public Text goldText;
    public Text CanText;
    public Text churuText;

    public int day;
    public double gold;
    public double can;
    public double churu;
    public int[] upgradeLevelList = { 0, 0, 0, 0, 0 };
    public List<int> selectedCat;

    public List<UserData_Cat> catDatas;
    public Sprite[] catSprites;
    private void Init()
    {
        Set_rewardUP();
        Set_touchGaugeUP();
        Set_tenGaugeUP();
        Set_successRate();
        Set_failedRate();
        catDatas = new List<UserData_Cat>();
        selectedCat = new List<int>();

        for (int i = 0; i < StaticDataManager.Instance.catData.datas.Count; i++)
        {
            catDatas.Add(new UserData_Cat(i, 0, 0));
            selectedCat.Add(0);
        }

    }
    private void SetGoods()
    {
        goldText.text = Calculator.Instance.ConvertToString(gold);
        CanText.text = Calculator.Instance.ConvertToString(can);
        churuText.text = Calculator.Instance.ConvertToString(churu);

    }
    public bool UseProperty(Property type, double value)
    {
        if (type.Equals(Property.Gold))
        {
            if (gold >= value)
            {
                gold -= value;
                SetGoods();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (type.Equals(Property.Can))
        {
            if (can >= value)
            {
                can -= value;
                SetGoods();

                return true;
            }
            else
            {
                return false;
            }
        }
        else if (type.Equals(Property.Churu))
        {
            if (churu >= value)
            {
                churu -= value;
                SetGoods();

                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public float rewardUp;
    public void Set_rewardUP()
    {
        UpgradeData upgradeData = StaticDataManager.GetUpgradeData(0);
        rewardUp = upgradeData.value + Mathf.Pow(upgradeData.valueIncrease,upgradeLevelList[0]);
    }
    public float touchGaugeUP;

    public void Set_touchGaugeUP()
    {
        UpgradeData upgradeData = StaticDataManager.GetUpgradeData(1);
        touchGaugeUP = upgradeData.value + Mathf.Pow(upgradeData.valueIncrease, upgradeLevelList[1]);
    }
    public float tenGaugeUP;
    public void Set_tenGaugeUP()
    {
        UpgradeData upgradeData = StaticDataManager.GetUpgradeData(2);
        tenGaugeUP = upgradeData.value + Mathf.Pow(upgradeData.valueIncrease, upgradeLevelList[2]);
    }
    public float successRate;
    public void Set_successRate()
    {
        UpgradeData upgradeData = StaticDataManager.GetUpgradeData(3);
        successRate = upgradeData.value + Mathf.Pow(upgradeData.valueIncrease, upgradeLevelList[3]);
    }
    public float failedRate;
    public void Set_failedRate()
    {
        UpgradeData upgradeData = StaticDataManager.GetUpgradeData(4);
        failedRate = upgradeData.value + Mathf.Pow(upgradeData.valueIncrease, upgradeLevelList[4]);
    }
    public void InitUpgradeList(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.rewardUP:
                Set_rewardUP();
                break;
            case UpgradeType.touchGaugeUP:
                Set_touchGaugeUP();
                break;
            case UpgradeType.tenGaugeUP:
                Set_tenGaugeUP();
                break;
            case UpgradeType.successRate:
                Set_successRate();
                break;
            case UpgradeType.failedRate:
                Set_failedRate();
                break;
  
        }


    }

    public void AddCat(int catIndex, int amount = 1)
    {
        InGameManager.Instance.GainCharacter(catIndex);

        catDatas[catIndex].soulStone += amount;
        CatLevelUp(catIndex);

    }
    public void CatLevelUp(int catIndex)
    {
        int c = 0;
        while (c < 100)
        {
            int max = (catDatas[catIndex].tier + 1);
            if (catDatas[catIndex].soulStone >= max)
            {
                catDatas[catIndex].tier++;
                catDatas[catIndex].soulStone -= max;
  
            }
            else
            {
                break;
            }
        }

    }
}
