using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_TIP
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// tip
    /// </summary>
    public string Tip;

}
public class DataTable_TIPLoader
{
    public List<DataTable_TIP> ItemsList { get; private set; }
    public Dictionary<int, DataTable_TIP> ItemsDict { get; private set; }

    public DataTable_TIPLoader(string path = "JSON/DataTable_TIP")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_TIP>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_TIP> Items;
    }

    public DataTable_TIP GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
