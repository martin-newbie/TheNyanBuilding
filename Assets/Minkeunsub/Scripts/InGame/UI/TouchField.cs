using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Vector3 startPos;
    Vector3 camStartPos;
    Vector3 touchPos;
    Vector3 camTargetPos;
    public Transform camPos;
    CameraMove move;
    bool isDrag;
    private void Awake()
    {
        move = camPos.GetComponent<CameraMove>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!InGameManager.Instance.isDragging)
        {
            startPos = Camera.main.ScreenToWorldPoint(eventData.position);
            camStartPos = camPos.position;
            isDrag = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            touchPos = Camera.main.ScreenToWorldPoint(eventData.position);
            float distance = startPos.y - touchPos.y;

            camTargetPos.y = camStartPos.y + distance;
            camTargetPos.z = -100f;
            move.targetPos = camTargetPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InGameManager.Instance.TouchCharacter();
    }
}
