using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Enemy
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
    /// Description
    /// </summary>
    public string Description;

    /// <summary>
    /// HP
    /// </summary>
    public int HP;

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
    /// Monster_Modeling
    /// </summary>
    public string Path;

    public Sprite sprite;

    public void Init()
    {
        sprite = Resources.Load<Sprite>(Path);
    }
}
public class DataTable_EnemyLoader
{
    public List<DataTable_Enemy> ItemsList;
    public Dictionary<int, DataTable_Enemy> ItemsDict = new();

    public DataTable_EnemyLoader(string path = "JSON/DataTable_Enemy")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Enemy>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Enemy> Items;
    }

    public DataTable_Enemy GetEnemyByKey(int id)
    {
        if (ItemsDict.ContainsKey(id))
            return ItemsDict[id];

        return null;
    }
}
