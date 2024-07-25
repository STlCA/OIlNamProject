using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Manager
{
    //public EnemyDataBase enemyDataBase;
    public DataTable_EnemyLoader dataTable_EnemyLoader;
    public StoryDataBase storyDataBase;
    public UnitDataBase unitDataBase;

    private void Awake()
    {
        EnemyAwake();
        StoryAwake();
        UnitAwake();
    }

    //public override void Init(GameManager gm)
    //{
    //    base.Init(gm);
    //
    //    EnemyAwake();
    //    StoryAwake();
    //    UnitAwake();
    //}

    private void EnemyAwake()
    {
        //TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Enemy_Data");
        //if (jsonFile != null)
        //{
        //    string json = jsonFile.text;

        //    enemyDataBase = JsonUtility.FromJson<EnemyDataBase>(json);
        //    enemyDataBase.Initialize();
        //}
        //else
        //{
        //    Debug.LogError("Failed to load enemyDataBase.json");
        //}
        dataTable_EnemyLoader = new DataTable_EnemyLoader();
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

    private void UnitAwake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Unit_Data");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            unitDataBase = JsonUtility.FromJson<UnitDataBase>(json);
            unitDataBase.Initialize();
        }
        else
        {
            Debug.LogError("Failed to load unitDataBase.json");
        }
    }
}
