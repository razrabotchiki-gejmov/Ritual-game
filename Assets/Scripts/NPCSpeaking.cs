using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeaking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSpeak()
    {
        transform.Find("DialogWindow").gameObject.SetActive(true);
        Invoke(nameof(StopSpeak), 5f);
    }

    public void StopSpeak()
    {
        transform.Find("DialogWindow").gameObject.SetActive(false);
    }
}