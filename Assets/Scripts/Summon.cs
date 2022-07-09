//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class Summon : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
//{
//    Canvas canvas;
//    public Animator animator;

//    [Header("Link Object")]
//    public ArcherInfo archerInfo;
//    public Text cardCountText;
//    public Image[] tierFillImage;
//    public Button background;

//    int cardCount;
//    float maxValue;
//    float startCount;

//    bool isAlreadyTierUp;

//    public GameObject animationSkipButton;
//    public GameObject effectObject;
//    public GameObject summon_one_panel;
//    public GameObject summon_ten_panel;


//    public SummonBox summonBox_one;
//    public SummonBox[] summonBox_ten;

//    public GameObject cloaseButton_AD;
//    public GameObject cloaseButton_else;
//    public GameObject resummon_one;
//    public GameObject resummon_ten;

//    public CustomSlider slider;

//    public Image adSummonImage;
//    public Text adSummonRemainTime;


//    public int[] summonResultArray;
//    public int summonButtonIndex;

//    private bool isEffectAnimationPlayed;
//    private int maxTier;
//    private bool isFake;
//    private string animationClipName;
//    private string animationStartClipName;
    
    
    
    
//    private void Awake()
//    {
//        canvas = GetComponent<Canvas>();
//        animator = GetComponent<Animator>();
//        canvas.enabled = false;
//        summonResultArray = new int[11];
//        isEffectAnimationPlayed = false;
//    }
//    private void Start()
//    {
//        EventManager.StartListening("IAPArcherSummon_ten", IAPArcherSummon_ten);
//        EventManager.StartListening("IAPArcherSummon_one", IAPArcherSummon_one);
//        EventManager.StartListening("Summon_One_LimitGrade45", Summon_One_LimitGrade45);

//    }

//    public virtual void Init()
//    {
//        slider.value = 0f;
//        Time.timeScale = 0.1f;
//        maxTier = 1;
//        summon_one_panel.SetActive(false);
//        summon_ten_panel.SetActive(false);
//        for (int i = 0; i < 11; i++)
//            summonBox_ten[i].gameObject.SetActive(false);
//        summonBox_one.gameObject.SetActive(false);
//        cloaseButton_AD.SetActive(false);
//        cloaseButton_else.SetActive(false);
//        resummon_one.SetActive(false);
//        resummon_ten.SetActive(false);
//        effectObject.SetActive(true);
//        isFake = false;
//        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
//            isFake = true;
//    }

//    public void SetMaxTier(int tier)
//    {
//        if (tier > 3)
//            tier = 3;

//        if (tier > maxTier)
//            maxTier = tier;
//    }

//    public void StartSummonAnimation()
//    {
//        canvas.enabled = true;
//        animator.enabled = true;
//        animationSkipButton.SetActive(true);
//        if (maxTier == 1)
//            animationClipName = string.Format("prepare_tier{0}", maxTier);
//        else
//        {
//            if (isFake)
//                animationClipName = string.Format("prepare_tier{0}_fake", maxTier);
//            else
//                animationClipName = string.Format("prepare_tier{0}", maxTier);
//        }
//        animationStartClipName = string.Format("start_tier{0}", maxTier);
//        //animator.Play(animationClipName, -1, 0f);
//        slider.Init(animator, animationClipName, maxTier);
//    }

//    public void Summon_AD()
//    {
//        if (TimeChecker.ServerTime >= UserDataController.Instance.nextADTime_Summon)
//            UserDataController.Instance.ShowRewardedAd(()=>
//            {
//                FirebaseManager.Instance.LogEvent("ad_watch", "type", "archer");
//                FirebaseManager.Instance.LogEvent("archer_summon", "type", "ad_1");

//                Init();
//                summonButtonIndex = 0;
//                int cardCount = GameManager.Instance.additionalTierGage;
//                Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//                summonResultArray[0] =  NextSummonArcherIndex();
//                UserDataController.Instance.AddArcher(summonResultArray[0]);
//                EventManager.TriggerEvent("UpdateArcherList");
//                StartSummonAnimation();

//                UserDataController.Instance.nextADTime_Summon = TimeChecker.ServerTime.AddSeconds(Constant.adCoolTime);
//                StoreCanvas.Instance.ADCheck_Summon();
//            },WatchAdsType.archer);
//            //Summon_AD_Success();
//        else
//            MessageManager.Instance.Open(LocalizeDataManager.GetLocalizeString(25));
//    }

//    /*
//    void Summon_AD_Success()
//    {
//        StartCoroutine(iSummon_AD_Success());
//    }

//    IEnumerator iSummon_AD_Success()
//    {
//        yield return new WaitForSecondsRealtime(0.15f);
//        Init();
//        summonButtonIndex = 0;
//        int cardCount = GameManager.Instance.additionalTierGage;
//        Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//        summonResultArray[0] = NextSummonArcherIndex();
//        UserDataController.Instance.AddArcher(summonResultArray[0]);
//        EventManager.TriggerEvent("UpdateArcherList");
//        StartSummonAnimation();

