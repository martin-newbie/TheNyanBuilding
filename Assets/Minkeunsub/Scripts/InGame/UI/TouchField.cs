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
    private void Awake()
    {
        move = camPos.GetComponent<CameraMove>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = Camera.main.ScreenToWorldPoint(eventData.position);
        camStartPos = camPos.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPos = Camera.main.ScreenToWorldPoint(eventData.position);
        float distance = startPos.y - touchPos.y;

        camTargetPos.y = camStartPos.y + distance;
        camTargetPos.z = -100f;
        move.targetPos = camTargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InGameManager.Instance.TouchCharacter();
    }
}
