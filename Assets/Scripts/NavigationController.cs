using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationController : MonoBehaviour
{
    private static NavigationController instance;

    public static NavigationController Instance => instance;

    protected void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public Image[] barImage;
    public Sprite[] barImage_Clicked;
    public Sprite[] barImage_unClicked;
    public GameObject[] blockImages;
    public UpgradeCanvas upgradeCanvas;
    public CatCanvas catCanvas;
    public StoreCanvas storeCanvas;
    public int currentCanvasIndex;
    private void Start()
    {
        currentCanvasIndex = -1;
        //Init();
        for (int i = 0; i < blockImages.Length; i++)
            blockImages[i].SetActive(true);
    }



    // isSummonable -> arrow 생성
    public void Clicked(int index)
    {
        upgradeCanvas.gameObject.SetActive(false);
        catCanvas.gameObject.SetActive(false);
        storeCanvas.gameObject.SetActive(false);

        for (int i = 0; i < blockImages.Length; i++)
            blockImages[i].SetActive(true);


        switch (index)
        {
            case 0:
                if(currentCanvasIndex != index)
                {
                    blockImages[index].SetActive(false);
                    currentCanvasIndex = index;
                    upgradeCanvas.gameObject.SetActive(true);
                    upgradeCanvas.Open();
                    InGameManager.Instance.isUImoving = true;

                }
                else
                {
                    upgradeCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;
                    InGameManager.Instance.isUImoving = false;

                }
                break;
            case 1:
                if (currentCanvasIndex != index)
                {
                    blockImages[index].SetActive(false);
                    currentCanvasIndex = index;
                    catCanvas.gameObject.SetActive(true);
                    catCanvas.Open();
                    InGameManager.Instance.isUImoving = true;

                }
                else
                {
                    catCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;
                    InGameManager.Instance.isUImoving = false;

                }
                break;
            case 2:
                if (currentCanvasIndex != index)
                {
                    blockImages[index].SetActive(false);
                    currentCanvasIndex = index;
                    storeCanvas.gameObject.SetActive(true);
                    InGameManager.Instance.isUImoving = true;

                }
                else
                {
                    storeCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;
                    InGameManager.Instance.isUImoving = false;

                }
                break;
        }

    }
    

}