//        UserDataController.Instance.nextADTime_Summon = UnbiasedTime.Instance.Now().AddSeconds(Constant.adCoolTime);
//        StoreCanvas.Instance.ADCheck_Summon();
//    }
//    */


//    //4성 99% 5성 1% 확정 아처 뽑기
//    public void Summon_One_LimitGrade45()
//    {
//        Init();
//        summonButtonIndex = 3;
//        int cardCount = GameManager.Instance.additionalTierGage;
//        Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//        summonResultArray[0] = NextSummonArcherIndexLimit45();
//        UserDataController.Instance.AddArcher(summonResultArray[0]);
//        EventManager.TriggerEvent("UpdateArcherList");
//        StartSummonAnimation();
//    }


//    public void Summon_one(int redraw)
//    {
//        if (GameManager.Instance.UseProperty(Property.Jewel, 100))
//        {
//            FirebaseManager.Instance.LogEvent("archer_summon",
//                            new Firebase.Analytics.Parameter("type", "dia_1"),
//                            new Firebase.Analytics.Parameter("is_redraw", redraw)
//                            );
//            Init();
//            summonButtonIndex = 1;
//            int cardCount = GameManager.Instance.additionalTierGage;
//            Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//            summonResultArray[0] = NextSummonArcherIndex();
//            UserDataController.Instance.AddArcher(summonResultArray[0]);
//            EventManager.TriggerEvent("UpdateArcherList");
//            StartSummonAnimation();
//        }
//    }

//    public void Summon_ten(int redraw)
//    {
//        if (GameManager.Instance.UseProperty(Property.Jewel, 1000))
//        {
//            FirebaseManager.Instance.LogEvent("archer_summon",
//                new Firebase.Analytics.Parameter("type", "dia_11"),
//                new Firebase.Analytics.Parameter("is_redraw", redraw)
//                );

//            Init();
//            summonButtonIndex = 2;
//            int cardCount = GameManager.Instance.additionalTierGage;
//            for (int i = 0; i < 11; i++)
//            {
//                Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//                int archerIndex = NextSummonArcherIndex();
//                summonResultArray[i] = archerIndex;
//                UserDataController.Instance.AddArcher(archerIndex);
//            }
//            EventManager.TriggerEvent("UpdateArcherList");
//            StartSummonAnimation();
//        }
//    }
//    public void IAPArcherSummon_one()
//    {
//        Init();
//        summonButtonIndex = 3;
//        int cardCount = GameManager.Instance.additionalTierGage;
//        Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//        summonResultArray[0] = NextSummonArcherIndex();
//        UserDataController.Instance.AddArcher(summonResultArray[0]);
//        EventManager.TriggerEvent("UpdateArcherList");
//        StartSummonAnimation();
//    }
//    public void IAPArcherSummon_ten()
//    {
//        Init();
//        summonButtonIndex = 4;
//        int cardCount = GameManager.Instance.additionalTierGage;
//        for (int i = 0; i < 11; i++)
//        {
//            Quest.Instance.GetQuestActivity(QuestType.summonArcher);
//            int archerIndex = NextSummonArcherIndex();
//            summonResultArray[i] = archerIndex;
//            UserDataController.Instance.AddArcher(archerIndex);
//        }
//        EventManager.TriggerEvent("UpdateArcherList");
//        StartSummonAnimation();
//    }
//    public virtual void EffectFinished()
//    {
//        animationSkipButton.SetActive(false);
//        effectObject.SetActive(false);
//        animator.enabled = false;
//        switch (summonButtonIndex)
//        {
//            case 0: //광고 
//                summon_one_panel.SetActive(true);
//                StartCoroutine(WaitEffect_one_AD());
//                /*
//                summonBox_one.gameObject.SetActive(true);
//                summonBox_one.Init(summonResultArray[0]);
//                cloaseButton_AD.SetActive(true);
//                */
//                break;
//            case 1: // 1마리
//                summon_one_panel.SetActive(true);
//                StartCoroutine(WaitEffect_one());
//                /*
//                summonBox_one.gameObject.SetActive(true);
//                summonBox_one.Init(summonResultArray[0]);
//                cloaseButton_else.SetActive(true);
//                resummon_one.SetActive(true);
//                */
//                break;
//            case 2: // 10마리
//                summon_ten_panel.SetActive(true);
//                StartCoroutine(WaitEffect_ten());
//                break;
//            case 3: // iap1마리
//                summon_one_panel.SetActive(true);
//                StartCoroutine(WaitEffect_one_IAP());
//                break;
//            case 4: // iap10마리
//                summon_ten_panel.SetActive(true);
//                StartCoroutine(WaitEffect_ten_IAP());
//                break;
//        }
//    }

