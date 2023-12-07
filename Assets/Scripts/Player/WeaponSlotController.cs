using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSlotController : MonoBehaviour
{
    public List<string> weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(GameObject weapon)
    {
        weapons.Add(weapon.name);
    }

    public void RemoveWeapon(GameObject weapon)
    {
        weapons.Remove(weapon.name);
        Destroy(weapon);
    }
}
