using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float yMax;
    public float yMin = 0f;

    public Vector3 targetPos;

    void Start()
    {
        yMax = (InGameManager.Instance.y * 2f - 1f) - Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        Vector3 vec = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 15f);
        transform.position = vec;

        float y = transform.position.y;

        if(y > yMax)
        {
            y = yMax;
        }
        else if(y < yMin)
        {
            y = yMin;
        }
        transform.position = new Vector3(0, y, -10f);
    }
}
