using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawnController : MonoBehaviour
{
    private UnitManager unitManager;
    private DataTable_UnitStepLoader unitStepLoader;
    
    [Header("SpawnPoint")]
    public GameObject spawnGO;
    private Transform[] spawnArray;
    private List<Vector3> canSpawnPoints;

    public Dictionary<Vector3, UnitGameData> spawnData;
    public List<GameObject> onUnitPopUP = new();

    private float[] percents;
    private float totalPercent;


    private void Start()
    {
        unitManager = GameManager.Instance.UnitManager;
        unitStepLoader = GameManager.Instance.DataManager.dataTable_UnitStepLoader;

        PointsInit();
    }

    // Transform Vector3로 변환
    private void PointsInit()
    {
        spawnArray = GetComponentsInChildren<Transform>();
        canSpawnPoints = new List<Vector3>();

        foreach (Transform t in spawnArray)
        {
            canSpawnPoints.Add(t.position);
        }

        canSpawnPoints.Remove(canSpawnPoints[0]);
    }

    private void PercentInit()
    {
        percents = new float[3];

        //S,A,B
        percents[0] = 9.8f;
        percents[1] = 24.85f;
        percents[2] = 65.35f;

        TotalPercent();        
    }

    private void TotalPercent()
    {
        foreach( var t in percents )
        {
            totalPercent += t;
        }
    }

    public int RandomTier()
    {
        float random = Random.value * totalPercent;

        for( int i = 0; i < percents.Length; i++ )
        {
            random -= percents[i];

            if (random <= 0)
                return i + 1;//index 0 = Stier(Excel = 1)
        }

        return 0;
    }

    public void PercentChange(float s,float a, float b)
    {
        percents[0] += s;
        percents[1] += a;
        percents[2] += b;

        TotalPercent();
    }

    public Vector3 RandomSpawnPoint()
    {
        int index = Random.Range(0, canSpawnPoints.Count);

        Vector3 returnData = canSpawnPoints[index];
        canSpawnPoints.Remove(canSpawnPoints[index]);

        return returnData;
    }

    public void UnitRecall()
    {
        //spawnData채우기 키 Vector3, 값 UnitData

        Vector3 point = RandomSpawnPoint();

        UnitData newUnit = unitManager.GetRandomUnit(RandomTier());
        UnitGameData newGameData = new();
        newGameData.Init(newUnit, unitStepLoader.GetByKey(newUnit.stepKey),this);//강화된 공격력받아오기 unitgamedata에는 버프류 들어갈것

        spawnData.Add(point, newGameData);
    }

    

}
