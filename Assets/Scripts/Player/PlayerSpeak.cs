using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeak : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogWindow;
    public TextMeshProUGUI dialogMessage;

    void Start()
    {
        dialogWindow = transform.Find("DialogWindow").gameObject;
        dialogMessage = dialogWindow.transform.Find("Background").Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSpeak(string phrase)
    {
        dialogMessage.text = phrase;
        dialogWindow.SetActive(true);
        Invoke(nameof(StopSpeak), 5f);
    }

    public void StopSpeak()
    {
        dialogWindow.SetActive(false);
    }
}