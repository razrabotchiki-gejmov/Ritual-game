using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isBad;
    public Dialog dialog;

    void Start()
    {
        dialog = GetComponentInParent<Dialog>();
    }

    public void Answered()
    {
        if (isBad) dialog.isPlayerFailed = true;
    }
}