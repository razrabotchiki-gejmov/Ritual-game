using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private GameObject itemSlot;

    // public GameObject player;
    public Interaction interaction;
    public Image image;
    public int type;
    public GameObject backlight;
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
        // backlight = GetComponentsInChildren<Transform>()[1].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

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
        item.RemoveBacklight();
        itemSlot.GetComponent<ItemSlotController>().AddWeapon(item.gameObject);
        item.gameObject.name = gameObject.name;
        if (type <= 2) interaction.haveWeapon = true;
        if (type == 3) interaction.havePoison = true;
        if (type == 4) interaction.havePaint = true;
        if (type == 5)
        {
            interaction.haveCoin = true;
            interaction.coinPoint.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (type == 6) interaction.haveKey = true;
        item.audioSource.clip = pickUpSound;
        item.audioSource.Play();
        image.color = GetComponent<SpriteRenderer>().color;
        image.sprite = GetComponent<SpriteRenderer>().sprite;
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
    }

    public void AddBacklight()
    {
        backlight.SetActive(true);
    }

    public void RemoveBacklight()
    {
        backlight.SetActive(false);
    }
}