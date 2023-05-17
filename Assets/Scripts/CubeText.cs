using UnityEngine;
using TMPro;

public class CubeText : MonoBehaviour
{
    private TMP_Text[] texts;
    public void Init()
    {
        texts = GetComponentsInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        foreach (var txt in texts)
        {
            txt.SetText(text, true);
        }
    }
}
