using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persuadable : MonoBehaviour
{
    // Start is called before the first frame update
    // public Vector2 point;
    private NPCMovement movement;
    public List<Vector2> spots;
    private int spotIndex;
    public List<float> cooldowns = new();
    private SpriteChanger spriteChanger;
    private float timeToMove;
    private Transform body;
    public bool isEnabled;
    
    void Start()
    {
        movement = GetComponent<NPCMovement>();
        body = GetComponentInChildren<NPCVision>().transform;
        if (cooldowns.Count > 0) timeToMove = cooldowns[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled) return;
        if (spots.Count > 0 && cooldowns.Count > 0)
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
        // Vector2.MoveTowards(transform.position, point, movement.speed);
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
            (Vector3)Vector2.MoveTowards(transform.position, spots[spotIndex], movement.speed * Time.deltaTime) +
            Vector3.forward * transform.position.z;
        // transform.position = Vector2.MoveTowards(transform.position, spots[spotIndex], speed * Time.deltaTime);
    }
    public void ChangeSpot()
    {

        spotIndex +=1;
        if (spotIndex>=cooldowns.Count) End();
        else timeToMove = cooldowns[spotIndex];
    }

    public void StartMethod()
    {
        movement.enabled = false;
        isEnabled = true;
    }

    public void End()
    {
        isEnabled = false;
        movement.enabled = false;
    }
}
