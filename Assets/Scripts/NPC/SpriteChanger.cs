using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites = new();
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LookUp()
    {
        spriteRenderer.sprite = sprites[0];
    }
    public void LookDown()
    {
        spriteRenderer.sprite = sprites[1];
    }
    public void LookRight()
    {
        spriteRenderer.sprite = sprites[2];
    }
    public void LookLeft()
    {
        spriteRenderer.sprite = sprites[3];
    }
}
