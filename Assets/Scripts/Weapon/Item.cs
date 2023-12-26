using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private GameObject itemSlot;
    public GameObject player;
    public Image image;
    public int type;
    public GameObject backlight;

    // 0 - нож, 1 - камень, 2 - жила животного, 3 - яд, 4 - краска, 5 - монета, 6 - ключи
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        item.transform.parent = itemSlot.transform;
        item.GetComponent<Collider2D>().enabled = false;
        item.RemoveBacklight();
        itemSlot.GetComponent<ItemSlotController>().AddWeapon(item.gameObject);
        if (type <= 2) player.GetComponent<Interaction>().haveWeapon = true;
        if (type == 3) player.GetComponent<Interaction>().havePoison = true;
        if (type == 4) player.GetComponent<Interaction>().havePaint = true;
        if (type == 6) player.GetComponent<Interaction>().haveKey = true;
        image.color = GetComponent<SpriteRenderer>().color;
        image.sprite = GetComponent<SpriteRenderer>().sprite;
        Destroy(gameObject);
    }

    public void SwapEquippedWeapon()
    {
        var equippedItem = itemSlot.GetComponentInChildren<SpriteRenderer>().gameObject;
        var droppedWeapon = Instantiate(equippedItem,
            GetComponent<Transform>().position, GetComponent<Transform>().rotation);
        droppedWeapon.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<Interaction>().haveWeapon = false;
        player.GetComponent<Interaction>().havePoison = false;
        player.GetComponent<Interaction>().havePaint = false;
        player.GetComponent<Interaction>().haveKey = false;
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