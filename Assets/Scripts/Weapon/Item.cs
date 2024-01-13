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
    public Image image;
    public TextMeshProUGUI name;
    public int type;
    public GameObject backlight;
    public GameObject canvas;
    public AudioSource audioSource;
    public AudioClip pickUpSound;
    public AudioClip dropSound;
    // 0 - нож, 1 - камень, 2 - жила животного, 3 - яд, 4 - краска, 5 - монета, 6 - ключи
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameData.Items.Contains(gameObject.name))
        {
            Destroy(gameObject);
        }

        var player = GameObject.FindWithTag("Player");
        interaction = player.GetComponent<Interaction>();
        itemSlot = player.GetComponentInChildren<ItemSlotController>().gameObject;
        image = GameObject.Find("ItemImage").GetComponent<Image>();
        name = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        // backlight = GetComponentsInChildren<Transform>()[1].gameObject;
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
        if (type <= 2) interaction.haveWeapon = true;
        if (type == 0) name.text = "Нож";
        if (type == 1) name.text = "Камень";
        if (type == 2) name.text = "Жила";
        if (type == 3)
        {
            name.text = "Яд";
            interaction.havePoison = true;
            if (interaction.food != null) interaction.food.CanBePoisoned();
        }

        if (type == 4)
        {
            name.text = "Краска";
            interaction.havePaint = true;
        }

        if (type == 5)
        {
            name.text = "Монета";
            interaction.haveCoin = true;
            interaction.coinPoint.GetComponent<SpriteRenderer>().enabled = true;
            if (interaction.isCoinPointNear) interaction.coinPoint.ShowHint();
            if (interaction.lusterTrigger != null) interaction.lusterTrigger.HideHint();
            interaction.coinPoint.isCoinHere = false;
        }

        if (type == 6)
        {
            name.text = "Ключ";
            interaction.haveKey = true;
            if (interaction.door != null && interaction.door.isLocked)
                interaction.door.GetComponentInChildren<DoorHint>().DoorUnlockHint();
        }

        item.audioSource.clip = pickUpSound;
        item.audioSource.Play();
        image.enabled = true;
        name.enabled = true;
        image.sprite = GetComponent<SpriteRenderer>().sprite;
        image.preserveAspect = true;
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
        image.enabled = false;
        name.enabled = false;
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