using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            var npcState = other.GetComponent<NPCState>();
            if (npcState.type <= 1) npcState.Die();
            Destroy(GetComponentInParent<LusterConstruction>().gameObject);
        }
    }
}