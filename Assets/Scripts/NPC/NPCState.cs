using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCState : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDead;
    public int type;
    public GameObject dialogWindow;
    public TextMeshProUGUI dialogMessage;
    public GameManager gameManager;
    //0 -монахи, 1 - повара, 2 - стражники, 3 - паладины, 4- настоятель

    void Start()
    {
        if (GameData.Names.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }

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
        if (isDead)
        {
            return;
        }

        dialogMessage.text = UsePhrase(cause);
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
        isDead = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        GetComponent<NPCMovement>().FullStop();
        gameManager.SomeoneDied();
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
    public string UsePhrase(int cause)
    {
        if (cause == 0)
        {
            if (type == 0)
            {
                if (GameData.Day == 1)
                {
                    return new[]
                    {
                        "Приветствую, брат", "Как настроение?", "Да будет вера твоя крепка",
                        "Не сомневайся в Боге нашем", "На этой земле нет места для темных сил"
                    }[
                        Random.Range(0, 5)];
                }
                else if (GameData.Day == 2)
                {
                    return new[]
                    {
                        "Как такое могло произойти?", "Мира тебе", "Славо Богу ты в полном здравии",
                        "Будь осторожен!", "Мы должны с этим разобраться!"
                    }[
                        Random.Range(0, 5)];
                }

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
            return new[] { "Иди своей дорогой!", "Не стой столбом", "Проходи, не задерживайс" }[
                Random.Range(0, 3)];
        }
        else if (cause == 8)
        {
            return new[] { "Что-то здесь не так", "Я чувствую что-то", "Этот парень какой-то не такой" }[
                Random.Range(0, 3)];
        }
        else if (cause == 9)
        {
            return "Спасибо что рассказ, я обязательно займусь этим";
        }

        return "...";
    }
}