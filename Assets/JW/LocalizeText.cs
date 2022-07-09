using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    public int keyIndex;
    private void Start()
    {
       GetComponent<Text>().text = LocalizeDataManager.GetLocalizeString(keyIndex).Replace("\\n", "\n");
    }
}
