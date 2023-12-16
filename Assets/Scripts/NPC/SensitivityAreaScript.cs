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
    public GameManager gameManager;
    public GameObject NPC;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && !NPC.GetComponent<NPCState>().isDead)
        {
            if (timeToSense <= 0)
            {
                gameManager.IncreaseDetectionRating(5);
                NPC.GetComponent<NPCState>().StartSpeak(8);
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
}