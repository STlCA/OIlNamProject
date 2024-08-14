using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Goods
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Product_Name
    /// </summary>
    public string Name;

    /// <summary>
    /// Description
    /// </summary>
    public string Description;

    /// <summary>
    /// Money_Icon
    /// </summary>
    public string Path;

}
public class DataTable_GoodsLoader
{
    public List<DataTable_Goods> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Goods> ItemsDict { get; private set; }

    public DataTable_GoodsLoader(string path = "JSON/DataTable_Goods")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Goods>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Goods> Items;
    }

    public DataTable_Goods GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
