using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image iconImage;

    public bool addPressedEvent;

    public UnityEvent m_MyEvent;
    private Vector3 originScale;

    void Awake()
    {
        if (iconImage == null)
            iconImage = GetComponent<Image>();
        originScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //iconImage.transform.localScale = Vector3.one * 0.9f;
        transform.localScale = originScale * 0.9f;

        if (addPressedEvent)
        {
            if (cOnMouseDownAction != null)
                StopCoroutine(cOnMouseDownAction);
            cOnMouseDownAction = StartCoroutine(iOnMouseDownAction());
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //iconImage.transform.localScale = Vector3.one;
        transform.localScale = originScale;

        if (addPressedEvent)
        {
            if (cOnMouseDownAction != null)
            {
                StopCoroutine(cOnMouseDownAction);
                cOnMouseDownAction = null;
            }
        }
    }

    Coroutine cOnMouseDownAction;
    IEnumerator iOnMouseDownAction()
    {
        float interval = 0.5f;
        yield return new WaitForSeconds(interval);
        while (true)
        {
            interval *= 0.8f;
            if (interval < 0.1f)
                interval = 0.05f;
            m_MyEvent.Invoke();
            yield return new WaitForSeconds(interval);
        }
    }
    
}
