using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manager = InGameManager;

public class CharacterBuff : Character, IBuff
{

    [Header("Character Buff")]
    public List<Vector2Int> BuffList = new List<Vector2Int>(); // 현재 자기 위치 기준
    public float buffValue;
    public BuffType buffType;

    public float value => buffValue;

    public BuffType type => buffType;

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
