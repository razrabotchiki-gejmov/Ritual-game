using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public Food food;
    public DoorNew door;
    public Item item;
    public bool havePoison;
    public bool haveKey;
    public bool haveWeapon;
    public float invisibilityCooldown = 60f;
    public float timeToBecomeVisible;
    public bool isInvisible;

    public PlayerSpeak playerSpeak;
    // private Controls _input;

    void Start()
    {
        playerSpeak = GetComponent<PlayerSpeak>();
        // _input = new Controls();
        // _input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToBecomeVisible <= 0)
        {
            BecomeVisible();
        }

        if (isInvisible)
        {
            timeToBecomeVisible -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NPC != null)
            {
                NPC.GetComponent<NPCState>().StartSpeak(0);
            }

            if (door != null)
            {
                if (door.isLocked)
                {
                    if (haveKey)
                    {
                        door.Unlock();
                        haveKey = false;
                        Destroy(GetComponentInChildren<Item>().gameObject);
                    }
                    else
                    {
                        playerSpeak.StartSpeak("Похоже она заперта");
                    }
                }

                else if (!door.isOpened)
                    door.Open();
                else
                    door.Close();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item != null)
            {
                item.PickUpWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (NPC != null && haveWeapon && NPC.GetComponent<NPCState>().type <= 1)
            {
                NPC.GetComponent<NPCState>().Die();
                BecomeVisible();
            }

            if (food != null && havePoison)
            {
                PoisonFood();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (GetComponentInChildren<Item>())
            {
                DropWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (NPC != null && !NPC.GetComponent<NPCState>().isDead && NPC.GetComponent<NPCState>().type != 3)
            {
                NPC.GetComponent<NPCMovement>().FullStop();
                NPC.GetComponent<NPCMovement>().isMoveToPoint = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BecomeInvisible();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (NPC != null && NPC.GetComponent<NPCState>().type <= 2)
            {
                NPC.GetComponent<NPCState>().Die();
                BecomeVisible();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EndDay();
        }
    }

    public void BecomeInvisible()
    {
        var newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 0.1f;
        GetComponent<SpriteRenderer>().color = newColor;
        isInvisible = true;
    }

    public void BecomeVisible()
    {
        var newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 1f;
        GetComponent<SpriteRenderer>().color = newColor;
        timeToBecomeVisible = invisibilityCooldown;
        isInvisible = false;
    }

    public void PoisonFood()
    {
        food.GetComponent<Food>().BecomePoisoned();
        Destroy(GetComponentInChildren<Item>().gameObject);
        havePoison = false;
    }

    public void DropWeapon()
    {
        var equippedItem = GetComponentInChildren<Item>().gameObject;
        var droppedWeapon = Instantiate(equippedItem,
            GetComponent<Transform>().position, GetComponent<Transform>().rotation);
        droppedWeapon.GetComponent<Collider2D>().enabled = true;
        haveWeapon = false;
        havePoison = false;
        haveKey = false;
        Destroy(equippedItem);
    }
}