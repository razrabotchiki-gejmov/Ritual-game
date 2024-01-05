using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementTest : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public Vector2 spot;
    public List<Sprite> sprites = new();
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (spot - (Vector2)transform.position).normalized;
        if (dir.magnitude > 0.1) Move(dir);
    }

    public void Move(Vector2 dir)
    {
        // Идёт вертикально
        if (Mathf.Abs(dir.x) < 0.3)
        {
            // Смотрит вверх
            if (dir.y > 0) spriteRenderer.sprite = sprites[0];
            // Смотрит вниз
            else spriteRenderer.sprite = sprites[1];
        }
        // Идёт горизонтально
        else if (Mathf.Abs(dir.y) < 0.3)
        {
            // Смотрит вправо
            if (dir.x > 0) spriteRenderer.sprite = sprites[2];
            // Смотрит влево
            else spriteRenderer.sprite = sprites[3];
        }
        // Диагональное движение
        else if (dir.x > 0.3 && dir.y > 0.3)
        {
        }
        else if (dir.x > 0.3 && dir.y < -0.3)
        {
        }
        else if (dir.x < -0.3 && dir.y < -0.3)
        {
        }
        else if (dir.x < -0.3 && dir.y > 0.3)
        {
        }

        transform.position = Vector2.MoveTowards(transform.position, spot, speed * Time.deltaTime);
    }
}