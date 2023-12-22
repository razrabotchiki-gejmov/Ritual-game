using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerName;
    public GameObject superiorName;
    public GameObject phrase;
    public GameObject answer1;
    public GameObject answer2;
    public List<MyClass> list = new();
    public int chances = 3;
    public int index;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;

    void Start()
    {
        list = new List<MyClass>()
        {
            new(0, "Приветствую, сын мой. Я пришел сообщить пренеприятнейшую новость.", "", ""),
            new(1, "Что же случилось, Отец?", "", ""),
            new(0, "Один из наших братьев вчера погиб, об этом стало известно совсем недавно…", "", ""),
            new(1, "Какой ужас, что с ним произошло?", "", ""),
            new(0,
                "Надо сказать, что его увечья говорят о насильственной смерти, мы подозреваем, что его мог кто-то убить.",
                "", ""),
            new(1, "Но кто мог это сделать, я уверен в каждом человеке в этом храме.", "", ""),
            new(0, "Скажи честно, сын мой, ты знаешь что-то об этом?", "", ""),
            new[]
            {
                new MyClass(2, "", "Никак нет, Отец, я слышу эту новость впервые от тебя.",
                    "Да, Отче, это я убил его!"),
                new MyClass(2, "", "Мне кажется, я мог видеть вчера что-то странное, но ничего конкретного",
                    "Я видел, как закатываются его глаза, а потом обмякает и все тело."),
                new MyClass(2, "", "Лично я нет, но возможно кто-то из братьев мог что-то видеть.",
                    "Да, Отец, начало положено, скоро я доберусь и до остальных.")
            }[Random.Range(0, 2)],
            new(0, "Скажи мне как есть, что ты делал вчера?", "", ""),
            new[]
            {
                new MyClass(2, "", "Ничего необычного, всего лишь ходил на молитвы, это могут подтвердить и другие.",
                    "Я искал и выжидал нужный момент, чтобы ударить."),
                new MyClass(2, "",
                    "Честно говоря, ко мне в голову закрадываются иногда мысли о всяких непотребствах, но не о каком убийстве речи и не идет.",
                    "Я представлял, как окроплю кровью своей жертвы весь храм!"),
                new MyClass(2, "", "Я помогал своим братьям с делами по храму.",
                    "После хладнокровного убийства я еще ходил и высматривал себе следующую жертву.")
            }[Random.Range(0, 2)],
            new(0, "Как думаешь это может повториться снова?", "", ""),
            new[]
            {
                new MyClass(2, "", "Я очень надеюсь, что нет. Я сделаю все, что в моих силах дабы избежать этого.",
                    "О, конечно, повториться и очень скоро, оно кричит и требует крови."),
                new MyClass(2, "",
                    "Сложно сказать, но в любом случае нужно быть крайте осторожным, мы не знаем где прячется убийца.",
                    "Я думаю нам всем стоит быть осторожными, ведь зверь внутри не дремлет, я это чувствую. Кто знает, когда он покажет зубы в следующий раз."),
                new MyClass(2, "",
                    "Возможно это все же не убийство, а несчастный случай и нам не о чем беспокоиться.",
                    "Смерть преследует нас по пятам, одних она застегает в постели в преклонном возрасте, а к другим подкрадывается незаметно из-за угла с ножом в руках.")
            }[Random.Range(0, 2)],
            new(0, "Хорошо я понял, спасибо за честность! Ну будь здоров, мне нужно идти.", "", ""),
            new(1, "И вы будьте, до встречи. ", "", ""),
        };
        ChangeDialogState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (list[index - 1].Type == 2)
            {
                return;
            }

            if (index < list.Count)
            {
                ChangeDialogState();
            }
            else
            {
                EndDialog();
            }
        }
    }

    public class MyClass
    {
        public int Type;

        // 0 - говорит настоятель, 1 - говорит игрок, 2 - отвечает игрокж 
        public string Phrase;
        public string Answer1;
        public string Answer2;

        public MyClass(int type, string phrase, string answer1, string answer2)
        {
            Type = type;
            Phrase = phrase;
            Answer1 = answer1;
            Answer2 = answer2;
        }
    }

    public void ChangeDialogState()
    {
        var myClass = list[index];
        if (myClass.Type == 0)
        {
            playerName.SetActive(false);
            answer1.SetActive(false);
            answer2.SetActive(false);
            phrase.GetComponent<TextMeshProUGUI>().text = myClass.Phrase;
            superiorName.SetActive(true);
            phrase.SetActive(true);
        }

        if (myClass.Type == 1)
        {
            superiorName.SetActive(false);
            answer1.SetActive(false);
            answer2.SetActive(false);
            playerName.SetActive(true);
            phrase.GetComponent<TextMeshProUGUI>().text = myClass.Phrase;
            phrase.SetActive(true);
        }

        if (myClass.Type == 2)
        {
            superiorName.SetActive(false);
            phrase.SetActive(false);
            playerName.SetActive(true);
            answer1.GetComponent<TextMeshProUGUI>().text = "1) " + myClass.Answer1;
            answer2.GetComponent<TextMeshProUGUI>().text = "2) " + myClass.Answer2;
            answer1.SetActive(true);
            answer2.SetActive(true);
        }

        index++;
    }

    public void AnsweredIncorrectly()
    {
        chances -= 1;
    }

    public void EndDialog()
    {
        if (chances <= 0)
        {
            Loose();
        }
        else
        {
            if (GameData.Day == 3)
            {
                Win();
            }
            else
            {
                StartDay();
            }
        }
    }

    public void StartDay()
    {
        {
            GameData.Day += 1;
            SceneManager.LoadScene(GameData.Day);
        }
    }

    public void Loose()
    {
        gameOverMenu.SetActive(true);
    }

    public void Win()
    {
        victoryMenu.SetActive(true);
    }
}