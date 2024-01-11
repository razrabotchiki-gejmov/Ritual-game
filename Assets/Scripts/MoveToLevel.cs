using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLevel : MonoBehaviour
{
    [SerializeField] private Transform nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            collision.transform.position = nextLevel.position ;
            if (name == "Teleport 2-3")
            {
                collision.GetComponent<PlayerSpeak>()
                    .StartSpeak("Комната смотрителя недалеко, уверен там я могу найти много интересного.", true);
            }
        }
    }
}