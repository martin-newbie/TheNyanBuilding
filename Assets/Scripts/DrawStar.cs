using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawStar : MonoBehaviour
{
    public GameObject[] stars;
    RectTransform[] rectTransforms;
    float size;
    int tier;
    private void Awake()
    {
        rectTransforms = new RectTransform[stars.Length];
        for (int i = 0; i < stars.Length; i++)
        {
            rectTransforms[i] = stars[i].GetComponent<RectTransform>();
        }
        size = rectTransforms[0].sizeDelta.x;
    }

    public void Init(int count)
    {
        cPromote = null;
        tier = count;
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < count)
                stars[i].SetActive(true);
            else
                stars[i].SetActive(false);
        }
        if (rectTransforms != null)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < count)
                    rectTransforms[i].sizeDelta = Vector3.one * size;
                else
                    rectTransforms[i].sizeDelta = Vector3.one * 0f;
            }
        }
    }

    public void Promote()
    {
        if (tier < 5)
        {
            stars[tier].SetActive(true);
            if (cPromote != null)
                StopCoroutine(cPromote);
            cPromote = StartCoroutine(iPromote(rectTransforms[tier]));
        }
    }

    Coroutine cPromote;
    IEnumerator iPromote(RectTransform rect)
    {
        float time = 0f;
        float duration = 0.4f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rect.sizeDelta = Vector3.one * size * 2f * time / duration;
            yield return null;
        }
        time = 0f;
        duration = 0.15f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rect.sizeDelta = Vector3.one * size * (2f - 1f * time / duration);
            yield return null;
        }
        rect.sizeDelta = Vector3.one * size;
        cPromote = null;
    }
}
