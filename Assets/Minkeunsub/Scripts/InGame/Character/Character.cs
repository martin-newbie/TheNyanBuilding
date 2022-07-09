using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterValue
{
    public float[] rewardChance;
    public float[] rewardAmount;
    public int idx;
}

public abstract class Character : MonoBehaviour
{
    [Header("Info")]
    public CharacterValue info;
    public CharacterType characterType;

    [Header("Componenet")]
    public SpriteRenderer BodySR;
    public SpriteRenderer HeadSR;

    [Header("Animation")]
    public Sprite[] BodyAnimationFrame;
    public float frameDelay = 0.05f;

    [Header("Value")]
    public bool isDrag;
    public Vector2Int thisPosIdx = new Vector2Int();

    Coroutine animationCoroutine;

    private void Awake()
    {
        BodySR = GetComponentsInChildren<SpriteRenderer>()[0];
        HeadSR = GetComponentsInChildren<SpriteRenderer>()[1];

        PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimationPlay(BodyAnimationFrame));
    }

    public void PlayHovereAnimation()
    {
        // todo: stop default coroutine and play hover coroutine
    }

    IEnumerator AnimationPlay(Sprite[] sprites)
    {
        int idx = 0;
        while (true)
        {
            BodySR.sprite = sprites[idx];
            yield return new WaitForSeconds(frameDelay);

            if (idx < sprites.Length - 1) idx++;
            else idx = 0;
        }
    }
}
