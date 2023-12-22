using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject detectionRatingScale;
    public TextMeshProUGUI detectionRatingText;
    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void IncreaseDetectionRating(int value)
    {
        var currentValue = int.Parse(detectionRatingText.text);
        var newValue = currentValue + value;
        if (newValue <= 33)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.green;
        }
        else if (newValue <= 66)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.yellow;
        }
        else if (newValue <= 99)
        {
            detectionRatingScale.GetComponent<Image>().color = new Color(1, 0.4f, 0);
        }
        else
        {
            detectionRatingScale.GetComponent<Image>().color = Color.red;
            newValue = 100;
        }

        detectionRatingText.text = newValue.ToString();
        detectionRatingScale.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue * 4, 20);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void EndDay()
    {
        SceneManager.LoadScene(4);
    }
}