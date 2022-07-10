using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCanvas : MonoBehaviour
{
    public Animator animator;
    public CatBlock[] catBlocks;
    private void Start()
    {
        EventManager.StartListening("CatSetting", Setting);

        for (int i = 0; i < catBlocks.Length; i++)
        {
            catBlocks[i].gameObject.SetActive(false);
        }
        Setting();
    }
    public void Setting()
    {
        for (int i = 1; i < GameManager.Instance.catDatas.Count; i++)
        {
            UserData_Cat userData_Cat = GameManager.Instance.catDatas[i];
            if (userData_Cat.tier > 0 && !GameManager.Instance.selectedCat.Contains(i))
            {
                catBlocks[i].gameObject.SetActive(true);
                catBlocks[i].Init(userData_Cat.key_index);

            }

        }
    }
    public void UnSelect()
    {
        for (int i = 0; i < catBlocks.Length; i++)
        {
            catBlocks[i].UnSelect();
        }
    }

    public void Open()
    {
        animator.SetFloat("speed", 1);
        animator.Play("OpenCat", -1);
        InGameManager.Instance.isUImoving = true;
    }
    public void Close()
    {
        animator.SetFloat("speed",-1);
        animator.Play("OpenCat", -1);
        InGameManager.Instance.isUImoving = false;

    }
}
