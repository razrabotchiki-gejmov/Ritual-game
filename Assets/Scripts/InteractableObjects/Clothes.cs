using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    public bool isInBlood = false;
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

    public void PaintInBlood()
    {
        isInBlood = true;
    }
}
