using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    //unit = 강화 도감
    //controller = 공격
    // spawn = 스폰

    [Header("Unit")]
    public UnitSpawn unitSpawnGo;
    private List<int> spawnUnit = new();

    [Header("Prefab")]
    public GameObject UnitPrefab;

    [Header("Temp")]
    public DataManager DataManager;
    private UnitDataBase unitDataBase;

    private void Start()
    {
        if (GameManager.Instance != null)
            unitDataBase = GameManager.Instance.DataManager.unitDataBase;
        else
            unitDataBase = DataManager.unitDataBase;
    }

    public void UnitRecall()
    {
        //기존에 있으면
        

        //없으면
        GameObject unit = Instantiate(UnitPrefab);
        unit.transform.position = unitSpawnGo.RandomUnitSpawn();
        //spawnUnit.add
    }
}
