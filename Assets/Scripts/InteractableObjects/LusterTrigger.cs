using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LusterTrigger : MonoBehaviour
{
    public LusterConstruction lusterConstruction;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        lusterConstruction = GetComponentInParent<LusterConstruction>();
    }
    public void ShowHint()
    {
        canvas.SetActive(true);
    }
    public void HideHint()
    {
        canvas.SetActive(false);
    }
}
