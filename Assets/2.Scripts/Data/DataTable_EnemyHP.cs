using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_EnemyHP
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Wave
    /// </summary>
    public int Wave;

    /// <summary>
    /// HP
    /// </summary>
    public int HP;

}
public class DataTable_EnemyHPLoader
{
    public List<DataTable_EnemyHP> ItemsList { get; private set; }
    public Dictionary<int, DataTable_EnemyHP> ItemsDict { get; private set; }

    public DataTable_EnemyHPLoader(string path = "JSON/DataTable_EnemyHP")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_EnemyHP>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_EnemyHP> Items;
    }

    public DataTable_EnemyHP GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
