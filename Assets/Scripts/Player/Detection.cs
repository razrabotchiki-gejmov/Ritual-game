using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    public Interaction interaction;

    void Start()
    {
        interaction = GetComponentInParent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            interaction.NPC = other.gameObject;
        }

        if (other.CompareTag("Food"))
        {
            interaction.food = other.GetComponent<Food>();
        }

        if (other.CompareTag("Door"))
        {
            interaction.door = other.GetComponentInParent<DoorNew>();
        }

        if (other.CompareTag("Item"))
        {
            if (interaction.item != null)
            {
                interaction.item.RemoveBacklight();
            }
            interaction.item = other.GetComponent<Item>();
            interaction.item.AddBacklight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && other.gameObject == interaction.NPC)
        {
            interaction.NPC = null;
        }

        if (other.CompareTag("Food") && other.GetComponent<Food>() == interaction.food)
        {
            interaction.food = null;
        }

        if (other.CompareTag("Door") && other.GetComponentInParent<DoorNew>() == interaction.door)
        {
            interaction.door = null;
        }

        if (other.CompareTag("Item") && other.GetComponent<Item>() == interaction.item)
        {
            interaction.item.RemoveBacklight();
            interaction.item = null;
        }
    }
}