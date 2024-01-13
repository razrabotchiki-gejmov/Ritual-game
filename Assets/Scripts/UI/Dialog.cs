﻿using System.Collections;
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
    public GameObject answer3;
    private TextMeshProUGUI phraseText;
    private TextMeshProUGUI answer1Text;
    private TextMeshProUGUI answer2Text;
    private TextMeshProUGUI answer3Text;
    public List<MyClass> list = new();
    public int index;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;
    private int startChances;

    void Start()
    {
        startChances = GameData.Chances;
        phraseText = phrase.GetComponent<TextMeshProUGUI>();
        answer1Text = answer1.GetComponentInChildren<TextMeshProUGUI>();
        answer2Text = answer2.GetComponentInChildren<TextMeshProUGUI>();
        answer3Text = answer3.GetComponentInChildren<TextMeshProUGUI>();
        GetPhrasesList();
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
        public string Answer3;

        // public MyClass(int type, string phrase, string answer1, string answer2, string answer3)
        // {
        //     Type = type;
        //     Phrase = phrase;
        //     Answer1 = answer1;
        //     Answer2 = answer2;
        // }
        public MyClass(int type, string phrase)
        {
            Type = type;
            Phrase = phrase;
        }

        public MyClass(string goodAnswer1, string badAnswer1, string goodAnswer2, string badAnswer2, string goodAnswer3,
            string badAnswer3)
        {
            Type = 2;
            if (GameData.Chances >= 1) Answer1 = goodAnswer1;
            else Answer1 = badAnswer1;
            if (GameData.Chances >= 2) Answer2 = goodAnswer2;
            else Answer2 = badAnswer2;
            if (GameData.Chances >= 3) Answer3 = goodAnswer3;
            else Answer3 = badAnswer3;
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
            answer3.SetActive(false);
            phraseText.text = myClass.Phrase;
            superiorName.SetActive(true);
            phrase.SetActive(true);
        }

        else if (myClass.Type == 1)
        {
            superiorName.SetActive(false);
            answer1.SetActive(false);
            answer2.SetActive(false);
            answer3.SetActive(false);
            playerName.SetActive(true);
            phraseText.text = myClass.Phrase;
            phrase.SetActive(true);
        }
        else
        {
            superiorName.SetActive(false);
            phrase.SetActive(false);
            playerName.SetActive(true);
            answer1Text.text = myClass.Answer1;
            answer2Text.text = myClass.Answer2;
            answer3Text.text = myClass.Answer3;
            answer1.SetActive(true);
            answer2.SetActive(true);
            answer3.SetActive(true);
            if (startChances <= 0) answer1.GetComponent<Answer>().isBad = true;
            if (startChances <= 1) answer2.GetComponent<Answer>().isBad = true;
            if (startChances <= 2) answer3.GetComponent<Answer>().isBad = true;
        }

        index++;
    }


    public void EndDialog()
    {
        if (GameData.Chances <= 0)
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
                StartDayPanel();
            }
        }
    }

    public void StartDayPanel()
    {
        {
            GameData.Day += 1;
            GameData.Chances = 3;
            // SceneManager.LoadScene(GameData.Day);
            SceneManager.LoadScene(5);
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

    public void GetPhrasesList()
    {
        if (GameData.Day == 1)
        {
            list = new List<MyClass>
            {
                new(0, "Приветствую, сын мой. Я пришел сообщить пренеприятнейшую новость."),
                new(1, "Что же случилось, Отец?"),
                new(0, "Один из наших братьев вчера погиб, об этом стало известно совсем недавно…"),
                new(1, "Какой ужас, что с ним произошло?"),
                new(0,
                    "Надо сказать, что его увечья говорят о насильственной смерти, мы подозреваем, что его мог кто-то убить."),
                new(1, "Но кто мог это сделать, я уверен в каждом человеке в этом храме."),
                new(0, "Скажи честно, сын мой, ты знаешь что-то об этом?"),
                new("Никак нет, Отец, я слышу эту новость впервые от тебя.", "Да, Отче, это я убил его!",
                    "Мне кажется, я мог видеть вчера что-то странное, но ничего конкретного",
                    "Я видел, как закатываются его глаза, а потом обмякает и все тело.",
                    "Лично я нет, но возможно кто-то из братьев мог что-то видеть.",
                    "Да, Отец, начало положено, скоро я доберусь и до остальных."),
                new(0, "Скажи мне как есть, что ты делал вчера?"),
                new("Ничего необычного, всего лишь ходил на молитвы, это могут подтвердить и другие.",
                    "Я искал и выжидал нужный момент, чтобы ударить.",
                    "Честно говоря, ко мне в голову закрадываются иногда мысли о всяких непотребствах, но не о каком убийстве речи и не идет.",
                    "Я представлял, как окроплю кровью своей жертвы весь храм!",
                    "Я помогал своим братьям с делами по храму.",
                    "После хладнокровного убийства я еще ходил и высматривал себе следующую жертву."),
                new(0, "Как думаешь это может повториться снова?"),
                new("Я очень надеюсь, что нет. Я сделаю все, что в моих силах дабы избежать этого.",
                    "О, конечно, повториться и очень скоро, оно кричит и требует крови.",
                    "Сложно сказать, но в любом случае нужно быть крайте осторожным, мы не знаем где прячется убийца.",
                    "Я думаю нам всем стоит быть осторожными, ведь зверь внутри не дремлет, я это чувствую. Кто знает, когда он покажет зубы в следующий раз.",
                    "Возможно это все же не убийство, а несчастный случай и нам не о чем беспокоиться.",
                    "Смерть преследует нас по пятам, одних она застегает в постели в преклонном возрасте, а к другим подкрадывается незаметно из-за угла с ножом в руках."),
                new(0, "Хорошо я понял, спасибо за честность! Ну будь здоров, мне нужно идти."),
                new(1, "И вы будьте, до встречи."),
            };
        }

        if (GameData.Day == 2)
        {
            list = new List<MyClass>
            {
                new(0, "Доброе утро, сын мой, хотя оно не такое уж и доброе."),
                new(1, "Здравствуй, Отец. Что же случилось?"),
                new(0, "Вчера было найдено второе тело одного из наших братьев."),
                new(1, "Какой ужас! Неужели где-то здесь бродит маньяк?"),
                new(0,
                    "Боюсь, что это действительно так, сын мой. Все указывает на то, что среди нас прячется хладнокровный убийца."),
                new(1, "И что же нам теперь делать, Отец?"),
                new(0,
                    "Мне не остается ничего, кроме как лично допросить всех и каждого в этом монастыре. Скажи мне, где ты был и что делал вчера днем?"),
                new("Собственно, как и вчера, я выполнял всякие поручения по храму, ничего сверхъестественного.",
                    "О, я испытывал максимальное наслаждение, в момент убиения моего товарища, честно говоря, он мне никогда не нравился.",
                    "Отец, вы же знаете мой распорядок дня, всегда одно и тоже, ничего нового.",
                    "Я изучал модели поведения моих братьев, искал в них уязвимости.",
                    "Честно говоря, мне вчера нездоровилось, я провалялся целый день в постели не в силах встать с нее.",
                    "Я сидел у себя в комнате, разрабатывая план действий, чтобы остаться незамеченным."),
                new(0,
                    "Я буду с тобой честен, я уже многих опросил и некоторые сказали, что ты странно себя вел вчера. Сторонился других, что-то бормотал себе под нос, кому-то даже нагрубил, как ты можешь это прокомментировать?"),
                new("В этом не было злого умысла, уверяю вас. Наверное, встал не стой ноги.",
                    "Конечно, а как иначе, как мне нормально общаться с такими жалкими созданиями как вы?",
                    "Каюсь, Отец. Не знаю, что на меня нашло. Нужно и перед остальными извиниться.",
                    "Мое повеление – это мое дело, извиняться и не подумаю.",
                    "Признаю было такое, всю ночь мне кошмары снились, вот причина моего поведения.",
                    "Прошлой ночью мне голос явился, поведал мне всю правду про нас всех, какие мы грязные ничтожные существа, но я разберусь с этой проблемой."),
                new(0,
                    "Знаешь, я чувствую, что ты переменился и что скрываешь от меня свои истинные чувства. Расскажи мне, что с тобой случилось? "),
                new(
                    "Меня наверное поразило какое-то неизвестное заболевание, Отче, но это никак не связано с убийствами.",
                    "Если бы ты знал, насколько я изменился, старик. Мне теперь никто не нужен!",
                    "Я тоже это ощущаю, мне тяжело на душе, но я все тот же простой монах, что и был всегда.",
                    "Да я чувствую невероятную силу, она растекается по моему телу, а самое главное, что ее даровали мне не вы. Теперь я знаю где истина!",
                    "Честно я тоже ощущаю в себе какие-то изменения, давайте я приду к вам на личную беседу позже и мы разберемся, что со мной происходит.",
                    "Да вы правы, я изменился, предлагаю уединиться и я детально продемонстрирую вам свои изменения."),
                new(0,
                    "Хорошо, я услышал тебя, сын мой, я зайду к тебе еще завтра, а пока будь здоров и береги себя."),
                new(1, "И вы, Отче, будьте осторожны, мне кажется это еще не конец."),
            };
        }

        if (GameData.Day == 3)
        {
            list = new List<MyClass>
            {
                new(0,
                    "Здравствуй, сын мой. Похоже наши опасения оказались не напрасными и с теории о убийце окончательно отпадают все сомнения. Новый день…"),
                new(1, "Новое убийство, можете не договаривать."),
                new(0,
                    "Да ты к сожалению прав, скажи как ты думаешь, это Боже наказывает нас за грехи наши или у этого есть более приземленное объяснение?"),
                new(1,
                    "Сказать сложно, все мы не безгрешны, это очевидно, нам только и остается служить, замаливая свои проступки. Но это, здесь скорее замешаны злые силы, чем наш Бог."),
                new(0,
                    "Вот и мне так кажется, а еще мне кажется твое поведение довольно странным, все вокруг либо боятся лишний раз из комнаты выйти, либо старательно замаливают свои грехи. А тебя как будто и не волнует вся сложившаяся ситуация, довольно спокойно реагируешь на новости, которые я тебе сообщаю, не боишься ходить один по монастырю. Может ты что-то знаешь и не говоришь мне?"),
                new(
                    "Да нет, что вы, конечно мне страшно, но жизнь же продолжается, зачем мне лишний раз трястись за себя, если судьбы не избежать.",
                    "Конечно я не боюсь, я точно знаю что мне ничего не будет, я на короткой ноге с причиной всех бедствий.",
                    "Ну мне пришлось пройти через многое в своей жизни еще до момента, когда я стал служителем Божьим, так что мне проще, чем остальным.",
                    "За последнее время я столкнулся с такими вещами, который я совершал сам и которые происходили вокруг, что меня уже ничего не испугает.",
                    "Хотите сказать, что вы подозреваете меня в чем-то, Отец, уверяю вас, я нивчем не виноват и никого не убивал!",
                    "Ваши подозрения могут завлечь на вас проблемы, которые вы не способны будете решить, я могу это гарантировать."),
                new(0,
                    "Сын мой, не нужно скрывать от меня что-либо или тем более стыдиться меня, я чувствую темноту в твоем сердце, расскажи мне об этом."),
                new(
                    "Боюсь, что в первые вы ошиблись, Отче, я все тот же, незаурядный монах, какая тьма, о чем вы?",
                    "Вы все еще на что-то способны и чуете подвох там, где остальные его не видят в упор, я удивлен.",
                    "Ну о чем вы говорите, разве в оплоте света и жизни может быть человек с оскверненной душой?",
                    "Честно я и сам удивляюсь, как в столь “светлом” месте может спокойно чувствовать себя сущность, вроде той, что сидит во мне.",
                    "Меня, наверное, поразило какое-то неизвестное заболевание, Отец. Но это не связано с убийствами, я клянусь.",
                    "Кто-то сверху, а скорее снизу, одарил меня большой силой и не стоит относиться к этому как, к чему-то ужасному, лично я в восторге от нее."),
                new(0,
                    "Мальчик мой, бесполезно бежать от твоего недуга, нужно решить эту проблему как можно скорее!"),
                new(
                    "Хорошо, Отец, вы правы, я буду молиться за себя в десять раз усерднее и надеюсь, что у меня получится излечиться.",
                    "На мой взгляд главный недуг – это вы, я попрошу сил и своего покровителя, дабы уничтожить вас.",
                    "Возможно, мне нужна помощь врача, отец. Мне все еще кажется, что в этом есть рациональное объяснение.",
                    "Здесь будут бессильны абсолютно все, вы уже ничего не поменяете, советую вам лучше преклоняться передо мной!",
                    "Я понимаю, прошу помогите мне, боюсь я не выстою один против сил зла.",
                    "Еще недавно я бы бежал к вам и просил о помощи, но сейчас я вижу, что даже вы не способны совладать со мной!"),
                new[]
                {
                    new MyClass(0,
                        "Хорошо, я зайду за тобой чуть позже и я обещаю, мы положим этому конец раз и навсегда."),
                    new MyClass(0,
                        "Как видится мне все карты вскрыты. Мне искренне жаль, мальчик мой, что тебя поразила это страшное проклятье. Боюсь мне ничего не остается кроме как провести сеанс экзорцизма над тобой, к сожалению, процент выживания в этом случае крайне маленький, но мы не может рисковать. Прости меня…")
                }[GetEnding()],
                new[]
                {
                    new MyClass(1, "Я буду смиренно ждать вас, Отец."),
                    new MyClass(1, "НЕТ, я так просто не дамся!")
                }[GetEnding()]
            };
        }
    }

    public int GetEnding()
    {
        if (GameData.Chances > 0) return 0;
        return 1;
    }
}