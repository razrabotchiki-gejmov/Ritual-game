using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
    // Start is called before the first frame update
    public int rays = 11;
    public int distance = 10;
    public float angle = 50;
    public Vector2 offset;
    public bool isPlayerSpotted;
    public bool isPlayerHaveWeapon;
    public bool isPlayerHaveCoinOrKey;
    public bool isSomeoneKilledDirectly;
    public GameManager gameManager;
    public GameObject player;
    public Interaction interaction;
    public GameObject NPC;
    public int lastDetectionRating;
    public NPCState npcState;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        interaction = player.GetComponent<Interaction>();
        npcState = GetComponentInParent<NPCState>();
        NPC = npcState.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var newIsPlayerSpotted = false;
        var newIsPlayerHaveWeapon = interaction.haveWeapon;
        var newIsPlayerHaveCoinOrKey = (interaction.haveCoin || interaction.haveKey);
        var newIsPlayerSmeared = interaction.isClothesBlooded;
        for (int i = -rays / 2; i < rays / 2 + 1; i++)
        {
            var y = Mathf.Sin(i * angle / (rays - 1) * Mathf.Deg2Rad);
            var x = Mathf.Cos(i * angle / (rays - 1) * Mathf.Deg2Rad);
            Vector2 dir = transform.TransformDirection(new Vector2(x, y));
            var hits = Physics2D.RaycastAll((Vector2)transform.position + offset,
                dir, distance);
            var isSomethingFounded = false;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Player") && !NPC.GetComponent<NPCState>().isDead &&
                    !player.GetComponent<Interaction>().isInvisible)
                {
                    newIsPlayerSpotted = true;
                    Debug.DrawLine((Vector2)transform.position + offset, hit.point, Color.green);
                    isSomethingFounded = true;
                    break;
                }

                if (hit.collider.gameObject.GetComponent<Obstacle>())
                {
                    Debug.DrawLine((Vector2)transform.position + offset, hit.point, Color.blue);
                    isSomethingFounded = true;
                    break;
                }

                if (hit.collider.gameObject.CompareTag("NPC") && hit.collider.GetComponent<NPCState>().isDead)
                {
                    if (npcState.seenCorpse) continue;
                    npcState.StartSpeak(11);
                }
                // if (hit.collider.gameObject.GetComponent<Item>())
                // {
                //     if (hit.collider.gameObject.GetComponent<Item>().type == 5 &&
                //         gameObject.GetComponentInParent<NPCState>().type == 0)
                //     {
                //         Debug.DrawLine((Vector2)transform.position + offset, hit.point, Color.blue);
                //         // gameObject.GetComponentInParent<NPCMovementOld>().MoveToPoint(hit.point);
                //         // gameObject.GetComponentInParent<NPCMovementOld>().isMovingToCoin = true;
                //         break;
                //     }
                // }
            }

            if (!isSomethingFounded)
            {
                Debug.DrawRay((Vector2)transform.position + offset, dir * distance, Color.red);
            }
        }

        if (isPlayerSpotted && !isSomeoneKilledDirectly && gameManager.isSomeoneKilledDirectly)
        {
            gameManager.IncreaseDetectionRating(100);
            lastDetectionRating = 100;
            NPC.GetComponent<NPCState>().StartSpeak(3);
        }
        else if (isPlayerSpotted && !isPlayerHaveWeapon && newIsPlayerHaveWeapon)
        {
            gameManager.IncreaseDetectionRating(20);
            lastDetectionRating = 20;
            NPC.GetComponent<NPCState>().StartSpeak(1);
        }
        else if (npcState.type == 4 && isPlayerSpotted && !isPlayerHaveCoinOrKey && newIsPlayerHaveCoinOrKey)
        {
            gameManager.IncreaseDetectionRating(10);
            lastDetectionRating = 10;
            NPC.GetComponent<NPCState>().StartSpeak(12);
            player.GetComponent<Interaction>().DropWeapon();
        }

        else if (!isPlayerSpotted && newIsPlayerSpotted)
        {
            if (interaction.isClothesBlooded)
            {
                gameManager.IncreaseDetectionRating(40);
                lastDetectionRating = 40;
                NPC.GetComponent<NPCState>().StartSpeak(2);
            }
            else if (interaction.haveWeapon)
            {
                gameManager.IncreaseDetectionRating(20);
                lastDetectionRating = 20;
                NPC.GetComponent<NPCState>().StartSpeak(1);
            }
            else if (interaction.havePoison)
            {
                gameManager.IncreaseDetectionRating(20);
                lastDetectionRating = 20;
            }
        }

        isSomeoneKilledDirectly = gameManager.isSomeoneKilledDirectly;
        isPlayerSpotted = newIsPlayerSpotted;
        isPlayerHaveWeapon = newIsPlayerHaveWeapon;
        isPlayerHaveCoinOrKey = newIsPlayerHaveWeapon;
    }
}