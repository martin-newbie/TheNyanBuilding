using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCanvas : MonoBehaviour
{

    public void GetChur()
    {
        GameManager.Instance.churu += 1000;
    }
    
}
