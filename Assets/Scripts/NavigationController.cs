using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationController : MonoBehaviour
{
    public Image[] barImage;
    public Sprite[] barImage_Clicked;
    public Sprite[] barImage_unClicked;
    public UpgradeCanvas upgradeCanvas;
    public CatCanvas catCanvas;
    public StoreCanvas storeCanvas;
    private int currentCanvasIndex;
    private void Start()
    {
        currentCanvasIndex = -1;
        //Init();
        for ( int i=0; i< barImage.Length; i++)
            barImage[i].sprite = barImage_unClicked[i];
    }



    // isSummonable -> arrow 생성
    public void Clicked(int index)
    {
        upgradeCanvas.gameObject.SetActive(false);
        catCanvas.gameObject.SetActive(false);
        storeCanvas.gameObject.SetActive(false);

        for (int i = 0; i < barImage.Length; i++)
            barImage[i].sprite = barImage_unClicked[i];


        switch (index)
        {
            case 0:
                if(currentCanvasIndex != index)
                {
                    barImage[index].sprite = barImage_Clicked[index];
                    currentCanvasIndex = index;
                    upgradeCanvas.gameObject.SetActive(true);
                    upgradeCanvas.Open();

                }
                else
                {
                    upgradeCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;

                }
                break;
            case 1:
                if (currentCanvasIndex != index)
                {
                    barImage[index].sprite = barImage_Clicked[index];
                    currentCanvasIndex = index;
                    catCanvas.gameObject.SetActive(true);
                    catCanvas.Open();

                }
                else
                {
                    catCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;

                }
                break;
            case 2:
                if (currentCanvasIndex != index)
                {
                    barImage[index].sprite = barImage_Clicked[index];
                    currentCanvasIndex = index;
                    storeCanvas.gameObject.SetActive(true);

                }
                else
                {
                    storeCanvas.gameObject.SetActive(false);
                    currentCanvasIndex = -1;

                }
                break;
        }

    }
    

}
