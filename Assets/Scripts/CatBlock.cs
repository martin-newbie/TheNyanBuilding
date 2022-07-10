using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class CatBlock : MonoBehaviour
{
    public Image catImage;
    public int catPositionIndex;
    public bool isClicked;
    public GameObject button;

    int catIndex;
    int index;
    Text levelText;
    Shadow shadow;
    Vector3 moveDirection;
    Vector3 dragStartPosition;
    CatCanvas catCanvas;


    void Awake()
    {
        catCanvas = GetComponentInParent<CatCanvas>();
        levelText = GetComponentInChildren<Text>();
        shadow = GetComponentInChildren<Shadow>();
        index = transform.GetSiblingIndex();
    }

    void Start()
    {
        isClicked = false;
    }



    public void Init(int catIndex)
    {
        this.catIndex = catIndex;
        catImage.sprite = GameManager.Instance.catSprites[catIndex];
        levelText.text = GameManager.Instance.catDatas[catIndex].tier.ToString();
    }

    public void CatSelected()
    {
        catCanvas.UnSelect();
        button.SetActive(true);
    }


    public void CatInfo()
    {
        button.SetActive(false);

        CatInfoPanel.Instance.Open(catIndex);
    }
    public void UseCat()
    {
        button.SetActive(false);

    }
    public void UnSelect()
    {
        button.SetActive(false);

    }
}