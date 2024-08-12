using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_WaveReward
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Chapter
    /// </summary>
    public int Chapter;

    /// <summary>
    /// Wave
    /// </summary>
    public int Wave;

    /// <summary>
    /// Gold
    /// </summary>
    public int Gold;

    /// <summary>
    /// Level_Experience
    /// </summary>
    public int Exp;

    /// <summary>
    /// Enforcebook
    /// </summary>
    public int Enforcebook;

}
public class DataTable_WaveRewardLoader
{
    public List<DataTable_WaveReward> ItemsList { get; private set; }
    public Dictionary<int, DataTable_WaveReward> ItemsDict { get; private set; }

    public DataTable_WaveRewardLoader(string path = "JSON/DataTable_WaveReward")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_WaveReward>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_WaveReward> Items;
    }

    public DataTable_WaveReward GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
