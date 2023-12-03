using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject weaponSlot;
    // Start is called before the first frame update
    private Controls _input;
    void Start()
    {
        _input = new Controls();
        _input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            if (collision.GetComponent<Food>().isPoisened != true)
            {
                Debug.Log("In range of food");
                if (weaponSlot.GetComponentInChildren<Poison>())
                {
                    Debug.Log("Poison found");
                    if (_input.Interact.InteractWithObjects.WasPerformedThisFrame())
                    {
                        Debug.Log("Food poisoned");
                        collision.GetComponent<Food>().isPoisened = true;
                        weaponSlot.GetComponent<WeaponSlotController>().RemoveWeapon(weaponSlot.GetComponentInChildren<Poison>().gameObject);
                    }
                }
            }
        }
    }
}
