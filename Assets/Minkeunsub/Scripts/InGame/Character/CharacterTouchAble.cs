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

    public float GetBuff(BuffType type, params IBuff[] buff)
    {
        float value = 1f;
        foreach (var item in buff)
        {
            value += item.value;
        }

        switch (type)
        {
            case BuffType.SpeedUp:
                additionalGaugeValue = value;
                break;
            case BuffType.FailDecrease:
                additionalFailValue = value;
                break;
        }

        return value;
    }
}
