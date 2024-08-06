using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Upgrade
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Unit ID
    /// </summary>
    public int UnitID;

    /// <summary>
    /// Enforce
    /// </summary>
    public List<int> ATK;

    /// <summary>
    /// 2
    /// </summary>
    public List<int> Speed;

    /// <summary>
    /// 3
    /// </summary>
    public List<int> Range;

    /// <summary>
    /// Enforce_Gold
    /// </summary>
    public List<int> UseGold;

    /// <summary>
    /// Drop
    /// </summary>
    public int Percent;

    /// <summary>
    /// NeedPiece
    /// </summary>
    public List<int> NeedPiece;

}
public class DataTable_UpgradeLoader
{
    public List<DataTable_Upgrade> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Upgrade> ItemsDict { get; private set; }

    public DataTable_UpgradeLoader(string path = "JSON/DataTable_Upgrade")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Upgrade>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Upgrade> Items;
    }

    public DataTable_Upgrade GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
