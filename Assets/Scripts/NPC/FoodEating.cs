using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEating : MonoBehaviour
{
    // Start is called before the first frame update
    public Food food;
    public NPCState state;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatFood()
    {
        if (food.isPoisoned) state.Die();
    }
}
