using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;


public class CatInfoPanel : MonoBehaviour
{
    private static CatInfoPanel instance;

    public static CatInfoPanel Instance => instance;

    protected  void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        canvas = GetComponent<Canvas>();
    }
    public Canvas canvas;
    public Text nameText;
    private CatData catStaticData;
    public UserData_Cat catUseData;
    public DrawStar star;
    public Image gradeImage;
    public Image petImage;
    public Text levelText;
    public Text tierGageText;
    public Image tierFillImage;
    public Text InfoText;
    bool isOn;
    private void SettingPanel(int catIndex)
    {
        catUseData = GameManager.Instance.catDatas[catIndex];
        catStaticData = StaticDataManager.GetCatData(catIndex);
        petImage.sprite = GameManager.Instance.catSprites[catIndex];
        nameText.text = catStaticData.name;
        star.Init(catStaticData.grade);
        //gradeImage.sprite = SpriteManager.Instance.archerGradeSprites[petStaticData.grade - 1];
        levelText.text = string.Format("Level {0}", catUseData.tier);

        int maxValue = (catUseData.tier + 1);
        tierGageText.text = catUseData.soulStone + " / " + maxValue;
        tierFillImage.fillAmount = (float)catUseData.soulStone / (float)maxValue;
        InfoText.text = catStaticData.content;
        if (gradeImage != null)
        {
            SpriteAtlas spriteAtlas = Resources.Load("Sprites/GradeSprites", typeof(SpriteAtlas)) as SpriteAtlas;
            gradeImage.sprite = spriteAtlas.GetSprite(string.Format("grade{0}", catStaticData.grade));
        }

    }
    public void Open(int petIndex)
    {
        SettingPanel(petIndex);
        canvas.enabled = true;
    }

}
