using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed;
    private float timeToRotate;
    public List<float> cooldowns = new();
    public List<Vector3> rotations;
    private int rotIndex;

    // public List<Sprite> sprites = new();
    // private SpriteRenderer spriteRenderer;
    private Transform body;

    void Start()
    {
        body = GetComponentInChildren<NPCVision>().transform;
        // spriteRenderer = GetComponent<SpriteRenderer>();
        if (cooldowns.Count > 0)
            timeToRotate = cooldowns[0];
        // npcState = GetComponent<NPCState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotations.Count > 0)
        {
            
            timeToRotate -= Time.deltaTime;
            if (timeToRotate <= 0)
            {
                Rotate();
                ChangeRotation();
            }
        }
    }

    public void Rotate()
    {
        // Идёт вертикально
        body.rotation = Quaternion.Euler(rotations[rotIndex]);
    }

    public void ChangeRotation()
    {
        rotIndex = (rotIndex + 1) % rotations.Count;
        timeToRotate = cooldowns[rotIndex];
    }
}