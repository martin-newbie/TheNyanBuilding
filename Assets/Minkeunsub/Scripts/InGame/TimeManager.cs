using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Time")]
    public float curTime;
    public float maxTime = 30f;
    public int curDay; //�ؽ�Ʈ�� ��Ÿ���� �� ��¥
    public int maxDay = 30;

    void Update()
    {

        curTime += Time.deltaTime;

        if (curTime >= maxTime)
        {
            curDay++;
            curTime = 0f;

            if(curDay >= maxDay)
            {
                // change gold to churu
                InitCoin();
                curDay = 0;
            }
        }
    }

    public void InitCoin()
    {
        int goldCnt = (int)GameManager.Instance.gold;

        GameManager.Instance.churu += goldCnt * 10f;
        GameManager.Instance.gold = 0;
    }
}
