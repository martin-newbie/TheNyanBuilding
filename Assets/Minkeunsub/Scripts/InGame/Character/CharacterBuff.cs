using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manager = InGameManager;

public class CharacterBuff : Character, IBuff
{

    [Header("Character Buff")]
    public List<Vector2Int> BuffList = new List<Vector2Int>(); // 현재 자기 위치 기준
    public BuffType buffType;

    [Header("Character Buff Value")]
    public float buffValue;
    public float up_value;

    public float value => GetValue();

    public BuffType type => buffType;

    public float GetValue()
    {
        float v = 0f;
        v = buffValue + (manager.Instance.CharacterLevel[info.idx] * up_value);
        return v;
    }

    public void Init(Vector2Int idx)
    {
        thisPosIdx = idx;
    }

    // call when spawn or move end
    public void OnBuff()
    {
        foreach (var item in BuffList)
        {
            IGauge character = manager.Instance.GetIndexCharacter(thisPosIdx.x + item.x, thisPosIdx.y + item.y)?.GetComponent<IGauge>();
            character?.SetBuff(this);
        }
    }
}
