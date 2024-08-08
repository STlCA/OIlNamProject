using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_UnitStep
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
    /// ATK
    /// </summary>
    public List<int> StepATK;

    /// <summary>
    /// Speed
    /// </summary>
    public List<int> StepSpeed;

    /// <summary>
    /// Sell
    /// </summary>
    public List<int> SellGold;

}
public class DataTable_UnitStepLoader
{
    public List<DataTable_UnitStep> ItemsList { get; private set; }
    public Dictionary<int, DataTable_UnitStep> ItemsDict { get; private set; }

    public DataTable_UnitStepLoader(string path = "JSON/DataTable_UnitStep")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_UnitStep>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_UnitStep> Items;
    }

    public DataTable_UnitStep GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
