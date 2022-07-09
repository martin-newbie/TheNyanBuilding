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
            // gauge event
            curGagueValue = 0f;
        }
    }

}
