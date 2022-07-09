using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCanvas : MonoBehaviour
{
    public Animator animator;
    public UpgradeInfo[] upgradeInfos;
    List<UpgradeData> upgradeList;
    private void Start()
    {
        upgradeList = StaticDataManager.Instance.upgradeData.datas;

        Init();
    }
    public void Init()
    {
        int count = 0;
        for (int i = 0; i < upgradeInfos.Length; i++)
        {
            upgradeInfos[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < upgradeList.Count; i++)
        {
            //if (upgradeList[i].upgradeType == index)
            {
                upgradeInfos[count].gameObject.SetActive(true);
                upgradeInfos[count++].Init(i, i);
            }
        }
    }
    public void Open()
    {
        animator.SetFloat("speed", 1);
        animator.Play("OpenUpgrade",-1);

    }
    public void Close()
    {
        animator.SetFloat("speed",-1);
        animator.Play("OpenUpgrade",-1);
    }
}
