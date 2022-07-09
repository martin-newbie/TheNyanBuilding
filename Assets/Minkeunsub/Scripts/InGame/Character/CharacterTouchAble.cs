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
        curGaugeValue += (autoGaugeAmt * additionalGaugeValue)* Time.deltaTime;
        valueGauge?.SetGaugeFill(curGaugeValue, maxGaugeValue);

        if(curGaugeValue >= maxGaugeValue)
        {
            GaugeEvent?.Invoke();
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
        curGaugeValue += touchGaugeAmt;
    }

    public void SetBuff(IBuff buff)
    {
        BuffCharacterList.Add(buff);
    }

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
}
