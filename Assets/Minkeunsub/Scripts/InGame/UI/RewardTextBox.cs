using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTextBox : MonoBehaviour
{
    public TextMesh Text;
    public SpriteRenderer Icon;

    public void Init(string value, Vector3 pos)
    {
        Text.text = value;
        transform.position = pos;

        StartCoroutine(TextEffect(1f));
    }

    IEnumerator TextEffect(float duration)
    {
        yield return new WaitForSeconds(1.5f);

        float timer = duration;
        Color txtColor = Text.color;
        Color spriteColor = Icon.color;

        while (timer > 0f)
        {
            txtColor.a = timer / duration;
            Text.color = txtColor;

            spriteColor.a = timer / duration;
            Icon.color = spriteColor;


            timer -= Time.deltaTime;
            yield return null;
        }

        txtColor.a = 0f;
        Text.color = txtColor;

        spriteColor.a = 0f;
        Icon.color = spriteColor;

        yield break;
    }
}
