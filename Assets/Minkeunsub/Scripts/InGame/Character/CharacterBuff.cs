using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuff : Character, IBuff
{

    [Header("Character Buff")]
    public List<Vector2> BuffList = new List<Vector2>(); // ���� �ڱ� ��ġ ����
    public Vector2 thisPosIdx = new Vector2();

    public void OnBuff()
    {

    }
}
