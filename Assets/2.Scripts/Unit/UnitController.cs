using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

[System.Serializable]
public class SpawnData
{
    public GameObject unitGo;
    public Transform transform;
    public UnitInstance unitInstance;
    public int upgradeStep;

    public void Init(GameObject go, UnitInstance unit)
    {
        unitGo = go;
        transform = go.GetComponent<Transform>();
        unitInstance = unit;
        upgradeStep = 1;

        go.GetComponentInChildren<SpriteRenderer>().sprite = unitInstance.unitInfo.Sprite;
        go.GetComponentInChildren<Unit>().id = unit.id;
    }
}

public class UnitController : MonoBehaviour
{
    //unit = 강화 도감
    //controller = 공격,호출,검사
    // spawn = 스폰

    [Header("Unit")]
    public UnitSpawn unitSpawnGo;
    public UnitManager unitManager;// 나중에 private

    [Header("Prefab")]
    public GameObject UnitPrefab;

    [Header("Temp")]
    public DataManager DataManager;
    private UnitDataBase unitDataBase;

    [Header("UI")]
    public TMP_Text infoTxt;

    public Dictionary<Vector3,SpawnData> spawnData = new();
    public Dictionary<int, int> canUpgrade = new();

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            unitDataBase = GameManager.Instance.DataManager.unitDataBase;
            unitManager = GameManager.Instance.UnitManager;
            GameManager.Instance.UnitController = this;
        }
        else
        {
            unitDataBase = DataManager.unitDataBase;
        }
    }

    public void UnitRecall()
    {
        GameObject unitGo = Instantiate(UnitPrefab);
        CanSpawn canSpawn = unitSpawnGo.RandomUnitSpawn();

        if (canSpawn.canSpawn == true)
        {
            unitGo.transform.position = canSpawn.pos;
        }
        else
        {
            //TODO : 동료를 더이상 소환할수없습니다 팝업 혹은 글귀 올라가면서 투명해지는

            Debug.Log("동료를 더이상 소환할수없습니다.");

            Destroy(unitGo);
            return;
        }

        UnitInstance newUnit = unitManager.GetRandomUnit();

        SpawnData newData = new SpawnData();
        newData.Init(unitGo, newUnit);
        
        //임시
        unitGo.GetComponentInChildren<Unit>().controller = this;

        infoTxt.text = newUnit.unitInfo.Name + " 동료를 획득하였습니다.";

        spawnData.Add(canSpawn.pos,newData);

        if (canUpgrade.ContainsKey(newUnit.id) == true)
            canUpgrade[newUnit.id]++;
        else
            canUpgrade.Add(newUnit.id, 1);
    }

    public bool CanUpgradeCheck(int id)
    {
        if (canUpgrade[id] >= 3)
            return true;
        else
            return false;
    }

    public void UnitUpgrade()
    {

    }
}
