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
    private AudioSource audioSource;
    public AudioClip unlockSound;
    public AudioClip openSound;
    public AudioClip closeSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open(bool wasLocked = false)
    {
        isOpened = true;
        closedPosition.SetActive(false);
        openedPosition.SetActive(true);
        if (wasLocked) return;
        audioSource.clip = openSound;
        audioSource.Play();
    }

    public void Close()
    {
        isOpened = false;
        closedPosition.SetActive(true);
        openedPosition.SetActive(false);
        audioSource.clip = closeSound;
        audioSource.Play();
    }

    public void Unlock()
    {
        isLocked = false;
        audioSource.clip = unlockSound;
        audioSource.Play();
        Open(true);
    }
}