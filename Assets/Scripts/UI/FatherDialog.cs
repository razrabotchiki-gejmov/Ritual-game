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

    void Start()
    {
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

        if (myClass.Type == 1)
        {
            superiorName.SetActive(false);
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
            answer1.SetActive(true);
            if (!TalkWithFather)
                Destroy(GameData.SmearedNPC.gameObject);
        }

        index++;
    }

    public void EndDialog()
    {
        GameObject.Find("Player 1").GetComponent<MovementController>().enabled = true;
        gameObject.SetActive(false);
        index = 0;
    }

    public void GetPhrasesList()
    {
        if (TalkWithFather)
        {
            list = new List<MyClass>
                {
                    new(1, "����, ��� ������� � ����� ��� ���� �� ����� ������� ������ ���-�� ��������. � ����� ����� ���������.", "", ""),
                    new(0, "������� ��� ���������, � ����������� ������� ����.", "", ""),
                    new(2, "", "<����>", ""),
                };
            playerName.GetComponent<TextMeshProUGUI>().text = "��";
            superiorName.GetComponent<TextMeshProUGUI>().text = "����������";
        }
        else
        {
            list = new List<MyClass>
                {
                    /* 0 - �������� */ 
                new(0, "������, ����� ���������� �� ���, ��������� ���� � ����� ������� ������. ����!!!", "", ""),
                    /* 1 - ����� */ 
                new(1, "���, ��� ��, ��� �����-�� ������, � �� � ��� �� ����", "", ""),
                new(2, "", "<����>", ""),
                };
            playerName.GetComponent<TextMeshProUGUI>().text = "�����";
            superiorName.GetComponent<TextMeshProUGUI>().text = "��������";
        }
    }
}
