using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        // _rb.velocity = Vector2.left * 2;
    }


    public void Move()
    {
        var methodValue = Random.Range(0, 4);
        Debug.Log(methodValue);
        if (methodValue == 0)
        {
            MoveLeft();
        }
        else if (methodValue == 1)
        {
            MoveDown();
        }
        else if (methodValue == 2)
        {
            MoveRight();
        }
        else
        {
            MoveUp();
        }

        Invoke(nameof(Stop), 3);
    }

    public void MoveLeft()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        _rb.velocity = Vector2.left * 2;
    }

    public void MoveRight()
    {
        transform.localRotation = new Quaternion(0, 0, 180, 0);
        _rb.velocity = Vector2.right * 2;
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
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
        Invoke(nameof(Move), 1);
    }
}