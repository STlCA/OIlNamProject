using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_RecruitTutorial
{
    /// <summary>
    /// index
    /// </summary>
    public int key;

    /// <summary>
    /// Talk
    /// </summary>
    public string Message;

}
public class DataTable_RecruitTutorialLoader
{
    public List<DataTable_RecruitTutorial> ItemsList { get; private set; }
    public Dictionary<int, DataTable_RecruitTutorial> ItemsDict { get; private set; }

    public DataTable_RecruitTutorialLoader(string path = "JSON/DataTable_RecruitTutorial")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_RecruitTutorial>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_RecruitTutorial> Items;
    }

    public DataTable_RecruitTutorial GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
