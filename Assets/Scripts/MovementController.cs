using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Controls _input;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _input = new Controls();
        _input.Enable();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _input.Movement.Move.ReadValue<Vector2>() * speed;
    }
}
