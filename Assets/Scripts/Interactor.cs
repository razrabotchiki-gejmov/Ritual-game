using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public bool isInteractInRange;
    private Controls _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = new Controls();
        _input.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isn't in range");
        if(isInteractInRange)
        {
            Debug.Log("is in range");
            if(tag == "Door")
            {
                Debug.Log("this is door");
                if (_input.Interact.Door.WasPerformedThisFrame())
                {
                    Debug.Log("Door is interacted");
                    var door = GetComponent<Door>();
                    Debug.Log(door);
                    if (!door.isOpen)                   
                        door.Open();
                    else
                        door.Close();
                }
            }
            if(tag=="Weapon")
            {
                Debug.Log("this is weapon");
                if(_input.Interact.PickupWeapon.WasPerformedThisFrame())
                {
                    var weapon = GetComponent<WeaponScript>();
                    Debug.Log("Weapon pickuped");
                    isInteractInRange = false;
                    weapon.PickUpWeapon();
                    
                }
            }
            if (tag == "Clothes")
            {
                Debug.Log("this is clothes");
                if (_input.Interact.PickupClothes.WasPerformedThisFrame())
                {
                    var weapon = GetComponent<Clothes>();
                    Debug.Log("clothes pickuped");
                    isInteractInRange = false;
                    weapon.PickUpClothes();

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInteractInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteractInRange = false;
        }
    }

}