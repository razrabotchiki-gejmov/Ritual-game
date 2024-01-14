using System.Collections;
using System.Collections.Generic;
using NPC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCState : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDead;
    //0 -монахи, 1 - повара, 2 - стражники, 3 - паладины, 4- настоятель

    public int type;
    public bool canSpeak = true;
    public GameObject dialogWindow;
    public TextMeshProUGUI dialogMessage;
    public GameManager gameManager;
    public NPCHint hint;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject blackScreen;
    public GameObject bloodyStain;

    void Start()
    {
        if (GameData.Names.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }
        blackScreen = GameObject.Find("BlackScreen");
        hint = GetComponent<NPCHint>();
        dialogWindow = transform.Find("DialogWindow").gameObject;
        dialogMessage = dialogWindow.transform.Find("Background").Find("Text").gameObject
            .GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // dialogMessage = transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSpeak(int cause)
    {
        if (!canSpeak) return;
        if (isDead)
        {
            return;
        }

        dialogMessage.text = UsePhrase(cause);
        dialogWindow.SetActive(true);
        Invoke(nameof(StopSpeak), 5f);
        hint.HideHint();
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

    public void Die()
    {
        // transform.GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Collider2D>().isTrigger = true;
        GetComponentInChildren<NPCVision>().enabled = false;
        isDead = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        // var npcMovement = GetComponent<NPCMovement>();
        // if (npcMovement.enabled) npcMovement.FullStop();
        var npcMovement = GetComponent<NPCMovement>();
        if (npcMovement) npcMovement.enabled = false;
        GetComponent<AudioSource>().Play();
        GameData.Names.Add(name);
        gameManager.SomeoneDied();
        hint.HideHint();
    }

    public void GetSmeared()
    {
        gameManager.smearedNPC = this;
        bloodyStain.SetActive(true);
    }


    // cause
    // 0 - заговорил игрок
    // 1 - увидел игрока с оружием
    // 2 - увидел игрока в крови
    // 3 - увидел игрока, совершающего убийство
    // 4 - забывает последнее действие игрока
    // 5 - идёт в укромное место
    // 6 - отвлекается на невидимого игрока
    // 7 - игрок долго стоит перед стражником
    // 8 - игрок подошёл близко к паладину 
    // 9 - настоятель отвлекается
    // 10 - игрок сообщаект стражнику о запятнаном NPC
    public string UsePhrase(int cause)
    {
        if (cause == 0)
        {
            if (type == 0)
            {
                if (GameData.Day == 1)
                {
                    if (GetComponent<NPCPotentialKiller>())
                        return
                            "Спасибо, что скрасил эту минуту свои присутствием, а то компания этого мне изрядно надоела.";
                    return new[]
                    {
                        "Приветствую, брат", "Как настроение?", "Да будет вера твоя крепка",
                        "Не сомневайся в Боге нашем", "На этой земле нет места для темных сил"
                    }[
                        Random.Range(0, 5)];
                }

                if (GameData.Day == 2)
                {
                    if (GetComponent<NPCPotentialKiller>())
                        return "Не верь ему на слово, я давно его знаю, у него за душой много грехов.";
                    return new[]
                    {
                        "Как такое могло произойти?", "Мира тебе", "Славо Богу ты в полном здравии",
                        "Будь осторожен!", "Мы должны с этим разобраться!"
                    }[
                        Random.Range(0, 5)];
                }

                if (GetComponent<NPCPotentialKiller>())
                    return " Он точно хитрит в чем-то, я уверен. Еще и на меня наговаривает, я этого так не оставлю.";

                return new[]
                {
                    "Мы все обречены!", "Боже, смилуйся над нами", "Здесь теперь так жутко",
                    "Что же нам делать", "Остерегайся, брат, убийца рядом!"
                }[
                    Random.Range(0, 5)];
            }
            else if (type == 1)
            {
                if (GameData.Day == 1)
                {
                    return new[]
                    {
                        "Ухх, как пахнет!", "Обед не скоро!", "Ох уж этот лук!"
                    }[
                        Random.Range(0, 3)];
                }

                return new[]
                {
                    "Ножи не трожь!", " К кастрюле не лезь!", "Что пришел?",
                }[
                    Random.Range(0, 3)];
            }
            else if (type == 2)
            {
                return new[]
                {
                    "Куда идешь?", " Сюда нельзя!", "Убийца не дремлет!",
                }[
                    Random.Range(0, 3)];
            }
        }
        else if (cause == 1)
        {
            return new[] { "Зачем ему это?", "Что он собрался делать?", "Главное чтобы никого не поранил" }[
                Random.Range(0, 3)];
        }
        else if (cause == 2)
        {
            return new[] { "Боже, это что кровь?!", "Надеюсь это кепчук", "Где он так измазался?" }[
                Random.Range(0, 3)];
        }
        else if (cause == 3)
        {
            return " АаАаА!!! Сюда, на помощь!";
        }
        else if (cause == 4)
        {
            return "Я ничего не видел...";
        }
        else if (cause == 5)
        {
            return "Будет сделано";
        }
        else if (cause == 6)
        {
            return "Что это было?!";
        }
        else if (cause == 7)
        {
            return new[] { "Иди своей дорогой!", "Не стой столбом", "Проходи, не задерживайся" }[
                Random.Range(0, 3)];
        }
        else if (cause == 8)
        {
            return new[] { "Что-то здесь не так", "Я чувствую что-то", "Этот парень какой-то не такой" }[
                Random.Range(0, 3)];
        }
        else if (cause == 9)
        {
            DialogWithFather();
            return "";
            // return "Спасибо что рассказ, я обязательно займусь этим";
        }
        else if (cause == 10)
        {
            TeleportToScene();
            return "Я покараю этого нечестивого";
        }

        return "...";
    }

    public void DialogWithFather()
    {
        GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = false;
        dialog.GetComponent<FatherDialog>().TalkWithFather = true;
        // dialog.GetComponent<Dialog>().enabled = false;
        dialog.SetActive(true);
        GameData.TalkedToFather = true;
        Invoke("FatherMove", 5f);
    }

    public void FatherMove()
    {
        var movement = GetComponent<NPCMovement>();
        movement.cooldowns = new List<float>
            { 0, 0.23f, 0.1f, 0.1f, 0.3f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.35f, 0.1f, 0.3f, 10000000f };
    }

    private Vector3 initPlace;

    public void TeleportToScene()
    {
        GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = false;
        //TODO: НЕ РАБОТАЕТ ЗАТЕМНЕНИЕ ЭКРАНА
        blackScreen.GetComponent<Image>().CrossFadeAlpha(1, 1, false);
        dialog.GetComponent<FatherDialog>().TalkWithFather = false;
        var guardPlace = GameObject.Find("GuardPlace");
        var monkPlace = GameObject.Find("MonkPlace");
        var playerPlace = GameObject.Find("PlayerPlace");
        initPlace = transform.position;
        transform.position = guardPlace.transform.position;
        gameManager.smearedNPC.transform.position = monkPlace.transform.position;
        gameManager.smearedNPC.GetComponent<NPCMovement>().enabled = false;
        GameObject.FindWithTag("Player").transform.position = playerPlace.transform.position;
        dialog.SetActive(true);

        Invoke("TeleportBack", 5f);
        blackScreen.GetComponent<Image>().CrossFadeAlpha(0, 0.1f, false);
    }

    public void TeleportBack()
    {
        transform.position = initPlace;
    }
}