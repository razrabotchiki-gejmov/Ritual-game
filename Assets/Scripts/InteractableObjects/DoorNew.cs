using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNew : MonoBehaviour
{
    public bool isOpened;
    public GameObject openedPosition;
    public GameObject closedPosition;
    public bool isLocked;

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
        isOpened = true;
        closedPosition.SetActive(false);
        openedPosition.SetActive(true);
    }

    public void Close()
    {
        isOpened = false;
        closedPosition.SetActive(true);
        openedPosition.SetActive(false);
    }

    public void Unlock()
    {
        isLocked = false;
        Open();
    }
}