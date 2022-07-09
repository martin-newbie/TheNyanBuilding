using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using System;
public class CatInfo : MonoBehaviour
{
    public Image catImage;

    public Text catName;
    public Image gradeImage;

    public Text levelText;
    public Text LvText;

    public Text soulStoneCount;
    public Text tierGageText;
    public DrawStar star;
    public Image tierFillImage;
    public CatData catData;
    public UserData_Cat catUseData;

    int catIndex;

    private void Start()
    {
    }

    public void Init(int catIndex)
    {

        if (catIndex != -1)
        {
            catData = StaticDataManager.GetCatData(catIndex);
            catUseData = GameManager.Instance.catDatas[catIndex];

            this.catIndex = catIndex;

            if (catImage != null)
                catImage.sprite = GameManager.Instance.catSprites[catIndex];


            if (star != null)
                star.Init(catData.grade);

            if (catName != null)
                catName.text = catData.name;

            if (gradeImage != null)
            {
                SpriteAtlas spriteAtlas = Resources.Load("Sprites/GradeSprites", typeof(SpriteAtlas)) as SpriteAtlas;
                gradeImage.sprite = spriteAtlas.GetSprite(string.Format("grade{0}", catData.grade));
            }
            if (levelText != null)
            {
                levelText.text = string.Format("Level {0}", catUseData.tier);
            }
            if (LvText != null)
            {
                LvText.text = string.Format("Lv. {0}", catUseData.tier);
            }

        }

    }



}
[Serializable]
public class UserData_Cat
{
    public int key_index;
    public int tier;
    public int soulStone;

    public UserData_Cat(int key_index, int tier, int soulStone)
    {
        this.key_index = key_index;
        this.tier = tier;
        this.soulStone = soulStone;

    }
}