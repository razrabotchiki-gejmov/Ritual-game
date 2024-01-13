using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLevel : MonoBehaviour
{
    [SerializeField] private Transform nextLevel;
    private float time = 0.4f;
    private float curTime = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = nextLevel.position;
            if (name == "Teleport 2-3")
            {
                collision.GetComponent<PlayerSpeak>()
                    .StartSpeak("Комната смотрителя недалеко, уверен там я могу найти много интересного.", true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            curTime += Time.deltaTime;
            if (curTime > time)
            {
                collision.transform.position = nextLevel.position;
                curTime = 0;
                time = 0.4f;
            }
        }
    }
}