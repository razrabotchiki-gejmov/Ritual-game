using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    // 2 - телепортироваться к следующей точке
    public List<Vector2> spots;
    private int spotIndex;
    private Transform body;
    public bool cannotMove;
    private bool isMovingToPoint;
    private CoinPoint coinPoint;
    private NPCState npcState;
    public bool isPlayerDetected;
    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        npcState = GetComponent<NPCState>();
        spriteChanger = GetComponent<SpriteChanger>();
        body = GetComponentInChildren<NPCVision>().transform;
        if (cooldowns.Count > 0) timeToMove = cooldowns[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDetected) LookAtPoint(playerTransform);
        if (cannotMove) return;
        if (isMovingToPoint)
        {
            if (!coinPoint.isCoinHere) isMovingToPoint = false;
            var dir = (Vector2)coinPoint.transform.position - (Vector2)transform.position;
            if (dir.magnitude > 0.1)
            {
                transform.position =
                    (Vector3)Vector2.MoveTowards(transform.position, coinPoint.transform.position,
                        speed * Time.deltaTime) +
                    Vector3.forward * transform.position.z;
            }
        }
        else if (spots.Count > 0 && cooldowns.Count > 0 && methods.Count > 0)
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
        if (methods[spotIndex] == 2)
        {
            transform.position = spots[spotIndex];
            spotIndex = (spotIndex + 1) % spots.Count;
        }

        timeToMove = cooldowns[spotIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CoinPoint") && npcState.type <= 1 && other.GetComponent<CoinPoint>().isCoinHere)
        {
            isMovingToPoint = true;
            coinPoint = other.GetComponent<CoinPoint>();
        }
    }

    public void LookAtPoint(Transform point)
    {
        var dir = (Vector2)point.transform.position - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
        body.rotation = Quaternion.Euler(0, 0, angle);
        if (angle is <= 45 and > -45)
        {
            spriteChanger.LookRight();
        }
        else if (angle is > 45 and <= 135)
        {
            spriteChanger.LookUp();
        }
        else if (angle is > 135 or <= -135)
        {
            spriteChanger.LookLeft();
        }
        else if (angle is > -135 and <= -45)
        {
            spriteChanger.LookDown();
        }
    }
}