using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;
    public bool haveWeapon;

    void Start()
    {
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
    }
}