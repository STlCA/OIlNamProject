using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Unit
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
    /// Unit_Tier
    /// </summary>
    public int Tier;

    /// <summary>
    /// Description
    /// </summary>
    public string Description;

    /// <summary>
    /// ATK
    /// </summary>
    public int ATK;

    /// <summary>
    /// Attack_Speed
    /// </summary>
    public int Speed;

    /// <summary>
    /// Attack_Range
    /// </summary>
    public int Range;

    /// <summary>
    /// Open
    /// </summary>
    public bool Open;

    /// <summary>
    /// Unit_Profile
    /// </summary>
    public string Profile;

    /// <summary>
    /// Unit_Modeling
    /// </summary>
    public string Sprite;

    /// <summary>
    /// UpgradeKey
    /// </summary>
    public int UpgradeKey;

    /// <summary>
    /// StepKey
    /// </summary>
    public int StepKey;

    /// <summary>
    /// UnitType
    /// </summary>
    public string UnitType;

    /// <summary>
    /// SoundIndex
    /// </summary>
    public int Sound;

}
public class DataTable_UnitLoader
{
    public List<DataTable_Unit> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Unit> ItemsDict { get; private set; }

    public DataTable_UnitLoader(string path = "JSON/DataTable_Unit")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Unit>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Unit> Items;
    }

    public DataTable_Unit GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
