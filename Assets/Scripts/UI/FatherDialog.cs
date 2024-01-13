using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Dialog;

public class FatherDialog : MonoBehaviour
{
    public GameObject playerName;
    public GameObject superiorName;
    public GameObject phrase;
    public GameObject answer1;
    private List<MyClass> list = new();
    private int index = 0;
    public bool TalkWithFather = false;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetPhrasesList();
        ChangeDialogState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (index < list.Count)
            {
                ChangeDialogState();
            }
            else
            {
                EndDialog();
            }
            if (list[index - 1].Type == 2)
            {
                return;
            }

        }
    }

    public void ChangeDialogState()
    {
        Debug.Log(index);
        var myClass = list[index];
        if (myClass.Type == 0)
        {
            playerName.SetActive(false);
            phrase.GetComponent<TextMeshProUGUI>().text = myClass.Phrase;
            superiorName.SetActive(true);
            phrase.SetActive(true);
        }

        else if (myClass.Type == 1)
        {
            superiorName.SetActive(false);
            playerName.SetActive(true);
            phrase.GetComponent<TextMeshProUGUI>().text = myClass.Phrase;
            phrase.SetActive(true);
        }
        else 
        { 
            superiorName.SetActive(false);
            phrase.SetActive(false);
            playerName.SetActive(true);
            answer1.GetComponent<TextMeshProUGUI>().text = "1) " + myClass.Answer1;
            answer1.SetActive(true);       
        }

        index++;
    }

    public void EndDialog()
    {
        GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = true;
        gameObject.SetActive(false);
        index = 0;
        if (!TalkWithFather)
            Destroy(gameManager.smearedNPC.gameObject);
    }

    public void GetPhrasesList()
    {
        if (TalkWithFather)
        {
            list = new List<MyClass>
            {
                new(1,
                    "Отец, мне кажется я видел как один из наших братьев делает что-то странное. Я думаю стоит проверить."),
                new(0, "Спасибо что рассказал, я обязательно займусь этим."),
                new("<Уйти>", "<Уйти>", "", "", "", ""),
            };
            playerName.GetComponent<TextMeshProUGUI>().text = "Вы";
            superiorName.GetComponent<TextMeshProUGUI>().text = "Настоятель";
        }
        else
        {
            list = new List<MyClass>
            {
                /* 0 - Стражник */
                new(0, "Убийца, думал спрячешься от нас, оставлять тебя в живых слишком опасно. Умри!!!"),
                /* 1 - Монах */
                new(1, "НЕТ, что вы, это какая-то ошибка, я не в чем не вино…"),
                new("<Уйти>", "<Уйти>", "", "", "", ""),
            };
            playerName.GetComponent<TextMeshProUGUI>().text = "Монах";
            superiorName.GetComponent<TextMeshProUGUI>().text = "Стражник";
        }
    }
}