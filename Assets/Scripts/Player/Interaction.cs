using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    public NPCState NPC;
    public Food food;
    public DoorNew door;
    public Item item;
    public Clothes clothes;
    public LusterTrigger lusterTrigger;
    public CoinPoint coinPoint;
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

    void Start()
    {
        playerMovement = GetComponent<MovementController>();
        playerSpeak = GetComponent<PlayerSpeak>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinPoint = GameObject.FindWithTag("CoinPoint").GetComponent<CoinPoint>();
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
                    NPC.StartSpeak(6);
                }

                else if (NPC.type == 2 && gameManager.smearedNPC != null)
                {
                    playerSpeak.StartSpeak("Я видел человека запачканного кровью", false, true);
                    NPC.StartSpeak(10);
                    gameManager.smearedNPC.Die();
                    gameManager.smearedNPC = null;
                }
                else if (NPC.type == 4)
                {
                    if (!GameData.TalkedToFather)
                    {
                        NPC.StartSpeak(9);
                    }
                }
                else
                {
                    if (NPC.GetComponent<NPCPotentialKiller>())
                        GameData.SpokeWithPotentialKiller[GameData.Day - 1] = 1;
                    NPC.StartSpeak(0);
                }
            }

            if (door != null)
            {
                if (door.isLocked)
                {
                    playerSpeak.StartSpeak("Похоже она заперта");
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
            if (lusterTrigger != null && coinPoint.isCoinHere)
            {
                lusterTrigger.lusterConstruction.DropLuster();
                gameManager.ShowLusterInfo(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (NPC != null && NPC.type <= 1 && !NPC.isDead)
            {
                if (haveWeapon)
                {
                    var currentItem = GetComponentInChildren<Item>();
                    var npcMovement = NPC.GetComponent<NPCMovement>();
                    if (npcMovement) npcMovement.cannotMove = true;
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
                    NPC.GetSmeared();
                    Destroy(GetComponentInChildren<Item>().gameObject);
                    havePaint = false;
                    gameManager.HideItem();
                }
            }

            if (food != null && havePoison)
            {
                PoisonFood();
            }

            if (haveCoin && isCoinPointNear)
            {
                PlaceCoin();
                coinPoint.HideHint();
                if (lusterTrigger != null) lusterTrigger.ShowHint();
                gameManager.ShowLusterInfo();
            }

            if (haveKey && door != null && door.isLocked)
            {
                door.Unlock();
                haveKey = false;
                Destroy(GetComponentInChildren<Item>().gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (GetComponentInChildren<Item>())
            {
                if (havePoison && food != null) food.CannotBePoisoned();
                if (haveKey && door != null) door.GetComponentInChildren<DoorHint>().DoorOpenCloseHint();
                if (haveCoin && isCoinPointNear) coinPoint.HideHint();
                GetComponentInChildren<Item>().DropWeapon(GetComponent<Transform>());
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            clothes.PickUpClothes();
            CleanClothes();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (NPC != null && !NPC.isDead &&
                gameManager.canUseConviction)
            {
                var potentialKiller = NPC.GetComponent<NPCPotentialKiller>();
                var npcVision = NPC.GetComponentInChildren<NPCVision>();
                if (npcVision.lastDetectionRating > 0)
                {
                    playerSpeak.StartSpeak("Ты ничего здесь не видел", false, true);
                    NPC.StartSpeak(4);
                    gameManager.IncreaseDetectionRating(-npcVision.lastDetectionRating);
                    gameManager.BecomeOutOfUse(1);
                }
                else if (GameData.SpokeWithPotentialKiller[0] + GameData.SpokeWithPotentialKiller[1] == 2 &&
                         potentialKiller)
                {
                    playerSpeak.StartSpeak(
                        "Я знаю, как ты ненавидишь своего “друга”, и внутри каждый из нас понимает, что это он во всем виноват. Останови же его, спаси невинные души.",
                        false, true);
                    NPC.StartSpeak("Ты абсолютно прав. Ооо, как же долго я ждал этого момента");
                    potentialKiller.KillTargetNPC();
                    gameManager.BecomeOutOfUse(1);
                }
                else if (NPC.type <= 2)
                {
                    playerSpeak.StartSpeak(" Иди найди тихое место и жди меня там", false, true);
                    // NPC.GetComponent<NPCMovementOld>().FullStop();
                    // NPC.GetComponent<NPCMovementOld>().isMoveToPoint = true;
                    NPC.StartSpeak(5);
                    gameManager.BecomeOutOfUse(1);
                }
                else if (NPC.type == 4)
                {
                    NPC.StartSpeak(9);
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
            if (NPC != null && NPC.type <= 2 && gameManager.canUseSuperpower)
            {
                NPC.Die();
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
        gameManager.HideItem();
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
        coinPoint.isCoinHere = true;
    }

    public void KillNpcKnife()
    {
        NPC.Die();
        gameManager.isSomeoneKilledDirectly = true;
        BloodyClothes();
        BecomeVisible();
        playerMovement.enabled = true;
    }

    public void KillNpcRock()
    {
        var currentItem = GetComponentInChildren<Item>();
        NPC.Die();
        gameManager.isSomeoneKilledDirectly = true;
        BloodyClothes();
        GameData.Items.Add(currentItem.gameObject.name);
        Destroy(currentItem.gameObject);
        haveWeapon = false;
        BecomeVisible();
        playerMovement.enabled = true;
        gameManager.HideItem();
    }

    public void KillNpcVein()
    {
        NPC.Die();
        gameManager.isSomeoneKilledDirectly = true;
        var currentItem = GetComponentInChildren<Item>();
        GameData.Items.Add(currentItem.gameObject.name);
        Destroy(currentItem.gameObject);
        haveWeapon = false;
        BecomeVisible();
        playerMovement.enabled = true;
        gameManager.HideItem();
    }
}