using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Food : MonoBehaviour
{
    public bool isPoisoned;

    public GameObject backlight;
    public GameObject canvas;
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
        CannotBePoisoned();
    }
    public void CanBePoisoned()
    {
        backlight.SetActive(true);
        canvas.SetActive(true);
    }

    public void CannotBePoisoned()
    {
        backlight.SetActive(false);
        canvas.SetActive(false);
    }
}
