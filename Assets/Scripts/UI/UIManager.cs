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


    public void StartNewGame()
    {
        GameData.Reset();
        SceneManager.LoadScene(5);
    }
    public void LoadGame()
    {
        GameData.Load();
        SceneManager.LoadScene(5);
    }

    public void GoToMenu()
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

    public void ChangeGameState()
    {
        if (gameManager.isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene(0);
    }
}