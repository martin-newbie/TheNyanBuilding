using UnityEngine;
using UnityEngine.UI;

public class LocalizePetName : MonoBehaviour
{
    public int keyIndex;
    private void Start()
    {
        GetComponent<Text>().text = LocalizeDataManager.GetPetName(keyIndex).Replace("\\n", "\n");
    }
}
