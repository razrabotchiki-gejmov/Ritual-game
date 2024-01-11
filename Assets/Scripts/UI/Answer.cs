using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isBad;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Answered()
    {
        if (isBad) GameData.Chances -= 1;
    }
}