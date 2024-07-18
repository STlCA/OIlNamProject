using UnityEngine;
using System.Collections.Generic;
using System.IO;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class StoryInfo
{
    public int ID;
    public string Name;
    public float HP;
    public float Speed;
    public int Gold;
    public int EXP;
    public int Level;
}

// 마물에 대한 정보를 가져온다
public class StoryInstance
{
    int no;
    public StoryInfo StoryInfo { get; set; }
}

[System.Serializable]
public class StoryDataBase
{
    public List<StoryInfo> StoryData;
    public Dictionary<int, StoryInfo> storyDic = new();

    public void Initialize()
    {
        foreach (StoryInfo enemy in StoryData)
        {
            storyDic.Add(enemy.ID, enemy);
        }
    }

    public StoryInfo GetEnemyByKey(int id)
    {
        if (storyDic.ContainsKey(id))
            return storyDic[id];

        return null;
    }
}
