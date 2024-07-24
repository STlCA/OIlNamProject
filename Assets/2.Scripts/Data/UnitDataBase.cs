using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class UnitInfo
{
    public int ID;
    public int Step;
    public string Type;
    public string Name;
    public string Description;
    public float ATK;
    public float Attack_Speed;
    public float Attack_Range;
    public int Skill_ID;
    public string Unit_Modeling;
    public bool Open; 

    public float Speed;
    public Sprite Sprite;

    public void Init()
    {
        Speed = Attack_Speed / 10000;
        Sprite = Resources.Load<Sprite>("Unit/"+Unit_Modeling);
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
    public int id;
    public UnitInfo unitInfo { get; set; }
    
    public void Init(int id, UnitInfo unit)
    {
        this.id = id;
        unitInfo = unit;
    }
}

[System.Serializable]
public class UnitDataBase
{
    public List<UnitInfo> UnitData;//이름 똑같게
    public Dictionary<int, UnitInfo> unitDic = new();


    public void Initialize()
    {
        foreach (UnitInfo unit in UnitData)
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
