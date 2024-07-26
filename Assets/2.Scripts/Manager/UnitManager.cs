using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : Manager
{
    //도감, 강화

    [Header("Temp")]
    public DataManager DataManager;
    public UnitDataBase unitDataBase;

    public List<UnitInstance> canSpawnUnit = new();

    private void Start()
    {
        if (GameManager.Instance != null)
            unitDataBase = GameManager.Instance.DataManager.unitDataBase;
        else
        {
            unitDataBase = DataManager.unitDataBase;
        }

        //if 이전저장이 없다면
        UnitReset();
    }

    private void UnitReset()
    {
        foreach (var (id, unit) in unitDataBase.unitDic)
        {
            if (unit.Step == 1 && unit.Open == true)
            {
                UnitInstance newUnit = new();
                UnitInfo newInfo = new();
                newInfo = unitDataBase.GetUnitByKey(id);

                newUnit.Init(id, newInfo);

                canSpawnUnit.Add(newUnit);
            }
        }
    }

    public UnitInstance GetRandomUnit()
    {
        int index = Random.Range(0, canSpawnUnit.Count);

        UnitInstance newUnit = new();
        newUnit = canSpawnUnit[index];

        return newUnit;
    }
}
