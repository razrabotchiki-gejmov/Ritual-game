using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject weaponSlot;
    public GameObject player;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PickUpWeapon()
    {
        var weapon = Instantiate(this, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot.transform;
        weapon.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
        player.GetComponent<InteractWithNPC>().haveWeapon = true;
    }
}