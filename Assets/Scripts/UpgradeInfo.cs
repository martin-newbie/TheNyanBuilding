using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfo : MonoBehaviour
{
    public Image iconImage;
    public Text title;
    public Text content;
    public Text leveText;
    public Text costText;
    public Text upgradeValueText;
    public Image propertyImage;

    public GameObject purchaseBtn;
    public GameObject maxBtn;

    UpgradeCanvas upgradeCanvas;
    public int maxLevel;
    public int currentLevel;
    int upgradeIndex;
    public int userUpgradeIndex;
    UpgradeData upgradeData;

    public double cost;

    private void Awake()
    {
        upgradeCanvas = GetComponentInParent<UpgradeCanvas>();
    }

    private void Start()
    {
    }


    public void Init(int upgradeIndex, int userUpgradeIndex)
    {
        this.upgradeIndex = upgradeIndex;
        this.userUpgradeIndex = userUpgradeIndex;
        upgradeData = StaticDataManager.GetUpgradeData(this.upgradeIndex);

        string path = string.Format("Sprites/UpgradeIcon/{0}", upgradeData.upgradeTypeString);
        iconImage.sprite = Resources.Load<Sprite>(path);
        maxLevel = upgradeData.limitCount <0 ? 999999: upgradeData.limitCount;
        propertyImage.color = Color.white;

        //title.text = upgradeData.name;
        currentLevel = GameManager.Instance.upgradeLevelList[this.userUpgradeIndex];

        float contentValue = upgradeData.value + (upgradeData.valueIncrease * currentLevel);
        
        //content.text = string.Format(upgradeData.content, upgradeData.value * currentLevel);
        content.text = string.Format(upgradeData.name, contentValue);
        leveText.text = string.Format("<size=30>Lv.</size><size=35>{0}</size>", currentLevel);
        if (currentLevel < maxLevel)
        {
            purchaseBtn.SetActive(true);
            maxBtn.SetActive(false);

            float diff = upgradeData.valueIncrease;
            /*
            if (diff > 0f)
                upgradeValueText.text = upgradeData.value[currentLevel].ToString() + " (+" + string.Format("{0:0.##}", diff)+ ")";
            else
                upgradeValueText.text = upgradeData.value[currentLevel].ToString() + " (" + string.Format("{0:0.##}", diff) + ")";
            */
            if (diff > 0f)
                upgradeValueText.text = string.Format("+ {0}", diff);
            else
                upgradeValueText.text = string.Format("{0}", diff);

            cost = upgradeData.cost * Mathf.Pow(upgradeData.costIncrease, currentLevel) ;


            costText.text = Calculator.Instance.ConvertToString(cost);
        }
        else
        {
            purchaseBtn.SetActive(false);
            maxBtn.SetActive(true);
        }
    }

    public void UpgradeBtnClicked()
    {
        if (currentLevel < maxLevel)
        {
            if (GameManager.Instance.UseProperty(Property.Can, cost))
            {
                GameManager.Instance.upgradeLevelList[userUpgradeIndex]++;

            }
            Init(upgradeIndex, userUpgradeIndex);

        }
    }


}
