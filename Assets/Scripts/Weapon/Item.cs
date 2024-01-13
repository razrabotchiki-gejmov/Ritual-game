using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private GameObject itemSlot;

    // public GameObject player;
    public Interaction interaction;
    public int type;
    public GameObject backlight;
    public GameObject canvas;
    public AudioSource audioSource;
    public AudioClip pickUpSound;
    public GameManager gameManager;
    public AudioClip dropSound;

    // 0 - нож, 1 - камень, 2 - жила животного, 3 - яд, 4 - краска, 5 - монета, 6 - ключи
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameData.Items.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var player = GameObject.FindWithTag("Player");
        interaction = player.GetComponent<Interaction>();
        itemSlot = player.GetComponentInChildren<ItemSlotController>().gameObject;
    }

    // Update is called once per frame


    public void PickUpWeapon()
    {
        if (itemSlot.GetComponentsInChildren<Transform>().Length > 1)
        {
            SwapEquippedWeapon();
        }

        var item = Instantiate(this, itemSlot.transform.position, itemSlot.transform.rotation);
        item.transform.SetParent(itemSlot.transform);
        item.transform.localScale = transform.lossyScale;
        item.GetComponent<Collider2D>().enabled = false;
        item.HideHint();
        itemSlot.GetComponent<ItemSlotController>().AddWeapon(item.gameObject);
        item.gameObject.name = gameObject.name;
        var name = "";
        if (type <= 2) interaction.haveWeapon = true;
        if (type == 0) name = "Нож";
        if (type == 1) name = "Камень";
        if (type == 2) name = "Жила";
        if (type == 3)
        {
            name = "Яд";
            interaction.havePoison = true;
            if (interaction.food != null) interaction.food.CanBePoisoned();
        }

        if (type == 4)
        {
            name = "Краска";
            interaction.havePaint = true;
        }

        if (type == 5)
        {
            name = "Монета";
            interaction.haveCoin = true;
            gameManager.ShowLusterInfo(false);
            interaction.coinPoint.GetComponent<SpriteRenderer>().enabled = true;
            if (interaction.isCoinPointNear) interaction.coinPoint.ShowHint();
            if (interaction.lusterTrigger != null) interaction.lusterTrigger.HideHint();
            interaction.coinPoint.isCoinHere = false;
        }

        if (type == 6)
        {
            name = "Ключ";
            interaction.haveKey = true;
            if (interaction.door != null && interaction.door.isLocked)
                interaction.door.GetComponentInChildren<DoorHint>().DoorUnlockHint();
        }

        item.audioSource.clip = pickUpSound;
        item.audioSource.Play();
        gameManager.ShowItem(GetComponent<SpriteRenderer>().sprite, name);
        Destroy(gameObject);
    }

    public void SwapEquippedWeapon()
    {
        var equippedItem = itemSlot.GetComponentInChildren<SpriteRenderer>().gameObject;
        var droppedItem = Instantiate(equippedItem,
            GetComponent<Transform>().position, GetComponent<Transform>().rotation);
        droppedItem.GetComponent<Collider2D>().enabled = true;
        droppedItem.name = equippedItem.name;
        interaction.haveWeapon = false;
        interaction.havePoison = false;
        interaction.havePaint = false;
        interaction.haveKey = false;
        interaction.haveCoin = false;
        interaction.coinPoint.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(equippedItem);
    }

    public void DropWeapon(Transform playerTransform)
    {
        var equippedItem = gameObject;
        var droppedWeapon = Instantiate(equippedItem,
            playerTransform.position, playerTransform.rotation);
        droppedWeapon.GetComponent<Collider2D>().enabled = true;
        droppedWeapon.name = equippedItem.name;
        interaction.haveWeapon = false;
        interaction.havePoison = false;
        interaction.haveKey = false;
        interaction.havePaint = false;
        interaction.haveCoin = false;
        interaction.coinPoint.GetComponent<SpriteRenderer>().enabled = false;
        var droppedWeaponAudio = droppedWeapon.GetComponent<Item>().audioSource;
        droppedWeaponAudio.clip = dropSound;
        droppedWeaponAudio.Play();
        Destroy(equippedItem);
        gameManager.HideItem();
    }

    public void ShowHint()
    {
        backlight.SetActive(true);
        canvas.SetActive(true);
    }

    public void HideHint()
    {
        backlight.SetActive(false);
        canvas.SetActive(false);
    }
}