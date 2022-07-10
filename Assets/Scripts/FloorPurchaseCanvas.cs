using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPurchaseCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private static FloorPurchaseCanvas instance;

    public static FloorPurchaseCanvas Instance => instance;

    protected void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Open()
    {
        this.GetComponent<Canvas>().enabled = true;
        InGameManager.Instance.isUImoving = true;


    }
    public void OnClickCancel()
    {
        this.GetComponent<Canvas>().enabled = false;
        InGameManager.Instance.isUImoving = false;

    }
    public void OnClickPurchase()
    {
        if(GameManager.Instance.UseProperty(Property.Churu ,100))
        {
            InGameManager.Instance.UnlockFloor();
            this.GetComponent<Canvas>().enabled = false;
            InGameManager.Instance.isUImoving = false;

        }

    }
}
