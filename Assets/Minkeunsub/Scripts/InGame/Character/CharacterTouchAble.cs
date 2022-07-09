using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTouchAble : Character, ITouchAble
{
    [Header("Character Touch Able")]
    public float autoGaugeAmt;
    public float touchGaugeAmt;

    public float curGaugeValue;
    public float maxGaugeValue;

    protected Action GaugeEvent;

    public Gauge valueGauge;
    public Vector3 gaugeOffset;

    private void Update()
    {
        curGaugeValue += autoGaugeAmt * Time.deltaTime;
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
    }
}
