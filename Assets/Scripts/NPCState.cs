using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDead;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSpeak()
    {
        if (isDead)
        {
            return;
        }

        transform.Find("DialogWindow").gameObject.SetActive(true);
        Invoke(nameof(StopSpeak), 5f);
    }

    public void StopSpeak()
    {
        transform.Find("DialogWindow").gameObject.SetActive(false);
    }

    public void Die()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        isDead = true;
    }
}