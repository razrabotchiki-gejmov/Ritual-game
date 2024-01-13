using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public static class GameData
{
    // public static string filepath = Application.persistentDataPath + "GameData.gamesave";

    // Start is called before the first frame update
    public static int Day = 1;
    public static int Chances = 3;
    public static List<string> Names = new();
    public static List<string> Items = new();
    public static List<string> SpokenPhrases = new();
    public static List<int> SpokeWithPotentialKiller = new() { 0, 0, 0 };
    public static bool TalkedToFather;

    public static void Reset()
    {
        Day = 1;
        Chances = 3;
        Names = new();
        Items = new();
        SpokenPhrases = new();
        SpokeWithPotentialKiller = new() { 0, 0, 0 };
        TalkedToFather = false;
    }

    public static void Save()
    {
        YandexGame.savesData.day = Day;
        YandexGame.savesData.chances = Chances;
        YandexGame.savesData.names = Names.ToList();
        YandexGame.savesData.items = Items.ToList();
        YandexGame.savesData.spokenPhrases = SpokenPhrases.ToList();
        YandexGame.savesData.spokeWithPotentialKiller = SpokeWithPotentialKiller.ToList();
        
        
        // YandexGame.SaveProgress();
        // var bf = new BinaryFormatter();
        // var fs = new FileStream(filepath, FileMode.Create);
        // var save = new Save();
        // save.MakeSave();
        // ;
        // bf.Serialize(fs, save);
        // fs.Close();
    }

    public static void Load()
    {
        Day = YandexGame.savesData.day;
        Chances = YandexGame.savesData.chances;
        Names = YandexGame.savesData.names;
        Items = YandexGame.savesData.items;
        SpokenPhrases = YandexGame.savesData.spokenPhrases;
        SpokeWithPotentialKiller = YandexGame.savesData.spokeWithPotentialKiller;
        
        
        
        // if (!File.Exists(filepath)) return;
        // var bf = new BinaryFormatter();
        // var fs = new FileStream(filepath, FileMode.Open);
        // var save = (Save)bf.Deserialize(fs);
        // save.LoadSave();
        // fs.Close();
    }
}
//
// [System.Serializable]
// public class Save
// {
//     public int day;
//     public int chances;
//     public List<string> names;
//     public List<string> items;
//     public List<string> spokenPhrases;
//     public List<int> spokeWithPotentialKiller1;
//
//     public void MakeSave()
//     {
//         day = GameData.Day;
//         chances = GameData.Chances;
//         names = GameData.Names;
//         items = GameData.Items;
//         spokenPhrases = GameData.SpokenPhrases;
//         spokeWithPotentialKiller1 = GameData.SpokeWithPotentialKiller;
//     }
//
//     public void LoadSave()
//     {
//         GameData.Day = day;
//         GameData.Chances = chances;
//         GameData.Names = names;
//         GameData.Items = items;
//         GameData.SpokenPhrases = spokenPhrases;
//         GameData.SpokeWithPotentialKiller = spokeWithPotentialKiller1;
//     }
// }