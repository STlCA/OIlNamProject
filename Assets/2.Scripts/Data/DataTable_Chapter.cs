using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Chapter
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Group
    /// </summary>
    public int Group;

    /// <summary>
    /// Stage
    /// </summary>
    public int Stage;

    /// <summary>
    /// Wave
    /// </summary>
    public int Wave;

    /// <summary>
    /// Wave_Time
    /// </summary>
    public int Time;

    /// <summary>
    /// Wave_Gold
    /// </summary>
    public int Gold;

    /// <summary>
    /// Message
    /// </summary>
    public int Message;

    /// <summary>
    /// Spawn_Monster
    /// </summary>
    public int SpawnEnemy;

    /// <summary>
    /// Spawn_Count
    /// </summary>
    public int EnemyCount;

    /// <summary>
    /// Spawn_Hp
    /// </summary>
    public int HP;

}
public class DataTable_ChapterLoader
{
    public List<DataTable_Chapter> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Chapter> ItemsDict { get; private set; }

    public DataTable_ChapterLoader(string path = "JSON/DataTable_Chapter")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Chapter>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Chapter> Items;
    }

    public DataTable_Chapter GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
