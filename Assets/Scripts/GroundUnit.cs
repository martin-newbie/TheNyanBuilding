using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnit : MonoBehaviour
{
    public CatBlock[] catBlocks;
    int groundIndex;
    void Start()
    {
        for (int i = 0; i < catBlocks.Length; i++)
        {
            catBlocks[i].gameObject.SetActive(false);
        }

    }

    public void GroundSetting(int groundIndex)
    {
        this.groundIndex = groundIndex;
        for (int i = 0; i < catBlocks.Length; i++)
        {
            catBlocks[i].gameObject.SetActive(true);
            catBlocks[i].GrountInit(groundIndex,i);

        }

    }


}
