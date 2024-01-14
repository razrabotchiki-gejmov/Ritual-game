using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer bloodSpriteRenderer;
    public List<Sprite> sprites = new();
    public List<Sprite> bloodSprites = new();

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
        if (bloodSprites.Count != 0) bloodSpriteRenderer.sprite = null;
        // bloodSpriteRenderer.sprite = bloodSprites[0];
    }

    public void LookDown()
    {
        spriteRenderer.sprite = sprites[1];
        if (bloodSprites.Count != 0) bloodSpriteRenderer.sprite = bloodSprites[0];
    }

    public void LookRight()
    {
        spriteRenderer.sprite = sprites[2];
        if (bloodSprites.Count != 0) bloodSpriteRenderer.sprite = bloodSprites[1];
    }

    public void LookLeft()
    {
        spriteRenderer.sprite = sprites[3];
        if (bloodSprites.Count != 0) bloodSpriteRenderer.sprite = bloodSprites[2];
    }
}