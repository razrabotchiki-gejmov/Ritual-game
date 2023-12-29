using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public Food food;
    public DoorNew door;
    public Item item;
    public Clothes clothes;
    public LusterTrigger lusterTrigger;
    public GameObject coinPoint;
    public bool isCoinPointNear;
    public bool havePoison;
    public bool haveKey;
    public bool haveWeapon;
    public bool havePaint;
    public bool haveCoin;
    public float invisibilityCooldown = 60f;
    public float timeToBecomeVisible;
    public bool isInvisible;
    public bool isClothesBlooded;
    public SpriteRenderer clothesSprite;
    public PlayerSpeak playerSpeak;
    public GameManager gameManager;
    // private Controls _input;

    void Start()
    {
        playerSpeak = GetComponent<PlayerSpeak>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinPoint = GameObject.FindWithTag("CoinPoint");
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
            gameManager.DecreaseInvisibilityTimeScale(timeToBecomeVisible, invisibilityCooldown);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NPC != null)
            {
                if (isInvisible)
                {
                    NPC.GetComponent<NPCState>().StartSpeak(6);
                }
                else
                {
                    NPC.GetComponent<NPCState>().StartSpeak(0);
                }
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (lusterTrigger != null && coinPoint.transform.childCount > 0)
            {
                lusterTrigger.lusterConstruction.DropLuster();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (NPC != null && NPC.GetComponent<NPCState>().type <= 1 && !NPC.GetComponent<NPCState>().isDead)
            {
                if (haveWeapon)
                {
                    GameData.Names.Add(NPC.name);
                    var currentItem = GetComponentInChildren<Item>();
                    if (currentItem.type is 1 or 2)
                    {
                        GameData.Items.Add(currentItem.gameObject.name);
                        Destroy(currentItem.gameObject);
                    }

                    NPC.GetComponent<NPCState>().Die();
                    haveWeapon = false;
                    BecomeVisible();
                    BloodyClothes();
                }

                if (havePaint)
                {
                    NPC.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
                    Destroy(GetComponentInChildren<Item>().gameObject);
                    havePaint = false;
                }
            }

            if (food != null && havePoison)
            {
                PoisonFood();
            }

            if (haveCoin && isCoinPointNear)
            {
                PlaceCoin();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (GetComponentInChildren<Item>())
            {
                DropWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            clothes.PickUpClothes();
            CleanClothes();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (NPC != null && !NPC.GetComponent<NPCState>().isDead && NPC.GetComponent<NPCState>().type != 3 &&
                gameManager.canUseConviction)
            {
                gameManager.BecomeOutOfUse(1);
                NPC.GetComponent<NPCMovement>().FullStop();
                NPC.GetComponent<NPCMovement>().isMoveToPoint = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (gameManager.canUseInvisibility)
            {
                BecomeInvisible();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (NPC != null && NPC.GetComponent<NPCState>().type <= 2 && gameManager.canUseSuperpower)
            {
                gameManager.BecomeOutOfUse(3);
                NPC.GetComponent<NPCState>().Die();
                BecomeVisible();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            gameManager.EndDay();
        }
    }

    public void BecomeInvisible()
    {
        gameManager.BecomeOutOfUse(2);
        var newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 0.1f;
        GetComponent<SpriteRenderer>().color = newColor;
        isInvisible = true;
        gameManager.ShowInvisibilityTimeScale();
    }

    public void BecomeVisible()
    {
        var newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 1f;
        GetComponent<SpriteRenderer>().color = newColor;
        timeToBecomeVisible = invisibilityCooldown;
        isInvisible = false;
        gameManager.HideInvisibilityTimeScale();
    }

    public void PoisonFood()
    {
        food.GetComponent<Food>().BecomePoisoned();
        Destroy(GetComponentInChildren<Item>().gameObject);
        gameManager.SomeoneDied();
        havePoison = false;
    }

    public void DropWeapon()
    {
        var equippedItem = GetComponentInChildren<Item>().gameObject;
        var droppedWeapon = Instantiate(equippedItem,
            GetComponent<Transform>().position, GetComponent<Transform>().rotation);
        droppedWeapon.GetComponent<Collider2D>().enabled = true;
        droppedWeapon.name = equippedItem.name;
        haveWeapon = false;
        havePoison = false;
        haveKey = false;
        havePaint = false;
        haveCoin = false;
        coinPoint.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(equippedItem);
    }

    public void BloodyClothes()
    {
        clothesSprite.color = Color.red;
        isClothesBlooded = true;
        playerSpeak.StartSpeak("В таком виде мне лучше не попадаться на глаза");
    }

    public void CleanClothes()
    {
        clothesSprite.color = Color.white;
        isClothesBlooded = false;
    }

    public void PlaceCoin()
    {
        var coin = GetComponentInChildren<ItemSlotController>().GetComponentInChildren<SpriteRenderer>().gameObject;
        var droppedCoin = Instantiate(coin,
            coinPoint.transform.position, coinPoint.transform.rotation);
        // droppedCoin.transform.parent = coinPoint.transform;
        droppedCoin.transform.SetParent(coinPoint.transform);
        // droppedCoin.transform.localScale = coin.transform.lossyScale;
        droppedCoin.GetComponent<Collider2D>().enabled = true;
        coinPoint.GetComponent<SpriteRenderer>().enabled = false;
        droppedCoin.name = coin.name;
        haveCoin = false;
        Destroy(coin);
    }
}