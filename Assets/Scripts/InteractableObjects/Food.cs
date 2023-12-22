using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Food : MonoBehaviour
{
    public bool isPoisoned = false;
    [SerializeField] private GameObject poison;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BecomePoisoned()
    {
        isPoisoned = true;
    }
}
