using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Story
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// Text
    /// </summary>
    public string Text;

    /// <summary>
    /// Delete
    /// </summary>
    public bool Delete;

}
public class DataTable_StoryLoader
{
    public List<DataTable_Story> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Story> ItemsDict { get; private set; }

    public DataTable_StoryLoader(string path = "JSON/DataTable_Story")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Story>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Story> Items;
    }

    public DataTable_Story GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
