using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    private DoorNew doorNew;

    // Start is called before the first frame update
    void Start()
    {
        doorNew = GetComponentInParent<DoorNew>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            doorNew.Open();
        }
    }
}