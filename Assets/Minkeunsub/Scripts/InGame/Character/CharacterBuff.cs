using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manager = InGameManager;

public class CharacterBuff : Character, IBuff
{

    [Header("Character Buff")]
    public List<Vector2Int> BuffList = new List<Vector2Int>(); // ���� �ڱ� ��ġ ����
    public Vector2Int thisPosIdx = new Vector2Int();
    public float BuffValue;

    public float value => BuffValue;

    // call when spawn or move end
    public void OnBuff()
    {

    }
}
