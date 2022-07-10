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
    public int index;
    public GameObject button;
    public int catIndex;
    Text levelText;
    Shadow shadow;
    Vector3 moveDirection;
    Vector3 dragStartPosition;
    CatCanvas catCanvas;
    bool isEquiped;

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

    private void Update()
    {

    }

    public void Init(int catIndex)
    {
        this.catIndex = catIndex;
        catImage.sprite = GameManager.Instance.catSprites[catIndex];
        levelText.text = GameManager.Instance.catDatas[catIndex].tier.ToString();
    }

    public void OnClickSelect()
    {
        catCanvas.UnSelect();
        button.SetActive(true);
    }
    public void UnSelect()
    {
        button.SetActive(false);

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
    public void OnClickInfo()
    {
        CatInfoPanel.Instance.Open(catIndex);
        button.SetActive(false);

    }
    public void OnClickUse()
    {
        if (!isEquiped)
        {
            catCanvas.SelectedCharacterIdx = catIndex;
            button.SetActive(false);
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {

        //CatInfoPanel.Instance.Open(catIndex);
    }
}