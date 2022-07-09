using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class CatBlock : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image catImage;
    public int catPositionIndex;
    public bool isClicked;

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


    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CatInfoPanel.Instance.Open(catIndex);
    }
}