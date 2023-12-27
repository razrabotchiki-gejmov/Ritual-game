using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LusterConstruction : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform luster;
    public Transform impactPoint;
    public bool isLusterFalling;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLusterFalling)
        {
            luster.position = Vector3.MoveTowards(luster.position, impactPoint.position, 0.1f);
        }
        if (luster.position == impactPoint.position) Destroy(gameObject);
    }

    public void DropLuster()
    {
        isLusterFalling = true;
        luster.GetComponent<Collider2D>().enabled = true;
    }
}