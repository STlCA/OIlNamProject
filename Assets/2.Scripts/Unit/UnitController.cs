using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

[System.Serializable]
public class SpawnData
{
    public GameObject unitGo;
    public Unit unitData;
    public SpriteRenderer spriteRenderer;
    public Vector3 pos;
    public int id;
    public int upgradeStep;


    public void Init(GameObject go, UnitInstance unit, int upgrade = 1)
    {
        unitGo = go;
        pos = go.GetComponent<Transform>().position;
        id = unit.unitInfo.ID;
        upgradeStep = upgrade;

        spriteRenderer = go.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = unit.unitInfo.Sprite;
        unitData = go.GetComponentInChildren<Unit>();
        unitData.myData.Init(unit.id, unit.unitInfo.Range, unit.unitInfo.Range, unit.unitInfo.ATK);
    }
    public void UnitDataUpdate(UnitInstance unit)
    {
        unitData.myData.Init(unit.id, unit.unitInfo.Range, unit.unitInfo.Range, unit.unitInfo.ATK, 1);
    }
}

public class CanUpgrade
{
    public int count = 0;
    public List<Vector3> pos = new();

    public void Init(int count, Vector3 pos)
    {
        this.count = count;
        this.pos.Add(pos);
    }
    public void AddCount(int count, Vector3 pos)
    {
        this.count += count;
        this.pos.Add(pos);
    }
}

public struct PlusValue
{
    public int fixTempSpeed; // 대기중
    public int fixSpeed;//적용중

    public int tempSpeed; //대기중
    public int speed; //적용중

    public int lethalTempSpeed;// 대기중이지만 사실 지금은 필살기 즉발



    public int fixTempAtk; // 대기중
    public int fixAtk;//적용중

    public int tempAtk; //대기중
    public int atk; //적용중

    public int lethalTempAtk;// 대기중이지만 사실 지금은 필살기 즉발

    public int deleteSpeed;
    public int deleteAtk;
}

public class UnitController : MonoBehaviour
{
    //unit = 강화 도감
    //controller = 공격,호출,검사
    // spawn = 스폰

    [Header("GameScene")]
    public GameSceneManager gameSceneManager;

    [Header("Unit")]
    public UnitSpawn unitSpawnGo;
    public UnitManager unitManager;// 나중에 private
    public List<AnimatorController> animators = new List<AnimatorController>();

    [Header("Prefab")]
    public GameObject UnitPrefab;

    [Header("UI")]
    public GameObject infoGO;
    private TMP_Text infoTxt;

    public Dictionary<Vector3, SpawnData> spawnData = new();
    public Dictionary<int, CanUpgrade> canUpgrade = new();
    public Dictionary<int, AnimatorController> unitAnimator = new();

    public List<GameObject> onUnitPopUP = new();

    public PlusValue plusValue = new();


    private void Start()
    {
        if (GameManager.Instance != null)
        {
            unitManager = GameManager.Instance.UnitManager;
            GameManager.Instance.UnitController = this;
        }

        for (int i = 0; i < animators.Count; i++)
        {
            int temp = 201;

            unitAnimator.Add(temp + (i * 100), animators[i]);
        }

        infoTxt = infoGO.GetComponentInChildren<TMP_Text>();
    }

    public void UnitRecall()
    {
        if (!gameSceneManager.CanUseGold())
            return;

        GameObject unitGo = Instantiate(UnitPrefab);
        CanSpawn canSpawn = unitSpawnGo.RandomUnitSpawn();

        if (canSpawn.canSpawn == true)
        {
            unitGo.transform.position = canSpawn.pos;

            gameSceneManager.ChangeGold(-gameSceneManager.ChangeUseGold());


        }
        else
        {
            //TODO : 동료를 더이상 소환할수없습니다 팝업 혹은 글귀 올라가면서 투명해지는

            Debug.Log("동료를 더이상 소환할수없습니다.");

            Destroy(unitGo);
            return;
        }

        UnitInstance newUnit = new();
        newUnit = unitManager.GetRandomUnit();



        SpawnData newData = new SpawnData();
        newData.Init(unitGo, newUnit);

        //임시
        unitGo.GetComponentInChildren<Unit>().controller = this;
        unitGo.GetComponentInChildren<UnitAnimation>().unitAnimator.runtimeAnimatorController = unitAnimator[newUnit.id];

        infoTxt.text = newUnit.unitInfo.Name + " 동료를 획득하였습니다.";

        spawnData.Add(canSpawn.pos, newData);

        if (canUpgrade.ContainsKey(newUnit.id) == true)
        {
            canUpgrade[newUnit.id].count++;
            canUpgrade[newUnit.id].pos.Add(canSpawn.pos);
        }
        else
        {
            CanUpgrade temp = new CanUpgrade();
            temp.Init(1, canSpawn.pos);

            canUpgrade.Add(newUnit.id, temp);
        }
    }

    public bool CanUpgradeCheck(int id)
    {
        if (canUpgrade[id].count >= 3)
            return true;
        else
            return false;
    }