//    IEnumerator WaitEffect_one_AD()
//    {
//        yield return new WaitForSecondsRealtime(0.1f);
//        summonBox_one.gameObject.SetActive(true);
//        summonBox_one.Init(summonResultArray[0]);
//        yield return new WaitForSecondsRealtime(0.1f);
//        cloaseButton_AD.SetActive(true);
//    }


//    IEnumerator WaitEffect_one()
//    {
//        yield return new WaitForSecondsRealtime(0.1f);
//        summonBox_one.gameObject.SetActive(true);
//        summonBox_one.Init(summonResultArray[0]);
//        yield return new WaitForSecondsRealtime(0.1f);
//        cloaseButton_else.SetActive(true);
//        resummon_one.SetActive(true);
//    }
    
//    /*
//    IEnumerator WaitEffect_one(Action whenDone, int tier)
//    {
//        canvas.enabled = true;
//        if (!isEffectAnimationPlayed)
//        {
//            isEffectAnimationPlayed = true;
//            AudioManager.Instance.Play("summon");
//            animator.Play("Effect_Normal", -1, 0f);
//            yield return new WaitForSecondsRealtime(1f);
//        }
//        whenDone();
//    }
//    */

//    IEnumerator WaitEffect_ten()
//    {
//        yield return new WaitForSecondsRealtime(0.1f);
//        for (int i = 0; i < 11; i++)
//        {
//            summonBox_ten[i].gameObject.SetActive(true);
//            summonBox_ten[i].Init(summonResultArray[i]);
//            yield return new WaitForSecondsRealtime(0.1f);
//        }
//        cloaseButton_else.SetActive(true);
//        resummon_ten.SetActive(true);
//    }
//    IEnumerator WaitEffect_one_IAP()
//    {
//        yield return new WaitForSecondsRealtime(0.1f);
//        summonBox_one.gameObject.SetActive(true);
//        summonBox_one.Init(summonResultArray[0]);
//        yield return new WaitForSecondsRealtime(0.1f);
//        cloaseButton_AD.SetActive(true);
//    }
//    IEnumerator WaitEffect_ten_IAP()
//    {
//        yield return new WaitForSecondsRealtime(0.1f);
//        for (int i = 0; i < 11; i++)
//        {
//            summonBox_ten[i].gameObject.SetActive(true);
//            summonBox_ten[i].Init(summonResultArray[i]);
//            yield return new WaitForSecondsRealtime(0.1f);
//        }
//        cloaseButton_AD.SetActive(true);

//    }

//    public int NextSummonArcherIndexLimit45()
//    {
//        float randNumber = RandNumberGenerator.Instance.randNumber;
//        int archerIndex;

//        List<ArcherData> archer4 = new List<ArcherData>();
//        List<ArcherData> archer5 = new List<ArcherData>();

//        for(int i = 0; i < StaticDataManager.Instance.archerData.datas.Count; i++)
//        {
//            ArcherData archerData = StaticDataManager.GetArcherData(i);
//            if(archerData.grade == 4)
//            {
//                archer4.Add(archerData);
//            }
//            if(archerData.grade == 5)
//            {
//                archer5.Add(archerData);
//            }
//        }

//        if(randNumber<= 0.01f)
//        {
//            int rand5 = UnityEngine.Random.RandomRange(0, archer5.Count);
//            return archer5[rand5].key_index;
//        }
//        else
//        {
//            int rand4 = UnityEngine.Random.RandomRange(0, archer4.Count);
//            return archer4[rand4].key_index;
//        }

//    }

//    public int NextSummonArcherIndex()
//    {
//        float randNumber = RandNumberGenerator.Instance.randNumber;
//        //int archerIndex = Mathf.FloorToInt((float)GameManager.Instance.archerSize * randNumber);
//        int archerIndex;
//        /*
//        4star = 0, 1  ==> 0.02, 0.04
//        3star = 2, 3, 4 ==> 0.07, 0.1, 0.13
//        2star = 5, 6, 7 ==> 0.21, 0.29, 0.37
//        1star = 8, 9, 10, 11, 12, 13, 14 ==> 0.46, 0.55, 0.64, 0.73, 0.82, 0.91, 1
//        */

//        for (int i = 0; i < StaticDataManager.Instance.archerData.datas.Count; i++)
//        {
//            ArcherData archerData = StaticDataManager.GetArcherData(i);
//            if (randNumber <= archerData.summonRate)
//            {
//                SetMaxTier(archerData.grade-1);
//                //int[] tests = { 47, 15 };
//                //int ran = UnityEngine.Random.Range(0, 2);
//                //return tests[ran];
//                return i;

//            }
//        }
//        Debug.Log("wrong summon probability!!");
//        return -1;
//    }

//    public void CanvasClose()
//    {
//        isEffectAnimationPlayed = false;
//        canvas.enabled = false;
//        Time.timeScale = GameManager.Instance.gameSpeed;
//    }
    
//}
