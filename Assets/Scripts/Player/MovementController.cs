using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Controls _input;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer bloodSpriteRenderer;
    public List<Sprite> sprites;
    public List<Sprite> bloodSprites;
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
        if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            // Смотрит вверх
            if (dir.y > 0)
            {
                spriteRenderer.sprite = sprites[0];
                bloodSpriteRenderer.sprite = null;
            }
            // Смотрит вниз
            else
            {
                spriteRenderer.sprite = sprites[1];
                bloodSpriteRenderer.sprite = bloodSprites[0];
            }
        }
        // Идёт горизонтально
        else
        {
            // Смотрит вправо
            if (dir.x > 0)
            {
                spriteRenderer.sprite = sprites[2];
                bloodSpriteRenderer.sprite = bloodSprites[1];
            }
            // Смотрит влево
            else
            {
                spriteRenderer.sprite = sprites[3];
                bloodSpriteRenderer.sprite = bloodSprites[2];
            }
        }
    }
}