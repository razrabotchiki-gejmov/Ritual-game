using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public bool haveWeapon;
    public float invisibilityCooldown = 60f;
    public float timeToBecomeVisible;
    public bool isInvisible;
    // private Controls _input;

    void Start()
    {
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

        if (Input.GetKey(KeyCode.E))
        {
            if (NPC != null)
            {
                NPC.GetComponent<NPCState>().StartSpeak();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (NPC != null && haveWeapon && NPC.GetComponent<NPCState>().type == 0)
            {
                NPC.GetComponent<NPCState>().Die();
                BecomeVisible();
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (NPC != null && !NPC.GetComponent<NPCState>().isDead && NPC.GetComponent<NPCState>().type != 2)
            {
                NPC.GetComponent<NPCMovement>().FullStop();
                NPC.GetComponent<NPCMovement>().isMoveToPoint = true;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            BecomeInvisible();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (NPC != null && NPC.GetComponent<NPCState>().type <= 1)
            {
                NPC.GetComponent<NPCState>().Die();
                BecomeVisible();
            }
        }
        // if (GetComponentInChildren<WeaponSlotController>().GetComponentInChildren<Paint>())
        // {
        //     Debug.Log("Have paint");
        //     if (_input.Interact.PaintClothes.WasPerformedThisFrame())
        //     {
        //         Debug.Log("Try to paint");
        //         if (NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().isInBlood != true)
        //         {
        //             Debug.Log("Painted");
        //             NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().isInBlood = true;
        //             NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().GetComponent<SpriteRenderer>().color = Color.cyan;
        //             GetComponentInChildren<WeaponSlotController>().RemoveWeapon(GetComponentInChildren<WeaponSlotController>().GetComponentInChildren<Paint>().gameObject);
        //         }
        //     }
        // }
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
}