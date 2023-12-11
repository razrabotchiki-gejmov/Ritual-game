using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLevel : MonoBehaviour
{
    [SerializeField] private Transform nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = nextLevel.position - new Vector3(2,0,0);
    }
}
