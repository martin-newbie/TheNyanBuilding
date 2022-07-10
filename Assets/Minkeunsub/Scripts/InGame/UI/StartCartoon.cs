using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCartoon : MonoBehaviour
{

    public Image CartoonImg;
    bool isActive;

    private void Start()
    {
        InGameManager.Instance.isUImoving = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isActive)
        {
            StartCoroutine(FadeOut(0.5f));
            isActive = true;
        }
    }

    IEnumerator FadeOut(float duration)
    {
        float timer = duration;
        Color color = CartoonImg.color;

        while (timer > 0f)
        {
            color.a = timer / duration;
            CartoonImg.color = color;

            timer -= Time.deltaTime;
            yield return null;
        }

        InGameManager.Instance.isUImoving = false;
        gameObject.SetActive(false);
    }

}
