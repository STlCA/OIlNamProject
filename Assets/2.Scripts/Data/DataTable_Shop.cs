using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DataTable_Shop
{
    /// <summary>
    /// Index
    /// </summary>
    public int key;

    /// <summary>
    /// Item
    /// </summary>
    public string Item;

    /// <summary>
    /// Shop_Item_Name
    /// </summary>
    public string ItemName;

    /// <summary>
    /// Description
    /// </summary>
    public string Description;

    /// <summary>
    /// Value
    /// </summary>
    public string Value;

    /// <summary>
    /// Product_Value_1
    /// </summary>
    public int PID1;

    /// <summary>
    /// Product_Count_1
    /// </summary>
    public int PCount1;

    /// <summary>
    /// Product_Value_2
    /// </summary>
    public int PID2;

    /// <summary>
    /// Product_Count_2
    /// </summary>
    public int PCount2;

    /// <summary>
    /// Product_Value_3
    /// </summary>
    public int PID3;

    /// <summary>
    /// Product_Count_3
    /// </summary>
    public int PCount3;

    /// <summary>
    /// Is_Cash
    /// </summary>
    public bool isCash;

    /// <summary>
    /// Is_Ad
    /// </summary>
    public bool isAd;

    /// <summary>
    /// Money_Type
    /// </summary>
    public int MoneyType;

    /// <summary>
    /// Money_Count
    /// </summary>
    public int Cost;

    /// <summary>
    /// Availiable_Count
    /// </summary>
    public int AvailiableCount;

    /// <summary>
    /// Is_Once
    /// </summary>
    public bool isOnce;

    /// <summary>
    /// Availiable_Time 상품 재구매시 필요 기간 (단위 : day)
    /// </summary>
    public int AvailiableTime;

    /// <summary>
    /// Icon
    /// </summary>
    public string Path;

}
public class DataTable_ShopLoader
{
    public List<DataTable_Shop> ItemsList { get; private set; }
    public Dictionary<int, DataTable_Shop> ItemsDict { get; private set; }

    public DataTable_ShopLoader(string path = "JSON/DataTable_Shop")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DataTable_Shop>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DataTable_Shop> Items;
    }

    public DataTable_Shop GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
}
