using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Summon : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    Canvas canvas;
    public Animator animator;

    [Header("Link Object")]
    public Text cardCountText;
    public Image[] tierFillImage;
    public Button background;

    int cardCount;
    float maxValue;
    float startCount;

    bool isAlreadyTierUp;

    public GameObject summon_one_panel;
    public GameObject summon_ten_panel;


    public SummonBox summonBox_one;
    public SummonBox[] summonBox_ten;

    public Image adSummonImage;
    public Text adSummonRemainTime;


    public int[] summonResultArray;
    public int summonButtonIndex;

    private bool isEffectAnimationPlayed;
    private int maxTier;
    private bool isFake;
    private string animationClipName;
    private string animationStartClipName;


    private int state;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        animator = GetComponent<Animator>();
        canvas.enabled = false;
        summonResultArray = new int[11];
        isEffectAnimationPlayed = false;
    }
    private void Start()
    {


    }

    public virtual void Init()
    {
        state = 0;
        Time.timeScale = 0.1f;
        maxTier = 1;
        summon_one_panel.SetActive(false);
        summon_ten_panel.SetActive(false);
        for (int i = 0; i < 11; i++)
            summonBox_ten[i].gameObject.SetActive(false);
        summonBox_one.gameObject.SetActive(false);
        isFake = false;
        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
            isFake = true;
    }

    public void SetMaxTier(int tier)
    {
        if (tier > 3)
            tier = 3;

        if (tier > maxTier)
            maxTier = tier;
    }

    public void StartSummonAnimation()
    {
        state = 1;
        canvas.enabled = true;
        animator.enabled = true;
        animator.Play("start_tier1", -1, 0f);
    }



    public void Summon_one()
    {
        if (GameManager.Instance.UseProperty(Property.Churu, 100))
        {

            Init();
            summonButtonIndex = 1;
            summonResultArray[0] = NextSummonCatIndex();
            GameManager.Instance.AddCat(summonResultArray[0]);
            StartSummonAnimation();
            EventManager.TriggerEvent("CatSetting");

        }
    }

    public void Summon_ten()
    {
        if (GameManager.Instance.UseProperty(Property.Churu, 1000))
        {


            Init();
            summonButtonIndex = 2;
            for (int i = 0; i < 11; i++)
            {
                int catIndex = NextSummonCatIndex();
                summonResultArray[i] = catIndex;
                GameManager.Instance.AddCat(catIndex);
            }
            StartSummonAnimation();
            EventManager.TriggerEvent("CatSetting");

        }
    }

    public virtual void EffectFinished()
    {
        animator.enabled = false;
        switch (summonButtonIndex)
        {
            case 1: // 1마리
                summon_one_panel.SetActive(true);
                StartCoroutine(WaitEffect_one());
                /*
                summonBox_one.gameObject.SetActive(true);
                summonBox_one.Init(summonResultArray[0]);
                cloaseButton_else.SetActive(true);
                resummon_one.SetActive(true);
                */
                break;
            case 2: // 10마리
                summon_ten_panel.SetActive(true);
                StartCoroutine(WaitEffect_ten());
                break;
        }
    }

    IEnumerator WaitEffect_one()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        summonBox_one.gameObject.SetActive(true);
        summonBox_one.Init(summonResultArray[0]);
        yield return new WaitForSecondsRealtime(0.1f);
        state = 2;
    }


    IEnumerator WaitEffect_ten()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < 11; i++)
        {
            summonBox_ten[i].gameObject.SetActive(true);
            summonBox_ten[i].Init(summonResultArray[i]);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        state = 2;

    }



    public int NextSummonCatIndex()
    {
        float randNumber = UnityEngine.Random.Range(0f, 1f);


        for (int i = 1; i < StaticDataManager.Instance.catData.datas.Count; i++)
        {
            CatData catData = StaticDataManager.GetCatData(i);
            if (randNumber <= catData.summonRate)
            {
                SetMaxTier(catData.grade - 1);
                return i;

            }
        }
        Debug.Log("wrong summon probability!!");
        return -1;
    }

    public void CanvasClose()
    {
        isEffectAnimationPlayed = false;
        canvas.enabled = false;
        Time.timeScale = 1f;
    }
    public void OnClickLastAni()
    {
        if (state == 1)
            animator.Play("prepare_tier1", -1, 0f);
        else if (state == 2)
            CanvasClose();
    }

}
