using UnityEngine;
using System.Collections.Generic;
using System.IO;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class StoryInfo
{
    public int ID;
    public string Text;
    public bool Delete;
}

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
        foreach (StoryInfo story in StoryData)
        {
            storyDic.Add(story.ID, story);
        }
    }

    public StoryInfo GetStoryByKey(int id)
    {
        if (storyDic.ContainsKey(id))
            return storyDic[id];

        return null;
    }
}
