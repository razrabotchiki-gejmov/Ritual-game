using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // gameObject.transform.Translate(playerTransform.position);
        gameObject.transform.position = playerTransform.position + Vector3.back * 10;
    }
}