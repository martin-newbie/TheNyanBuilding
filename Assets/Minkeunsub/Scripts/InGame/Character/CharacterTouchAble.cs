using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTouchAble : Character, ITouchAble, IGauge
{
    [Header("Character Touch Able")]
    public float autoGaugeAmt;
    public float touchGaugeAmt;

    public float curGaugeValue;
    public float maxGaugeValue;

    protected Action GaugeEvent;

    public Gauge valueGauge;
    public Vector3 gaugeOffset;

    public float additionalGaugeValue;
    public float additionalFailValue;

    List<IBuff> BuffCharacterList = new List<IBuff>();


    private void Update()
    {
        float value = autoGaugeAmt * additionalGaugeValue;
        curGaugeValue += value * Time.deltaTime;
        valueGauge?.SetGaugeFill(curGaugeValue, maxGaugeValue);

        if(curGaugeValue >= maxGaugeValue)
        {
            InGameManager.Instance.GaugeReward(this, additionalFailValue);
            curGaugeValue = 0f;
        }
    }

    public void InitGauge(Gauge _gauge)
    {
        valueGauge = _gauge;
        valueGauge.SetPos(gaugeOffset, transform);
    }

    public void OnTouch()
    {
        curGaugeValue += touchGaugeAmt + (touchGaugeAmt * GameManager.Instance.tenGaugeUP);
    }

    public void SetBuff(IBuff buff)
    {
        BuffCharacterList.Add(buff);
    }

    public void GetValue()
    {
        additionalFailValue = 0f;
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
}
