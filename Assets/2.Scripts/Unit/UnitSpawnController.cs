using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawnController : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    private UnitManager unitManager;
    private DataTable_UnitStepLoader unitStepLoader;
    
    [Header("SpawnPoint")]
    public GameObject spawnGO;
    private Transform[] spawnArray;
    private List<Vector3> canSpawnPoints;

    [Header("UI")]
    public GameObject spawnFalse;
    public GameObject rubyFalse;
    public TMP_Text infoTxt;
    public GameObject spriteCanvas;

    public Dictionary<Vector3, UnitGameData> spawnData = new();
    //public List<GameObject> onUnitPopUP = new();

    private float[] percents;
    private float totalPercent;

    public void Init(GameObject spawn)
    {
        unitManager = GameManager.Instance.UnitManager;
        unitStepLoader = GameManager.Instance.DataManager.dataTable_UnitStepLoader;

        spawnGO = spawn;

        PointsInit();
        PercentInit();
    }

    // Transform Vector3로 변환
    private void PointsInit()
    {
        spawnArray = spawnGO.GetComponentsInChildren<Transform>();
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
        if (!gameSceneManager.CanUseRuby())//루비모자라면
        {
            rubyFalse.SetActive(true);
            return;
        }
        if (canSpawnPoints.Count <= 0)//스폰할자리가없으면
        {
            spawnFalse.SetActive(true);
            return;
        }

        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Recall);
        gameSceneManager.ChangeRuby(-gameSceneManager.ChangeUseRuby());

        Vector3 point = RandomSpawnPoint();

        UnitData newUnit = unitManager.GetRandomUnit(RandomTier());

        GameObject go = Instantiate(newUnit.prefabs);
        go.transform.position = point;

        UnitGameData unitGameData = go.GetComponentInChildren<UnitGameData>();
        unitGameData.Init(newUnit, unitStepLoader.GetByKey(newUnit.stepKey), this);

        spawnData.Add(point, unitGameData);

        infoTxt.text = newUnit.name + " 영웅을 획득하였습니다.";
    }
}
