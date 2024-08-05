using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Message
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Message
    /// </summary>
    public string Message;

    /// <summary>
    /// Info1_1
    /// </summary>
    public string Info1_1;

    /// <summary>
    /// Info1_2
    /// </summary>
    public string Info1_2;

    /// <summary>
    /// Answer1
    /// </summary>
    public string Answer1;

    /// <summary>
    /// Energy1
    /// </summary>
    public int Energy1;

    /// <summary>
    /// Price1
    /// </summary>
    public int Price1;

    /// <summary>
    /// Text
    /// </summary>
    public string Text1;

    /// <summary>
    /// Info2
    /// </summary>
    public string Info2;

    /// <summary>
    /// Answer2
    /// </summary>
    public string Answer2;

    /// <summary>
    /// Price2
    /// </summary>
    public int Price2;

    /// <summary>
    /// Energy2
    /// </summary>
    public int Energy2;

    /// <summary>
    /// Text
    /// </summary>
    public string Text2;

    /// <summary>
    /// Info3
    /// </summary>
    public string Info3;

    /// <summary>
    /// Answer3
    /// </summary>
    public string Answer3;

    /// <summary>
    /// Price3
    /// </summary>
    public int Price3;

    /// <summary>
    /// Energy3
    /// </summary>
    public int Energy3;

    /// <summary>
    /// Text
    /// </summary>
    public string Text3;

}
public class DataTable_MessageLoader
{
    public List<DataTable_Message> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Message> ItemsDict { get; private set; }

    public DataTable_MessageLoader(string path = "JSON/DataTable_Message")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Message>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Message> Items;
    }

    public DataTable_Message GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
