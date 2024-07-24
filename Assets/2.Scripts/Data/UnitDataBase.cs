using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class UnitInfo
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

public class UnitInstance
{
    int no;
    public UnitInfo unitInfo { get; set; }
}

[System.Serializable]
public class UnitDataBase
{
    public List<UnitInfo> unitData;
    public Dictionary<int, UnitInfo> unitDic = new();


    public void Initialize()
    {
        foreach (UnitInfo unit in unitData)
        {
            unit.Init();
            unitDic.Add(unit.ID, unit);
        }
    }

    public UnitInfo GetUnitByKey(int id)
    {
        if (unitDic.ContainsKey(id))
            return unitDic[id];

        return null;
    }
}
