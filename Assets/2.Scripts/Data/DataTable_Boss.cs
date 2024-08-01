using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Boss
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Name
    /// </summary>
    public string Name;

    /// <summary>
    /// Movement_Speed
    /// </summary>
    public int Speed;

    /// <summary>
    /// Gain_Experience
    /// </summary>
    public int Exp;

    /// <summary>
    /// Acquisition_Play Goods
    /// </summary>
    public int PlayGoods;

    /// <summary>
    /// Boss_Modeling
    /// </summary>
    public string Path;

}
public class DataTable_BossLoader
{
    public List<DataTable_Boss> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Boss> ItemsDict { get; private set; }

    public DataTable_BossLoader(string path = "JSON/DataTable_Boss")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Boss>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Boss> Items;
    }

    public DataTable_Boss GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
