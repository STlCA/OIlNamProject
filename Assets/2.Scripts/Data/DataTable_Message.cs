using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Message
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Message
    /// </summary>
    public string Message;

    /// <summary>
    /// Answer1
    /// </summary>
    public string Answer1;

    /// <summary>
    /// Price1
    /// </summary>
    public int Price1;

    /// <summary>
    /// Answer2
    /// </summary>
    public string Answer2;

    /// <summary>
    /// Price2
    /// </summary>
    public int Price2;

}
public class DataTable_MessageLoader
{
    public List<DataTable_Message> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Message> ItemsDict { get; private set; }

    public DataTable_MessageLoader(string path = "JSON/DataTable_Message")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Message>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Message> Items;
    }

    public DataTable_Message GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
