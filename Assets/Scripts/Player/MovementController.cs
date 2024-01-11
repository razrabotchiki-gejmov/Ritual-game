using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Controls _input;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _input = new Controls();
        _input.Enable();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = _input.Movement.Move.ReadValue<Vector2>();
        _rb.velocity = dir * speed;
        if (dir == Vector2.zero) return;
        // Идёт вертикально
        if (Mathf.Abs(dir.x) < 0.3)
        {
            // Смотрит вверх
            if (dir.y > 0)
            {
                spriteRenderer.sprite = sprites[0];
            }
            // Смотрит вниз
            else
            {
                spriteRenderer.sprite = sprites[1];
            }
        }
        // Идёт горизонтально
        else if (Mathf.Abs(dir.y) < 0.3)
        {
            // Смотрит вправо
            if (dir.x > 0)
            {
                spriteRenderer.sprite = sprites[2];
            }
            // Смотрит влево
            else
            {
                spriteRenderer.sprite = sprites[3];
            }
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
        // _rb.velocity = _input.Movement.Move.ReadValue<Vector2>() * speed;
    }
}