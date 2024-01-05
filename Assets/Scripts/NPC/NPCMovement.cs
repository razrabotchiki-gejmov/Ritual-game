using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float timeToMove;
    public List<float> cooldowns = new();
    public List<Vector2> spots;
    public int spotIndex;
    public List<Sprite> sprites = new();
    private SpriteRenderer spriteRenderer;
    private Transform body;

    void Start()
    {
        body = GetComponentInChildren<NPCVision>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeToMove = cooldowns[0];
    }

    // Update is called once per frame
    void Update()
    {
        var dir = (spots[spotIndex] - (Vector2)transform.position).normalized;
        if (dir.magnitude > 0.1) Move(dir);
        else
        {
            timeToMove -= Time.deltaTime;
        }

        if (timeToMove <= 0)
        {
            ChangeSpot();
        }
    }

    public void Move(Vector2 dir)
    {
        // Идёт вертикально
        if (Mathf.Abs(dir.x) < 0.3)
        {
            // Смотрит вверх
            if (dir.y > 0)
            {
                spriteRenderer.sprite = sprites[0];
                body.rotation = Quaternion.Euler(0, 0, 90);
            }
            // Смотрит вниз
            else
            {
                spriteRenderer.sprite = sprites[1];
                body.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
        // Идёт горизонтально
        else if (Mathf.Abs(dir.y) < 0.3)
        {
            // Смотрит вправо
            if (dir.x > 0)
            {
                spriteRenderer.sprite = sprites[2];
                body.rotation = Quaternion.Euler(0, 0, 0);
            }
            // Смотрит влево
            else
            {
                spriteRenderer.sprite = sprites[3];
                body.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        // Диагональное движение
        else if (dir.x > 0.3 && dir.y > 0.3)
        {
            body.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (dir.x > 0.3 && dir.y < -0.3)
        {
            body.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (dir.x < -0.3 && dir.y < -0.3)
        {
            body.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (dir.x < -0.3 && dir.y > 0.3)
        {
            body.rotation = Quaternion.Euler(0, 0, 135);
        }

        transform.position = Vector2.MoveTowards(transform.position, spots[spotIndex], speed * Time.deltaTime);
    }

    public void ChangeSpot()
    {
        spotIndex = (spotIndex + 1) % spots.Count;
        timeToMove = cooldowns[spotIndex];
    }
}