using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public Image CartoonImg;
    public Button HelpButton;
    public Image TutorialImg;
    bool isActive;

    private void Start()
    {
        InGameManager.Instance.isUImoving = true;
        CartoonImg.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isActive)
        {
            StartCoroutine(FadeOut(0.5f));
            isActive = true;
        }

        if (TutorialImg.gameObject.activeSelf && !trigger && Input.GetMouseButtonDown(0))
        {
            trigger = true;
        }
    }

    bool trigger;

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

        CartoonImg.gameObject.SetActive(false);

        while (!trigger) yield return null;
        trigger = false;
        HelpButton.gameObject.SetActive(false);

        TutorialImg.gameObject.SetActive(true);

        while (!trigger) yield return null;

        TutorialImg.gameObject.SetActive(false);
        InGameManager.Instance.isUImoving = false;
    }

    public void HelpButtonFunc()
    {
        trigger = true;
    }

}
