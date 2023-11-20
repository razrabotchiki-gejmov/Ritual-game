using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    [SerializeField] private GameObject clothesSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUpClothes()
    {
        var clothes = Instantiate(this, clothesSlot.transform.position, clothesSlot.transform.rotation);
        clothes.transform.parent = clothesSlot.transform;
        clothes.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}
