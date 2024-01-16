using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public RectTransform detectionRatingScaleTransform;
    private float detectionStep;
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
    public NPCState leftNpcSpeaker;
    public NPCState rightNpcSpeaker;
    public IEnumerator coroutine;
    public NPCState smearedNPC;
    public TextMeshProUGUI info;
    public Image image;
    public TextMeshProUGUI name;
    public PlayerSpeak playerSpeak;
    public bool isCoroutineWas;


    void Start()
    {
        playerSpeak = GameObject.FindWithTag("Player").GetComponent<PlayerSpeak>();
        image = GameObject.Find("ItemImage").GetComponent<Image>();
        name = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        info = GameObject.Find("HowToUse").GetComponentInChildren<TextMeshProUGUI>();
        detectionStep = 5;
        detectionRatingScaleTransform = detectionRatingScale.GetComponent<RectTransform>();
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

        coroutine = Coroutine();
        // StartCoroutine(coroutine);
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
        if (showedValue <= 25)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.green;
            GameData.Chances = 3;
        }
        else if (showedValue <= 60)
        {
            detectionRatingScale.GetComponent<Image>().color = Color.yellow;
            GameData.Chances = 2;
        }
        else if (showedValue <= 95)
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

        var scaleSize = detectionRatingScaleTransform.sizeDelta;
        detectionRatingText.text = showedValue.ToString();
        detectionRatingScaleTransform.sizeDelta =
            new Vector2(showedValue * detectionStep,
                scaleSize.y);
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
        playerSpeak.StartSpeak("На сегодня все, нужно быстрее вернуться в свою комнату");
    }

    IEnumerator Coroutine()
    {
        var number = 0;
        foreach (var delay in new TestEnumerable())
        {
            DialogManager(number++);
            yield return delay;
        }
    }

    class TestEnumerable : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new TestEnumerator();
        }
    }

    class TestEnumerator : IEnumerator
    {
        public object Current => new WaitForSeconds(5);

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
        }
    }

    public void DialogManager(int number)
    {
        if (GameData.Day == 1)
        {
            if (number == 0)
            {
                leftNpcSpeaker.canSpeak = false;
                rightNpcSpeaker.canSpeak = false;
                leftNpcSpeaker.StartSpeak(
                    "Здравствуй, брат. Надеюсь, ты нашел в своей\nдуше спокойствие в эти благословенные дни.");
            }

            if (number == 1)
            {
                rightNpcSpeaker.StartSpeak(
                    "Здравствуй и ты, брат. Конечно, душа моя\nзамечательно пребывает в мире молитв и покаяния.");
            }

            if (number == 2)
            {
                leftNpcSpeaker.StartSpeak("Ну в твоем случае не лишним\nбудет уточнить, а то я тебя знаю.");
            }

            if (number == 3)
            {
                rightNpcSpeaker.StartSpeak("Опять ты за свое, почему ты так\nко мне относишься, я не понимаю.");
            }

            if (number == 4)
            {
                leftNpcSpeaker.canSpeak = true;
                rightNpcSpeaker.canSpeak = true;
                StopCoroutine(coroutine);
            }
        }

        if (GameData.Day == 2)
        {
            if (number == 0)
            {
                leftNpcSpeaker.canSpeak = false;
                rightNpcSpeaker.canSpeak = false;
                leftNpcSpeaker.StartSpeak(
                    " Брат, слышал ли ты об ужасном\nсобытии вчерашнего вечера?");
            }

            if (number == 1)
            {
                rightNpcSpeaker.StartSpeak(
                    "Что ты имеешь в виду?");
            }

            if (number == 2)
            {
                leftNpcSpeaker.StartSpeak(
                    "Похоже, кто-то из нас совершил убийство.\nПодумай, кто из монахов мог бы совершить такое?");
            }

            if (number == 3)
            {
                rightNpcSpeaker.StartSpeak("Брат, ты что, с ума сошел,\nразве мог слуга Божий сделать подобное?");
            }

            if (number == 4)
            {
                leftNpcSpeaker.StartSpeak("Говори, что угодно, но меня не обманешь,\nя знаю о мраке в твоей душе.");
            }

            if (number == 5)
            {
                rightNpcSpeaker.StartSpeak("Окстись и не неси чепухи.");
            }

            if (number == 6)
            {
                leftNpcSpeaker.canSpeak = true;
                rightNpcSpeaker.canSpeak = true;
                StopCoroutine(coroutine);
            }
        }

        if (GameData.Day == 3)
        {
            if (number == 0)
            {
                leftNpcSpeaker.canSpeak = false;
                rightNpcSpeaker.canSpeak = false;
                leftNpcSpeaker.StartSpeak(
                    "Как думаешь, кто стоит за этим ужасом?\nНеужели ты не видишь, что это кто-то из наших?");
            }

            if (number == 1)
            {
                rightNpcSpeaker.StartSpeak(
                    "Ты, кажется, умеешь только обвинять других.\nМожет быть, ты сам причастен к этим злодеяниям?");
            }

            if (number == 2)
            {
                leftNpcSpeaker.StartSpeak("Не кати на меня бочку,\nты можешь об этом пожалеть!");
            }

            if (number == 3)
            {
                rightNpcSpeaker.StartSpeak(
                    "Мы все знаем, как твое сердце исполнено зависти и ненависти.\nНе удивительно, что темные силы нашли тебя.");
            }

            if (number == 4)
            {
                leftNpcSpeaker.canSpeak = true;
                rightNpcSpeaker.canSpeak = true;
                StopCoroutine(coroutine);
            }
        }
    }

    public void ShowLusterInfo(bool show = true)
    {
        if (show)
        {
            info.text =
                "Е - Поговорить/ Открыть дверь\nF - Поднять предмет\nR - Использовать предмет\nT - Выбросить предмет\nQ - уронить люстру";
            // info.transform.parent.GetComponent<RectTransform>().sizeDelta += Vector2.up * 27;
        }
        else
        {
            info.text =
                "Е - Поговорить/ Открыть дверь\nF - Поднять предмет\nR - Использовать предмет\nT - Выбросить предмет";
            // info.transform.parent.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 27;
        }
    }

    public void ShowItem(Sprite itemImage, string itemName)
    {
        image.enabled = true;
        name.enabled = true;
        image.sprite = itemImage;
        name.text = itemName;
        image.preserveAspect = true;
    }
    public void HideItem()
    {
        image.enabled = false;
        name.enabled = false;
    }
}