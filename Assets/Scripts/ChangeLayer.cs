using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            var newPos = other.transform.position;
            newPos.z = transform.parent.position.z + 0.01f;
            other.transform.position = newPos;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            var newPos = other.transform.position;
            newPos.z = -3;
            other.transform.position = newPos;
        }
    }
}