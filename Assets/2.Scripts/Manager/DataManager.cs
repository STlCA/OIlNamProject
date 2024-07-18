using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public EnemyDataBase enemyDataBase;

    private void Awake()
    {
        EnemyAwake();
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
}
