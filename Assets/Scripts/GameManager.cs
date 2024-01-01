using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int detectionRating;
    public GameObject endDayPoint;
    public GameObject detectionRatingScale;
    public GameObject invisibilityTimeScale;
    public TextMeshProUGUI detectionRatingText;
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject conviction;
    public GameObject invisibility;
    public GameObject superpower;
    public bool canUseConviction;
    public bool canUseInvisibility;
    public bool canUseSuperpower;
    public bool isSomeoneKilledDirectly;

    void Start()
    {
        endDayPoint = GameObject.FindWithTag("Finish");
        canUseConviction = true;
        canUseInvisibility = true;
        canUseSuperpower = true;
        if (GameData.Day < 3)
        {
            BecomeOutOfUse(3);
        }

        if (GameData.Day < 2)
        {
            BecomeOutOfUse(2);
        }
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
        detectionRating += value;
        var showedValue = detectionRating;
        if (showedValue <= 33)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.green;
            GameData.Chances = 3;
        }
        else if (showedValue <= 66)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.yellow;
            GameData.Chances = 2;
        }
        else if (showedValue <= 99)
        {
            detectionRatingScale.GetComponent<Image>().color = new Color(1, 0.4f, 0);
            GameData.Chances = 1;
        }
        else
        {
            detectionRatingScale.GetComponent<Image>().color = Color.red;
            showedValue = 100;
            GameData.Chances = 0;
        }

        detectionRatingText.text = showedValue.ToString();
        detectionRatingScale.GetComponent<RectTransform>().sizeDelta = new Vector2(showedValue * 4, 20);
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

    public void BecomeOutOfUse(int number)
    {
        if (number == 1)
        {
            var color = conviction.GetComponent<Image>().color;
            color.a = 0.3f;
            conviction.GetComponent<Image>().color = color;
            canUseConviction = false;
        }

        if (number == 2)
        {
            var color = invisibility.GetComponent<Image>().color;
            color.a = 0.4f;
            invisibility.GetComponent<Image>().color = color;
            canUseInvisibility = false;
        }

        if (number == 3)
        {
            var color = superpower.GetComponent<Image>().color;
            color.a = 0.4f;
            superpower.GetComponent<Image>().color = color;
            canUseSuperpower = false;
        }
    }

    public void DecreaseInvisibilityTimeScale(float currentTime, float maxTime)
    {
        var percent = currentTime / maxTime;
        invisibilityTimeScale.GetComponent<RectTransform>().sizeDelta = new Vector2(percent * 400, 20);
    }

    public void ShowInvisibilityTimeScale()
    {
        invisibilityTimeScale.SetActive(true);
    }

    public void HideInvisibilityTimeScale()
    {
        invisibilityTimeScale.SetActive(false);
    }

    public void SomeoneDied()
    {
        endDayPoint.GetComponent<SpriteRenderer>().enabled = true;
        endDayPoint.GetComponent<CircleCollider2D>().enabled = true;
    }
}