using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HunterInfo
{
    public int ID;
    public string Name;
    public float HP;
    public float Speed;
    public int EXP;
    public int Level;
}

public class HunterInstance
{
    int no;
    public HunterInfo hunterInfo { get; set; }
}

[System.Serializable]
public class HunterDataBase
{
    public List<HunterInfo> HunterData;
    public Dictionary<int, HunterInfo> hunterDic = new();


    public void Initialize()
    {
        foreach (HunterInfo hunter in HunterData)
        {
            hunterDic.Add(hunter.ID, hunter);
        }
    }

    public HunterInfo GetHunterByKey(int id)
    {
        if (hunterDic.ContainsKey(id))
            return hunterDic[id];

        return null;
    }

}
