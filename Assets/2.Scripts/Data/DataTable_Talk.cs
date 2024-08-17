using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Talk
{
    /// <summary>
    /// index
    /// </summary>
    public int key;

    /// <summary>
    /// Talk
    /// </summary>
    public string Message;

}
public class DataTable_TalkLoader
{
    public List<DataTable_Talk> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Talk> ItemsDict { get; private set; }

    public DataTable_TalkLoader(string path = "JSON/DataTable_Talk")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Talk>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Talk> Items;
    }

    public DataTable_Talk GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
