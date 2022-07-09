using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBox : MonoBehaviour
{
    Animator animator;
    CatInfo catInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (GetComponentInChildren<CatInfo>())
            catInfo = GetComponentInChildren<CatInfo>();
    }
    public void Init(int catIndex)
    {
        //archerInfo.gameObject.SetActive(false);
        catInfo.Init(catIndex);
        animator.Play("SummonEffect", -1, 0f);
    }


}
