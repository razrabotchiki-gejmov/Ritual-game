using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
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
    public MovementController playerMovement;
    public GameManager gameManager;
    // private Controls _input;

    void Start()
    {
        playerMovement = GetComponent<MovementController>();
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
                var npcState = NPC.GetComponent<NPCState>();
                if (isInvisible)
                {
                    npcState.StartSpeak(6);
                }

                else if (npcState.type == 2 && GameData.SmearedNPC != null)
                {
                    playerSpeak.StartSpeak("Я видел человека запачканного кровью");
                    npcState.StartSpeak(10);
                    GameData.SmearedNPC.Die();
                    GameData.SmearedNPC = null;
                }
                else
                {
                    if (NPC.GetComponent<NPCPotentialKiller>())
                        GameData.SpokeWithPotentialKiller1[GameData.Day - 1] = 1;
                    npcState.StartSpeak(0);
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
                var npcState = NPC.GetComponent<NPCState>();
                if (haveWeapon)
                {
                    var currentItem = GetComponentInChildren<Item>();
                    var npcMovement = NPC.GetComponent<NPCMovementOld>();
                    // if (npcMovement) npcMovement.FullStop();
                    playerMovement.enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    if (currentItem.type == 0)
                    {
                        Invoke(nameof(KillNpcKnife), 0);
                    }

                    if (currentItem.type == 1)
                    {
                        Invoke(nameof(KillNpcRock), 2);
                    }

                    if (currentItem.type == 2)
                    {
                        Invoke(nameof(KillNpcVein), 3);
                    }
                }

                if (havePaint)
                {
                    npcState.GetSmeared();
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
            if (NPC != null && !NPC.GetComponent<NPCState>().isDead &&
                gameManager.canUseConviction)
            {
                var potentialKiller = NPC.GetComponent<NPCPotentialKiller>();
                var npcVision = NPC.GetComponentInChildren<NPCVision>();
                if (npcVision.lastDetectionRating > 0)
                {
                    playerSpeak.StartSpeak("Ты ничего здесь не видел");
                    NPC.GetComponent<NPCState>().StartSpeak(4);
                    gameManager.IncreaseDetectionRating(-npcVision.lastDetectionRating);
                    gameManager.BecomeOutOfUse(1);
                }
                else if (GameData.SpokeWithPotentialKiller1[0] + GameData.SpokeWithPotentialKiller1[1] == 2 &&
                         potentialKiller)
                {
                    potentialKiller.KillTargetNPC();
                    gameManager.BecomeOutOfUse(1);
                }
                else if (NPC.GetComponent<NPCState>().type <= 2)
                {
                    playerSpeak.StartSpeak(" Иди найди тихое место и жди меня там");
                    NPC.GetComponent<NPCMovementOld>().FullStop();
                    NPC.GetComponent<NPCMovementOld>().isMoveToPoint = true;
                    NPC.GetComponent<NPCState>().StartSpeak(5);
                    gameManager.BecomeOutOfUse(1);
                }
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
                NPC.GetComponent<NPCState>().Die();
                gameManager.isSomeoneKilledDirectly = true;
                BecomeVisible();
                BloodyClothes();
                gameManager.BecomeOutOfUse(3);
            }
            else if (door != null && door.isLocked)
            {
                door.Unlock();
                gameManager.BecomeOutOfUse(3);
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
        playerSpeak.StartSpeak("В таком виде мне лучше не попадаться на глаза", false);
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

    public void KillNpcKnife()
    {
        BloodyClothes();
        gameManager.isSomeoneKilledDirectly = true;
        NPC.GetComponent<NPCState>().Die();
        BecomeVisible();
        playerMovement.enabled = true;
    }

    public void KillNpcRock()
    {
        var currentItem = GetComponentInChildren<Item>();
        BloodyClothes();
        GameData.Items.Add(currentItem.gameObject.name);
        Destroy(currentItem.gameObject);
        haveWeapon = false;
        gameManager.isSomeoneKilledDirectly = true;
        NPC.GetComponent<NPCState>().Die();
        BecomeVisible();
        playerMovement.enabled = true;
    }

    public void KillNpcVein()
    {
        var currentItem = GetComponentInChildren<Item>();
        GameData.Items.Add(currentItem.gameObject.name);
        Destroy(currentItem.gameObject);
        haveWeapon = false;
        gameManager.isSomeoneKilledDirectly = true;
        NPC.GetComponent<NPCState>().Die();
        BecomeVisible();
        playerMovement.enabled = true;
    }
}