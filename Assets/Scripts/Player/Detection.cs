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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            if (other.GetComponent<NPCState>().isDead) return;
            if (NPCs.Count > 0) interaction.NPC.hint.HideHint();
            NPCs.Add(other);
            interaction.NPC = other.GetComponent<NPCState>();
            if (interaction.havePaint || interaction.haveWeapon) interaction.NPC.hint.ShowsItemInteractionHint();
            else interaction.NPC.hint.ShowsSpeakHint();
        }

        if (other.CompareTag("Food"))
        {
            foods.Add(other);
            interaction.food = other.GetComponent<Food>();
            if (interaction.havePoison)
            {
                interaction.food.CanBePoisoned();
            }

            playerSpeak.StartSpeak("Если мне удастся залить туда яд…", true);
        }

        if (other.CompareTag("Door"))
        {
            if (doors.Count > 0) doors[^1].GetComponent<DoorHint>().CloseHint();
            doors.Add(other);
            ChooseDoor(other);
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
                interaction.item.HideHint();
            }

            interaction.item = other.GetComponent<Item>();
            interaction.item.ShowHint();
            if (interaction.item.type == 6)
            {
                playerSpeak.StartSpeak("Ключи, это будет полезно", true);
            }

            if (interaction.item.type == 5)
            {
                playerSpeak.StartSpeak("Хмм, она явно сможет привлечь чье-нибудь внимание", true);
            }

            if (interaction.item.type == 4)
            {
                playerSpeak.StartSpeak(
                    "Красная краска напоминает кровь, что если я смогу устроить театральное представление", true);
            }
        }

        if (other.CompareTag("LusterTrigger"))
        {
            playerSpeak.StartSpeak("На вид тяжелая, а что если она упадет на кого-нибудь…", true);
            interaction.lusterTrigger = other.GetComponent<LusterTrigger>();
            if (interaction.coinPoint.isCoinHere) interaction.lusterTrigger.ShowHint();
        }

        if (other.CompareTag("CoinPoint"))
        {
            interaction.isCoinPointNear = true;
            if (interaction.haveCoin) interaction.coinPoint.ShowHint();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            other.GetComponent<NPCState>().hint.HideHint();
            NPCs.Remove(other);
            if (NPCs.Count == 0)
            {
                interaction.NPC = null;
            }
            else
            {
                interaction.NPC = NPCs[^1].GetComponent<NPCState>();
                if (interaction.havePaint || interaction.haveWeapon) interaction.NPC.hint.ShowsItemInteractionHint();
                else interaction.NPC.hint.ShowsSpeakHint();
            }
        }

        if (other.CompareTag("Food"))
        {
            foods.Remove(other);
            interaction.food.CannotBePoisoned();
            if (foods.Count == 0)
            {
                interaction.food = null;
            }
            else
            {
                interaction.food = foods[^1].GetComponent<Food>();
                if (interaction.havePoison)
                {
                    interaction.food.CanBePoisoned();
                }
            }
        }

        if (other.CompareTag("Door"))
        {
            doors.Remove(other);
            var doorHint = other.GetComponent<DoorHint>();
            doorHint.CloseHint();
            if (doors.Count == 0)
            {
                interaction.door = null;
            }
            else
            {
                ChooseDoor(doors[^1]);
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
            interaction.item.HideHint();
            items.Remove(other);
            if (items.Count == 0)
            {
                interaction.item = null;
            }
            else
            {
                interaction.item = items[^1].GetComponent<Item>();
                interaction.item.ShowHint();
            }
        }

        if (other.CompareTag("LusterTrigger"))
        {
            interaction.lusterTrigger.HideHint();
            interaction.lusterTrigger = null;
        }

        if (other.CompareTag("CoinPoint"))
        {
            interaction.isCoinPointNear = false;
            interaction.coinPoint.HideHint();
        }
    }

    public void ChooseDoor(Collider2D door)
    {
        interaction.door = door.GetComponentInParent<DoorNew>();
        var doorHint = door.GetComponent<DoorHint>();
        if (interaction.door.isLocked && interaction.haveKey)
        {
            doorHint.DoorUnlockHint();
        }
        else
        {
            doorHint.DoorOpenCloseHint();
        }
    }
}