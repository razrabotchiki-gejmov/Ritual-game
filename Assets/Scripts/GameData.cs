using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // Start is called before the first frame update
    public static int Day = 1;
    public static int Chances = 3;
    public static List<string> Names = new();
    public static List<string> Items = new();

    public static void Reset()
    {
        Day = 1;
        Chances = 3;
        Names = new();
        Items = new();
    }
}