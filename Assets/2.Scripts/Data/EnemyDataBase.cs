using UnityEngine;
using System.Collections.Generic;
using System.IO;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[System.Serializable]
public class EnemyInfo
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
public class EnemyInstance
{
    int no;
    public EnemyInfo EnemyInfo { get; set; }
}

[System.Serializable]
public class EnemyDataBase : MonoBehaviour
{
    public List<EnemyInfo> EnemyData;
    public Dictionary<int, EnemyInfo> enemyDic = new();

    public void Initialize()
    {
        foreach (EnemyInfo enemy in EnemyData)
        {
            enemyDic.Add(enemy.ID, enemy);
        }
    }

    public EnemyInfo GetEnemyByKey(int id)
    {
        if (enemyDic.ContainsKey(id))
            return enemyDic[id];

        return null;
    }
}