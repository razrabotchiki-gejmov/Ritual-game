using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDay : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = "День " + GameData.Day;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartDay();
        }
    }

    public void StartDay()
    {
        GameData.Save();
        SceneManager.LoadScene(GameData.Day);
    }
}