using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public bool haveWeapon;
    private Controls _input;

    void Start()
    {
        _input = new Controls();
        _input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (NPC != null)
            {
                NPC.GetComponent<NPCState>().StartSpeak();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (NPC != null && haveWeapon)
            {
                NPC.GetComponent<NPCState>().Die();
            }
        }


        if (GetComponentInChildren<WeaponSlotController>().GetComponentInChildren<Paint>())
        {
            Debug.Log("Have paint");
            if (_input.Interact.PaintClothes.WasPerformedThisFrame())
            {
                Debug.Log("Try to paint");
                if (NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().isInBlood != true)
                {
                    Debug.Log("Painted");
                    NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().isInBlood = true;
                    NPC.GetComponentInChildren<Transform>().GetComponentInChildren<Clothes>().GetComponent<SpriteRenderer>().color = Color.cyan;
                    GetComponentInChildren<WeaponSlotController>().RemoveWeapon(GetComponentInChildren<WeaponSlotController>().GetComponentInChildren<Paint>().gameObject);
                }
            }
        }
    }
}