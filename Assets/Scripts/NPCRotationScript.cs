using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotationScript : MonoBehaviour
{
    public GameObject NPC;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (NPC.GetComponent<NPCState>().isDead)
        {
            enabled = false;
        }

        Rotate(Time.deltaTime * 10);
    }

    public void Rotate(float angle)
    {
        transform.Rotate(0, 0, angle);
    }
}