using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCHint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public TextMeshProUGUI text;

    public void ShowsSpeakHint()
    {
        text.text = "E";
        canvas.SetActive(true);
    }
    public void ShowsItemInteractionHint()
    {
        text.text = "R";
        canvas.SetActive(true);
    }
    public void HideHint()
    {
        canvas.SetActive(false);
    }
}