    public void UnitUpgrade(int id, Vector3 pos)
    {
        if (CanUpgradeCheck(id) == false)
            return;

        canUpgrade[id].count -= 3;
        canUpgrade[id].pos.Remove(pos);

        int count = 0;

        Vector3[] tempPos = new Vector3[2];

        foreach (var val in canUpgrade[id].pos)
        {
            if (val != pos && count < 2)
            {
                unitSpawnGo.PlusSpawnPoint(spawnData[val].pos);

                Destroy(spawnData[val].unitGo);
                spawnData.Remove(val);

                tempPos[count] = val;
                count++;
            }
        }

        canUpgrade[id].pos.Remove(tempPos[0]);
        canUpgrade[id].pos.Remove(tempPos[1]);

        UnitInstance newUnit = new();
        newUnit.id = id + 1;
        newUnit.unitInfo = unitManager.unitDataBase.GetUnitByKey(id + 1);
        spawnData[pos].UnitDataUpdate(newUnit);
        spawnData[pos].upgradeStep++;
        spawnData[pos].id++;
        //spawnData[pos].spriteRenderer.sprite = unitDataBase.GetUnitByKey(id + 1).Sprite;----------지금스프라이트준비안댐
        //아마 atk speed도 필요할듯


        //얕은복사로 인해 실패.
        /*        spawnData[pos].unitInstance.unitInfo = null;
                spawnData[pos].unitInstance.unitInfo = new();
                UnitInfo info = new();
                info = unitDataBase.GetUnitByKey(id + 1);
                spawnData[pos].unitInstance.unitInfo = info;*/



        if (canUpgrade.ContainsKey(id + 1) == true)
        {
            canUpgrade[id + 1].AddCount(1, pos);
        }
        else
        {
            CanUpgrade temp = new CanUpgrade();
            temp.Init(1, pos);

            canUpgrade.Add(id + 1, temp);
        }
    }

    public void PlusSpeed(int changeVal, PlusChangeType type, bool nowChange = false)//딸 팝업 선택했을때, 필살기 사용했을때
    {
        switch (type)
        {
            case PlusChangeType.FixChange:
                plusValue.fixTempSpeed += changeVal;
                if (nowChange)
                {
                    SpeedChange(type, plusValue.fixTempSpeed + plusValue.fixSpeed);
                    plusValue.fixSpeed += plusValue.fixTempSpeed;
                    plusValue.fixTempSpeed = 0;
                }
                break;
            case PlusChangeType.NormalChange:
                plusValue.tempSpeed += changeVal;
                plusValue.deleteSpeed += changeVal;
                if (nowChange)
                {
                    SpeedChange(type, plusValue.speed + plusValue.tempSpeed);
                    plusValue.speed += plusValue.tempSpeed;
                    plusValue.tempSpeed = 0;
                }
                break;
            case PlusChangeType.LethalChange:
                plusValue.lethalTempSpeed += changeVal;
                if (nowChange)
                {
                    SpeedChange(type, plusValue.speed + plusValue.lethalTempSpeed);
                    plusValue.speed += plusValue.lethalTempSpeed;
                    plusValue.lethalTempSpeed = 0;
                }
                break;
            case PlusChangeType.DeleteChange:
                if (plusValue.deleteSpeed != 0)
                {
                    plusValue.speed -= plusValue.deleteSpeed;
                    SpeedChange(type, plusValue.speed);
                    plusValue.deleteSpeed = 0;
                }
                break;
        }
    }

    private void SpeedChange(PlusChangeType type, int percent)
    {
        switch (type)
        {
            case PlusChangeType.FixChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.SpeedChange(percent, true);
                    val.Value.unitData.myData.SpeedChange(plusValue.speed);
                }
                break;
            case PlusChangeType.NormalChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.SpeedChange(percent);
                }
                break;
            case PlusChangeType.LethalChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.SpeedChange(percent);
                }
                break;
            case PlusChangeType.DeleteChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.SpeedChange(percent);
                }
                break;
        }
    }

    public void PlusATK(int changeVal, PlusChangeType type, bool nowChange = false)//딸 팝업 선택했을때, 필살기 사용했을때
    {
        switch (type)
        {
            case PlusChangeType.FixChange:
                plusValue.fixTempAtk += changeVal;
                if (nowChange)
                {
                    ATKChange(type, plusValue.fixTempAtk + plusValue.fixAtk);
                    plusValue.fixAtk += plusValue.fixTempAtk;
                    plusValue.fixTempAtk = 0;
                }
                break;
            case PlusChangeType.NormalChange:
                plusValue.deleteAtk += changeVal;
                plusValue.tempAtk += changeVal;
                if (nowChange)
                {
                    ATKChange(type, plusValue.atk + plusValue.tempAtk);
                    plusValue.atk += plusValue.tempAtk;
                    plusValue.tempAtk = 0;
                }
                break;
            case PlusChangeType.LethalChange:
                plusValue.lethalTempAtk += changeVal;
                if (nowChange)
                {
                    ATKChange(type, plusValue.atk + plusValue.lethalTempAtk);
                    plusValue.atk += plusValue.lethalTempAtk;
                    plusValue.lethalTempAtk = 0;
                }
                break;
            case PlusChangeType.DeleteChange:
                if (plusValue.deleteAtk != 0)
                {
                    plusValue.atk -= plusValue.deleteAtk;
                    SpeedChange(type, plusValue.atk);
                    plusValue.deleteAtk = 0;
                }
                break;
        }
    }

    private void ATKChange(PlusChangeType type, int percent)
    {
        switch (type)
        {
            case PlusChangeType.FixChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.ATKChange(percent, true);
                    val.Value.unitData.myData.ATKChange(plusValue.atk);
                }
                break;
            case PlusChangeType.NormalChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.ATKChange(percent);
                }
                break;
            case PlusChangeType.LethalChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.ATKChange(percent);
                }
                break;
            case PlusChangeType.DeleteChange:
                foreach (var val in spawnData)
                {
                    val.Value.unitData.myData.ATKChange(percent);
                }
                break;

        }
    }
}
