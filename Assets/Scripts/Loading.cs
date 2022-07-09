using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Text startText;
    public AudioSource audioSource;
    Color startColor;

    void Start()
    {
        startColor = startText.color;
        cStartButton = StartCoroutine(iStartButton());

    }

    Coroutine cStartButton;
    IEnumerator iStartButton()
    {
        float time = 0f;
        float duration = 1f;
        while (true)
        {
            time += Time.deltaTime;
            yield return null;

            if (time < duration)
            {
                startText.color = Color.Lerp(startColor, Color.clear, time);
            }
            else if (time < duration * 2f)
            {
                startText.color = Color.Lerp(Color.clear, startColor, time - duration);
            }
            else
                time = 0f;
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Ingame");
    }
}
