using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    public Interaction interaction;
    public List<Collider2D> NPCs;
    public List<Collider2D> foods;
    public List<Collider2D> doors;
    public List<Collider2D> items;
    public List<Collider2D> clothes;
    public PlayerSpeak playerSpeak;

    void Start()
    {
        NPCs = new List<Collider2D>();
        foods = new List<Collider2D>();
        doors = new List<Collider2D>();
        items = new List<Collider2D>();
        interaction = GetComponentInParent<Interaction>();
        playerSpeak = GetComponentInParent<PlayerSpeak>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCs.Add(other);
            interaction.NPC = other.gameObject;
        }

        if (other.CompareTag("Food"))
        {
            foods.Add(other);
            interaction.food = other.GetComponent<Food>();
            playerSpeak.StartSpeak("Если мне удастся залить туда яд…");
        }

        if (other.CompareTag("Door"))
        {
            doors.Add(other);
            interaction.door = other.GetComponentInParent<DoorNew>();
        }

        if (other.CompareTag("Clothes"))
        {
            clothes.Add(other);
            interaction.clothes = other.GetComponentInParent<Clothes>();
        }

        if (other.CompareTag("Item"))
        {
            items.Add(other);
            if (interaction.item != null)
            {
                interaction.item.RemoveBacklight();
            }

            interaction.item = other.GetComponent<Item>();
            interaction.item.AddBacklight();
            if (interaction.item.type == 6)
            {
                playerSpeak.StartSpeak("Ключи, это будет полезно");
            }

            if (interaction.item.type == 5)
            {
                playerSpeak.StartSpeak("Хмм, она явно сможет привлечь чье-нибудь внимание");
            }

            if (interaction.item.type == 4)
            {
                playerSpeak.StartSpeak(
                    "Красная краска напоминает кровь, что если я смогу устроить театральное представление");
            }
        }

        if (other.CompareTag("LusterTrigger"))
        {
            interaction.lusterTrigger = other.GetComponent<LusterTrigger>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            NPCs.Remove(other);
            if (NPCs.Count == 0)
            {
                interaction.NPC = null;
            }
            else
            {
                interaction.NPC = NPCs[^1].gameObject;
            }
        }

        if (other.CompareTag("Food"))
        {
            foods.Remove(other);
            if (foods.Count == 0)
            {
                interaction.food = null;
            }
            else
            {
                interaction.food = foods[^1].GetComponent<Food>();
            }
        }

        if (other.CompareTag("Door"))
        {
            doors.Remove(other);
            if (doors.Count == 0)
            {
                interaction.door = null;
            }
            else
            {
                interaction.door = doors[^1].GetComponent<DoorNew>();
            }
        }

        if (other.CompareTag("Clothes"))
        {
            clothes.Remove(other);
            if (clothes.Count == 0)
            {
                interaction.clothes = null;
            }
            else
            {
                interaction.clothes = clothes[^1].GetComponent<Clothes>();
            }
        }

        if (other.CompareTag("Item"))
        {
            interaction.item.RemoveBacklight();
            items.Remove(other);
            if (items.Count == 0)
            {
                interaction.item = null;
            }
            else
            {
                interaction.item = items[^1].GetComponent<Item>();
                interaction.item.AddBacklight();
            }
        }

        if (other.CompareTag("LusterTrigger"))
        {
            interaction.lusterTrigger = null;
        }
    }
}