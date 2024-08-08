using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Manager
{
    //public EnemyDataBase enemyDataBase;
    public DataTable_EnemyLoader dataTable_EnemyLoader;
    public DataTable_ChapterLoader dataTable_ChapterLoader;
    public DataTable_BossLoader dataTable_BossLoader;

    //¼öÁ¤
    public DataTable_MessageLoader dataTable_MessageLoader;
    public DataTable_StoryLoader dataTable_StoryLoader;

    //Unit
    public DataTable_UnitLoader dataTable_UnitLoader;
    public DataTable_UpgradeLoader dataTable_UpgradeLoader;


    private void Awake()
    {
        EnemyAwake();
        ChapterAwake();
        BossAwake();
        MessageAwake();
        StoryAwake();
        UnitAwake();
    }

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

    private void ChapterAwake()
    {
        dataTable_ChapterLoader = new DataTable_ChapterLoader();
    }
    
    private void BossAwake()
    {
        dataTable_BossLoader = new DataTable_BossLoader();
    }
    
    private void StoryAwake()
    {
        dataTable_StoryLoader = new DataTable_StoryLoader();
    }
    private void MessageAwake()
    {
        dataTable_MessageLoader = new DataTable_MessageLoader();
    }

    private void UnitAwake()
    {
        dataTable_UnitLoader = new DataTable_UnitLoader();
        dataTable_UpgradeLoader = new DataTable_UpgradeLoader();
    }
}
