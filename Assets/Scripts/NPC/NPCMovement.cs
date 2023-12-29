using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    public float speed;
    public float dir = 1;
    public GameObject body;
    public bool isMoveToPoint;
    public bool isMoveToStartPoint;
    public float timeToReturn = 60;
    public float timeToMove = 3;
    public Vector2 startPoint;
    public bool isMovingToCoin = false;
    private bool isStopped = false;

    void Start()
    {
        startPoint = transform.position;
        body.transform.rotation = new Quaternion(0, 0, 0, 0);
        dir = 1;
        _rb = GetComponent<Rigidbody2D>();
        MoveRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped && !isMovingToCoin)
        {
            // _rb.velocity = Vector2.left * 2;
            if (isMoveToPoint) MoveToPoint(new Vector3(9, 9, 0));
            if (timeToReturn <= 0)
            {
                timeToReturn = 60;
                isMoveToPoint = false;
                isMoveToStartPoint = true;
            }

            if (isMoveToStartPoint) ReturnToStartPoint();
            if (timeToMove <= 0)
            {
                timeToMove = 3;
                isMoveToStartPoint = false;
                Start();
            }
        }
    }


    // public void Move()
    // {
    //     var dir =
    //         var methodValue = Random.Range(0, 4);
    //     Debug.Log(methodValue);
    //     if (methodValue == 0)
    //     {
    //         MoveLeft();
    //     }
    //     else if (methodValue == 1)
    //     {
    //         MoveDown();
    //     }
    //     else if (methodValue == 2)
    //     {
    //         MoveRight();
    //     }
    //     else
    //     {
    //         MoveUp();
    //     }
    //
    //     Invoke(nameof(Stop), 3);
    // }
    public void RotateBody()
    {
        body.transform.Rotate(0, 0, 180);
    }

    public void MoveLeft()
    {
        RotateBody();
        // transform.localRotation = new Quaternion(0, 0, 0, 0);
        _rb.velocity = Vector2.left * speed;
        Invoke(nameof(Stop), 3);
    }

    public void MoveRight()
    {
        RotateBody();
        // transform.localRotation = new Quaternion(0, 0, 180, 0);
        _rb.velocity = Vector2.right * speed;
        Invoke(nameof(Stop), 3);
    }

    public void MoveUp()
    {
        transform.localRotation = new Quaternion(0, 0, 270, 0);
        _rb.velocity = Vector2.up * 2;
    }

    public void MoveDown()
    {
        transform.localRotation = new Quaternion(0, 0, 90, 0);
        _rb.velocity = Vector2.down * 2;

        // Invoke(nameof(Stop), 3);
    }

    public void Stop()
    {
        dir = (dir + 1) % 2;
        _rb.velocity = Vector2.zero;
        if (dir == 0)
        {
            Invoke(nameof(MoveLeft), 1);
        }
        else
        {
            Invoke(nameof(MoveRight), 1);
        }
    }

    public void FullStop()
    {
        _rb.velocity = Vector2.zero;
        CancelInvoke();
        isMoveToPoint = false;
        isStopped = true;
    }

    public void MoveToPoint(Vector2 point)
    {
        var vector = point - (Vector2)transform.position;
        _rb.velocity = Vector2.Min(vector, (vector).normalized) * 2;
        timeToReturn -= Time.deltaTime;
    }

    public void ReturnToStartPoint()
    {
        var vector = startPoint - (Vector2)transform.position;
        var normalizedVector = (vector).normalized;
        if (normalizedVector.magnitude > vector.magnitude)
        {
            timeToMove -= Time.deltaTime;
            _rb.velocity = vector * 2;
        }
        else
        {
            _rb.velocity = normalizedVector * 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Item>().type == 5)
        {
            FullStop();
        }
    }
}