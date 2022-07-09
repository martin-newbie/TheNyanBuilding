using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] Image gaugeImg;
    float tar, cur, max;

    private void Update()
    {
        cur = Mathf.Lerp(cur, tar, Time.deltaTime * 5f);
        gaugeImg.fillAmount = cur / max;
    }

    public void SetPof(Vector3 offset, Transform target)
    {
        transform.position = target.position + offset;
    }

    public void SetGuageFill(float _tar, float _max)
    {
        tar = _tar;
        max = _max;
    }
}
