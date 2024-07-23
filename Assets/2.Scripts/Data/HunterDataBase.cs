using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class HunterInfo
{
    public int ID;
    public string Name;
    public string Description;
    public float ATK;
    public float Attack_Speed;
    public float Attack_Range;
    public int Skill_ID;
    public string Unit_Modeling;

    public float Speed;

    public void Init()
    {
        Speed = Attack_Speed / 10000;
    }

/*    public string Path;
    public List<string> SpriteName;

    public List<Sprite> SpriteList;

    public void Init()
    {
        foreach (string path in SpriteName)
        {
            SpriteList.Add(Resources.Load<Sprite>(Path + path));
        }
    }*/
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
            hunter.Init();
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
