using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SensitivityAreaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivityCooldown = 2f;
    public float timeToSense;
    public bool isPlayerNear;
    public GameObject detectionRatingScale;
    public GameObject detectionRatingText;
    public GameObject NPC;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && !NPC.GetComponent<NPCState>().isDead)
        {
            if (timeToSense <= 0)
            {
                IncreaseDetectionRating(5);
                timeToSense = sensitivityCooldown;
            }
            else
            {
                timeToSense -= Time.deltaTime;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    public void IncreaseDetectionRating(int value)
    {
        var currentValue = int.Parse(detectionRatingText.GetComponent<TextMeshProUGUI>().text);
        var newValue = currentValue + value;
        if (newValue > 100)
        {
            newValue = 100;
        }

        detectionRatingText.GetComponent<TextMeshProUGUI>().text = (newValue).ToString();
        detectionRatingScale.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue * 4, 20);
    }
}