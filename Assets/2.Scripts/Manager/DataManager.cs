using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Manager
{
    public EnemyDataBase enemyDataBase;
    public StoryDataBase storyDataBase;

    private void Awake()
    {
        EnemyAwake();
        StoryAwake();
    }

    private void EnemyAwake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Enemy_Data");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            enemyDataBase = JsonUtility.FromJson<EnemyDataBase>(json);
            enemyDataBase.Initialize();
        }
        else
        {
            Debug.LogError("Failed to load enemyDataBase.json");
        }

    }
    
    private void StoryAwake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Story_Data");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            storyDataBase = JsonUtility.FromJson<StoryDataBase>(json);
            storyDataBase.Initialize();
        }
        else
        {
            Debug.LogError("Failed to load storyDataBase.json");
        }
    }
}
