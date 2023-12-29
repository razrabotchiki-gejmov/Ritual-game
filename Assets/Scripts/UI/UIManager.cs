using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject info;
    public GameObject menu;
    public GameManager gameManager;

    void Start()
    {
    }

    // Update is called once per frame


    public void StartTheGame()
    {
        GameData.Reset();
        SceneManager.LoadScene(1);
    }

    public void GoTeMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void ShowInfo()
    {
        info.SetActive(true);
    }

    public void Resume()
    {
        gameManager.Resume();
    }

    public void Pause()
    {
        gameManager.Pause();
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene(0);
    }
}