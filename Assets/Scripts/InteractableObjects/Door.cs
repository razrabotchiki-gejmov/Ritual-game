using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] bool leftRight;
    [SerializeField] bool upDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Open()
    {
        isOpen = true;
        if (upDown)
        {
            transform.localScale = new Vector3(0.2f, 2f, 0);
            transform.position = transform.position + new Vector3(0.5f, 0, 0f);
        }
        if (leftRight)
        {
            transform.localScale = new Vector3(1f, 0.2f, 0);
            transform.position = transform.position + new Vector3(0, 0.5f, 0f);
        }      
    }

    public void Close()
    {
        isOpen = false;
        if (upDown)
        {
            transform.localScale = new Vector3(1f, 2f, 1f);
            transform.position = transform.position - new Vector3(0.5f, 0f, 0f);
        }
        if (leftRight)
        {
            transform.localScale = new Vector3(0.2f, 2f, 0);
            transform.position = transform.position - new Vector3(0f, 0.5f, 0f);
        }
    }
}
