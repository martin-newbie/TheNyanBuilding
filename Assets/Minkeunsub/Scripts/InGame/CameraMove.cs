using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float yMax;
    public float yMin = 0f;

    void Start()
    {
        yMax = (InGameManager.Instance.y * 2f - 1f) - Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        float y = transform.position.y;

        if(y > yMax)
        {
            y = yMax;
        }
        else if(y < yMin)
        {
            y = yMin;
        }
        transform.position = new Vector2(0, y);
    }
}
