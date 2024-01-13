using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public bool isCoinHere;

    public void ShowHint()
    {
        canvas.SetActive(true);
    }

    public void HideHint()
    {
        canvas.SetActive(false);
    }
}