using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    public GameObject hint;

    public void ShowHint()
    {
        hint.SetActive(true);
    }
    public void HideHint()
    {
        hint.SetActive(false);
    }
}
