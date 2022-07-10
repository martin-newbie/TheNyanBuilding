using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCanvas : MonoBehaviour
{
    public Animator animator;
    public CatBlock[] catBlocks;

    bool isSelect;
    private int selectedIdx;
    public int SelectedCharacterIdx
    {
        get
        {
            return selectedIdx;
        }
        set
        {
            selectedIdx = value;
            Close();
            Invoke("Select", 0.5f);
        }
    }

    public int curButtonCnt;

    void Select()
    {
        isSelect = true;
    }

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

    private void Update()
    {
        if (isSelect && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (InGameManager.Instance.GetAblePos(mousePos))
            {
                InGameManager.Instance.SpawnCharacterAsIndex(selectedIdx, mousePos);
                catBlocks[selectedIdx].isEquiped = true;
            }
            selectedIdx = -1;
            isSelect = false;
        }
    }

    public void Open()
    {
        InGameManager.Instance.isUImoving = true;
        animator.SetFloat("speed", 1);
        animator.Play("OpenCat", -1);

    }
    public void Close()
    {
        InGameManager.Instance.isUImoving = false;
        animator.SetFloat("speed", -1);
        animator.Play("OpenCat", -1);
        NavigationController.Instance.blockImages[1].SetActive(true);
        InGameManager.Instance.isUImoving = false;
        NavigationController.Instance.currentCanvasIndex = -1;


    }
    private void OnDisable()
    {
        UnSelect();

    }

    public void UnSelect()
    {
        for (int i = 0; i < catBlocks.Length; i++)
        {
            catBlocks[i].UnSelect();
        }
    }
}
