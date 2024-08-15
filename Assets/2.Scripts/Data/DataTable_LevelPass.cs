using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_LevelPass
{
    /// <summary>
    /// index
    /// </summary>
    public int key;

    /// <summary>
    /// level
    /// </summary>
    public int Level;

    /// <summary>
    /// type
    /// </summary>
    public int LevelType;

    /// <summary>
    /// type
    /// </summary>
    public int GoldenType;

    /// <summary>
    /// count
    /// </summary>
    public int LevelValue;

    /// <summary>
    /// count
    /// </summary>
    public int GoldenValue;

}
public class DataTable_LevelPassLoader
{
    public List<DataTable_LevelPass> ItemsList { get; private set; }
    public Dictionary<int, DataTable_LevelPass> ItemsDict { get; private set; }

    public DataTable_LevelPassLoader(string path = "JSON/DataTable_LevelPass")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_LevelPass>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_LevelPass> Items;
    }

    public DataTable_LevelPass GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
