using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] Image gaugeImg;
    float tar, cur, max;
    Transform target;
    Vector3 offset;

    private void Update()
    {
        cur = Mathf.Lerp(cur, tar, Time.deltaTime * 5f);
        gaugeImg.fillAmount = cur / max;

        if (target != null)
            transform.position = target.position + offset;
    }

    public void SetPos(Vector3 _offset, Transform _target)
    {
        target = _target;
        offset = _offset;
    }

    public void SetGaugeFill(float _tar, float _max)
    {
        tar = _tar;
        max = _max;
    }
}
