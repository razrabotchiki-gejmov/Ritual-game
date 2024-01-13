using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorHint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public TextMeshProUGUI text;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoorOpenCloseHint()
    {
        text.text = "E";
        canvas.SetActive(true);
    }

    public void DoorUnlockHint()
    {
        text.text = "R";
        canvas.SetActive(true);
    }

    public void CloseHint()
    {
        canvas.SetActive(false);
    }
}