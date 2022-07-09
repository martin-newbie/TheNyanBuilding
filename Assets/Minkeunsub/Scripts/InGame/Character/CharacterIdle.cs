using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : Character, IAuto
{
    [Header("Character Dev")]
    public float autoGaugeAmt;

    public float curGagueValue;
    public float maxGagueValue;

    public Gauge gaugeObj;
    public Vector3 gaugeOffset = new Vector3(0, 1f, 0);

    public void InitGauge(Gauge gauge)
    {
        gaugeObj = gauge;
        gaugeObj.SetPos(gaugeOffset, transform);
    }

    public void OnIdle()
    {
        curGagueValue += autoGaugeAmt * Time.deltaTime;
        gaugeObj.SetGaugeFill(curGagueValue, maxGagueValue);

        if (curGagueValue >= maxGagueValue)
        {
            // gauge event
            curGagueValue = 0f;
        }
    }

}
