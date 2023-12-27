using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LusterTrigger : MonoBehaviour
{
    public LusterConstruction lusterConstruction;
    // Start is called before the first frame update
    void Start()
    {
        lusterConstruction = GetComponentInParent<LusterConstruction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
