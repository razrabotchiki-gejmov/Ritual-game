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
    public List<int> methods = new();
    private SpriteChanger spriteChanger;

    // 0 - ничего
    // 1 - поесть
    public List<Vector2> spots;
    private int spotIndex;
    private Transform body;
    public bool cannotMove;

    void Start()
    {
        spriteChanger = GetComponent<SpriteChanger>();
        body = GetComponentInChildren<NPCVision>().transform;
        if (cooldowns.Count > 0) timeToMove = cooldowns[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (cannotMove) return;
        if (spots.Count > 0 && cooldowns.Count > 0 && methods.Count > 0)
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
    }

    public void Move(Vector2 dir)
    {
        // Идёт вертикально
        if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            // Смотрит вверх
            if (dir.y > 0)
            {
                spriteChanger.LookUp();
                body.rotation = Quaternion.Euler(0, 0, 90);
            }
            // Смотрит вниз
            else
            {
                spriteChanger.LookDown();
                body.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
        // Идёт горизонтально
        else
        {
            // Смотрит вправо
            if (dir.x > 0)
            {
                spriteChanger.LookRight();
                body.rotation = Quaternion.Euler(0, 0, 0);
            }
            // Смотрит влево
            else
            {
                spriteChanger.LookLeft();
                body.rotation = Quaternion.Euler(0, 0, 180);
            }
        }

        transform.position =
            (Vector3)Vector2.MoveTowards(transform.position, spots[spotIndex], speed * Time.deltaTime) +
            Vector3.forward * transform.position.z;
        // transform.position = Vector2.MoveTowards(transform.position, spots[spotIndex], speed * Time.deltaTime);
    }

    public void ChangeSpot()
    {
        if (methods[spotIndex] == 1)
        {
            GetComponent<FoodEating>().EatFood();
        }

        spotIndex = (spotIndex + 1) % spots.Count;
        timeToMove = cooldowns[spotIndex];
    }
}