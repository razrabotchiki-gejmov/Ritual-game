using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            player.GetComponent<InteractWithNPC>().NPC = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            player.GetComponent<InteractWithNPC>().NPC = null;
        }
    }
}