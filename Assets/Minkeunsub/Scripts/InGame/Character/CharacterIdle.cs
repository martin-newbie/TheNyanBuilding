using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : Character, IAuto, IGauge
{
    [Header("Character Dev")]
    public float autoGaugeAmt;

    public float curGagueValue;
    public float maxGagueValue;

    public Gauge gaugeObj;
    public Vector3 gaugeOffset = new Vector3(0, 1f, 0);

    public float additionalGaugeValue;
    public float additionalFailValue;

    List<IBuff> BuffCharacterList = new List<IBuff>();

    public void GetValue()
    {
        additionalFailValue = 1f;
        additionalGaugeValue = 1f;

        foreach (var item in BuffCharacterList)
        {
            switch (item.type)
            {
                case BuffType.SpeedUp:
                    additionalGaugeValue += item.value;
                    break;
                case BuffType.FailDecrease:
                    additionalFailValue += item.value;
                    break;
            }
        }

        BuffCharacterList.Clear();
    }

    public void InitGauge(Gauge gauge)
    {
        gaugeObj = gauge;
        gaugeObj.SetPos(gaugeOffset, transform);
    }

    public void OnIdle()
    {
        curGagueValue += (autoGaugeAmt * additionalGaugeValue) * Time.deltaTime;
        gaugeObj.SetGaugeFill(curGagueValue, maxGagueValue);

        if (curGagueValue >= maxGagueValue)
        {
            InGameManager.Instance.GaugeReward(this, additionalFailValue);
            curGagueValue = 0f;
        }
    }

    public void SetBuff(IBuff buff)
    {
        BuffCharacterList.Add(buff);
    }
}
