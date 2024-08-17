using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Constants;
using TMPro;
using UnityEditor;

[Serializable]
public struct Save_UnitData
{
    public List<UnitSaveData> unitSaveDatas;
    public PieceSaveData pieceData;
}
[Serializable]
public struct PieceSaveData
{
    public int unitPiece;

    public int sPiece;
    public int aPiece;
    public int bPiece;
}

[Serializable]
public class PieceData
{
    public int unitPiece = 0;

    public int sPiece = 0;
    public int aPiece = 0;
    public int bPiece = 0;

    public void UsePiece(PieceType type, int value)
    {
        switch (type)
        {
            case PieceType.Unit:
                unitPiece += value; break;
            case PieceType.STier:
                sPiece += value; break;
            case PieceType.ATier:
                aPiece += value; break;
            case PieceType.BTier:
                bPiece += value; break;
        }
    }

    public void Save(ref PieceSaveData data)
    {
        data.unitPiece = unitPiece;
        data.sPiece = sPiece;
        data.aPiece = aPiece;
        data.bPiece = bPiece;
    }

    public void Load(PieceSaveData data)
    {
        unitPiece = data.unitPiece;
        sPiece = data.sPiece;
        aPiece = data.aPiece;
        bPiece = data.bPiece;
    }
}

[Serializable]
public struct UnitSaveData
{
    public int Key;
    public int Level;
    public int Piece;
    public bool Open;
}

[Serializable]
public class UnitData
{
    //기본정보
    public int key = -1;
    public int upgradeKey;
    public int stepKey;
    public string name;
    public int tier;
    public float atk;
    public float speed;
    public int range;
    public bool open;
    public string type;
    public int sound;

    //강화용정보
    public int level;
    public int piece;

    //Sprite
    public Sprite profile;
    public GameObject prefabs;

    //업그레이드 정보
    private DataTable_Upgrade upgradeData;

    public void Init(DataTable_Unit unit, DataTable_Upgrade upgradeData)
    {
        this.upgradeData = upgradeData;

        if (key == -1)//load데이터가 없다는 뜻
        {
            open = unit.Open;

            level = 0;
            piece = 0;
        }

        key = unit.key;
        upgradeKey = unit.UpgradeKey;
        stepKey = unit.StepKey;
        name = unit.Name;
        tier = unit.Tier;
        atk = unit.ATK;
        speed = unit.Speed / 10000f;
        range = unit.Range;
        type = unit.UnitType;
        sound = unit.Sound;

        if (level > 0)
            SetStatus();

        profile = Resources.Load<Sprite>("Unit/Profile/" + unit.Profile);
        prefabs = Resources.Load<GameObject>("Unit/Prefabs/" + unit.Sprite);
    }

    public void Load(UnitSaveData data)
    {
        key = data.Key;
        level = data.Level;
        piece = data.Piece;
        open = data.Open;
    }

    private void SetStatus()
    {
        for (int i = 0; i < level; i++)
        {
            atk += upgradeData.ATK[i];
            speed += upgradeData.Speed[i];
            range += upgradeData.Range[i];
        }
    }

    public void Save(ref UnitSaveData data)
    {
        data.Key = key;
        data.Level = level;
        data.Piece = piece;
        data.Open = open;
    }

    //필요한 업그레이드 조각 개수 리턴 --> 나중엔 필요없을지도
    public int Upgrade()
    {
        //나중에 캐릭별 조각으로 바뀌면 필요
        //piece -= upgradeData.NeedPiece[level];
        atk += upgradeData.ATK[level];
        speed += upgradeData.Speed[level];
        range += upgradeData.Range[level];
        level++;

        return upgradeData.NeedPiece[level - 1];
    }
}

public class UnitManager : Manager
{
    [Header("Temp")]//Data
    public DataManager DataManager;
    private DataTable_UnitLoader unitLoader; // 가지고만옴
    private DataTable_UpgradeLoader upgradeLoader; //가지고만옴

    [Header("UI")]
    public TMP_Text tabPieceTxt;
    public TMP_Text gachaTabPieceTxt;
    public GameObject falseGacha;
    public GameObject resultUI;
    public TMP_Text resultPieceTxt;
    public List<TMP_Text> resultTierPiece = new();
    public List<TMP_Text> unitTabTierPiece = new();
    public List<GameObject> tierAnim = new();
    public GameObject gachaAnim;

    //[HideInInspector]
    public PieceData pieceData = new();
    private int tempSPiece;
    private int tempAPiece;
    private int tempBPiece;

    //public List<UnitData> unitDataBase = new();
    public Dictionary<int, UnitData> unitDataDic = new();

    //CanSpawnUnit //id만 들고있음
    private List<int> sTierUnitID = new();
    private List<int> aTierUnitID = new();
    private List<int> bTierUnitID = new();

    private float[] percents;

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        unitLoader = GameManager.Instance.DataManager.dataTable_UnitLoader;
        upgradeLoader = GameManager.Instance.DataManager.dataTable_UpgradeLoader;
    }

    private void Start()
    {
        if (GameManager.Instance == null || unitLoader == null)
        {
            unitLoader = DataManager.dataTable_UnitLoader;
            upgradeLoader = DataManager.dataTable_UpgradeLoader;
        }

        UnitInit();
        InitTierID();
        PercentInit();
    }

    public void SetUIText(TMP_Text tabTxt, TMP_Text gachaTabTxt, GameObject falseGacha,
        GameObject resultUI, TMP_Text resultPieceTxt, List<TMP_Text> resultTierPiece,
        List<GameObject> tierAnim, GameObject gachaAnim, List<TMP_Text> unitTabTierPiece)
    {
        tabPieceTxt = tabTxt;
        gachaTabPieceTxt = gachaTabTxt;
        this.falseGacha = falseGacha;
        this.resultUI = resultUI;
        this.resultPieceTxt = resultPieceTxt;
        this.resultTierPiece = resultTierPiece;
        this.tierAnim = tierAnim;
        this.gachaAnim = gachaAnim;
        this.unitTabTierPiece = unitTabTierPiece;

        ChangeAllUnitPiece();
    }

    private void UnitInit()
    {
        foreach (var item in unitLoader.ItemsList)
        {
            if (unitDataDic.Count != 0)//유닛데이터가 0개가 아니면
            {
                if (unitDataDic.ContainsKey(item.key))//키가있는키면
                {
                    unitDataDic[item.key].Init(item, upgradeLoader.GetByKey(item.UpgradeKey));
                }
                else
                {
                    UnitData newData = new();
                    newData.Init(item, upgradeLoader.GetByKey(item.UpgradeKey));

                    unitDataDic.Add(newData.key, newData);
                }
            }
            else // 유닛데이터가 0개라면
            {
                UnitData newData = new();
                newData.Init(item, upgradeLoader.GetByKey(item.UpgradeKey));

                unitDataDic.Add(newData.key, newData);
            }
        }
    }

    private void PercentInit()
    {
        percents = new float[3];
        percents[0] = 3f;
        percents[1] = 25f;
        percents[2] = 72f;
    }

    private void InitTierID()
    {
        foreach (var (key, item) in unitDataDic)
        {
            if (!item.open)
                return;

            switch (item.tier)
            {
                case 1:
                    sTierUnitID.Add(key);
                    break;
                case 2:
                    aTierUnitID.Add(key);
                    break;
                case 3:
                    bTierUnitID.Add(key);
                    break;
            }
        }
    }

    private void AddTierID(int key, int tier)
    {
        switch (tier)
        {
            case 1:
                sTierUnitID.Add(key);
                break;
            case 2:
                aTierUnitID.Add(key);
                break;
            case 3:
                bTierUnitID.Add(key);
                break;
        }
    }

    public UnitData GetByKey(int key)
    {
        return unitDataDic[key];
    }

    // 영웅 뽑기할때
    // 티어별 모집권뽑기
    public void GachaUnitPiece(int count)
    {
        if (count * 30 > pieceData.unitPiece)
        {
            falseGacha.SetActive(true);
            return;
        }

        if (resultUI.activeSelf)
            resultUI.SetActive(false);

        float total = 0;
        int index = 0;
        float random;

        foreach (var t in percents)
        {
            total += t;
        }

        for (int i = 0; i < count * 3; i++)
        {
            random = Random.value * total;

            for (int j = 0; j < percents.Length; ++j)
            {
                random -= percents[j];

                if (random <= 0)
                {
                    index = j + 1;
                    break;
                }
            }

            //0번은 유닛모집권

            switch (index)
            {
                case 0:
                    return;
                case 1:
                    tempSPiece++; break;
                case 2:
                    tempAPiece++; break;
                case 3:
                    tempBPiece++; break;
                default:
                    Debug.Log("암것도 안뽑아짐");
                    return;
            }

            pieceData.UsePiece((PieceType)index, 1);
        }

        pieceData.UsePiece(PieceType.Unit, -30 * count);

        gachaAnim.SetActive(true);

        foreach (var t in tierAnim)
        {
            if (t.activeSelf)
                t.SetActive(false);
        }

        if (tempSPiece >= 1)
            tierAnim[0].SetActive(true);
        else if (tempAPiece >= 1)
            tierAnim[1].SetActive(true);
        else if (tempBPiece >= 1)
            tierAnim[2].SetActive(true);


        ChangeAllUnitPiece();
    }

    //캐릭별 조각으로 짜놔서 필요
    public void ChangeAllUnitPiece()
    {
        foreach (var (key, unit) in unitDataDic)
        {
            switch (unit.tier)
            {
                case (int)UnitTier.STier:
                    unit.piece = pieceData.sPiece; break;
                case (int)UnitTier.ATier:
                    unit.piece = pieceData.aPiece; break;
                case (int)UnitTier.BTier:
                    unit.piece = pieceData.bPiece; break;
            }
        }

        tabPieceTxt.text = pieceData.unitPiece.ToString() + " / 30";

        if (pieceData.unitPiece >= 30)
            tabPieceTxt.color = Color.cyan;
        else
            tabPieceTxt.color = Color.gray;

        gachaTabPieceTxt.text = pieceData.unitPiece.ToString();
        resultPieceTxt.text = pieceData.unitPiece.ToString();
    }

    //치트키BTN
    public void ChageAllTierPiece(int val)
    {
        pieceData.UsePiece(PieceType.STier, val);
        pieceData.UsePiece(PieceType.ATier, val);
        pieceData.UsePiece(PieceType.BTier, val);

        ChangeAllUnitPiece();
        UnitPieceTextUpdate();
    }

    //영웅조각 사용 세트 묶음
    public void ChangePiece(PieceType type, int val)
    {
        pieceData.UsePiece(type, val);
        ChangeAllUnitPiece();
        UnitPieceTextUpdate();

        if (val < 0)
            SaveSystem.Save();
    }

    //영웅 뽑기할때
    //영웅을 직접뽑기
    public void GachaNewUnit(int count)
    {
        int keyCount = 0;

        Dictionary<int, int> newUnit = new();

        for (int i = 0; i < count; i++)
        {
            UnitData newData = RandomNewUnit();

            if (newUnit.ContainsKey(newData.key))
                newUnit[newData.key] += 1;
            else
            {
                newUnit.Add(newData.key, 1);
                keyCount++;//슬롯몇개놓을지 기억용
            }
        }

        foreach (var (key, num) in newUnit)
        {
            if (!unitDataDic[key].open)
            {
                unitDataDic[key].open = true;
                unitDataDic[key].piece += (num - 1);
                AddTierID(key, unitDataDic[key].tier);
            }
            else
                unitDataDic[key].piece += num;
        }

        //TODO :: Save하기
    }

    private UnitData RandomNewUnit()
    {
        float total = 0;

        foreach (var item in upgradeLoader.ItemsList)
        {
            total += (item.Percent / 100f);
        }

        float random = Random.value * total;

        foreach (var item in upgradeLoader.ItemsList)
        {
            random -= item.Percent;

            if (random <= 0)
            {
                return unitDataDic[item.key];
            }
        }

        Debug.Log("아무유닛도 뽑아지지않음");
        return null;
    }

    //인게임 유닛 뽑기
    //티어랜덤으로 정하면 그 티어중 랜덤으로 유닛 뽑아짐
    public UnitData GetRandomUnit(int tier)
    {
        int index;
        UnitData unit = new();

        switch (tier)
        {
            case 1:
                index = Random.Range(0, sTierUnitID.Count);
                unit = unitDataDic[sTierUnitID[index]];
                break;
            case 2:
                index = Random.Range(0, aTierUnitID.Count);
                unit = unitDataDic[aTierUnitID[index]];
                break;
            case 3:
                index = Random.Range(0, bTierUnitID.Count);
                unit = unitDataDic[bTierUnitID[index]];
                break;
            default:
                Debug.Log("유닛가져오기 실패");
                return null;
        }

        return unit;
    }

    public void ResultSetting(GameObject go = null)
    {
        resultTierPiece[0].text = tempBPiece.ToString();
        resultTierPiece[1].text = tempAPiece.ToString();
        resultTierPiece[2].text = tempSPiece.ToString();

        tempBPiece = 0;
        tempAPiece = 0;
        tempSPiece = 0;

        resultUI.SetActive(true);
        gachaAnim.SetActive(false);

        UnitPieceTextUpdate();

        if (go != null)
            go.SetActive(false);
    }

    public void UnitPieceTextUpdate()
    {
        unitTabTierPiece[0].text = "B 티어\n영웅 강화 주문서 " + pieceData.bPiece.ToString() + "개";
        unitTabTierPiece[1].text = "A 티어\n영웅 강화 주문서 " + pieceData.aPiece.ToString() + "개";
        unitTabTierPiece[2].text = "S 티어\n영웅 강화 주문서 " + pieceData.sPiece.ToString() + "개";
    }

    public void ChangeUnitPiece(int value)
    {
        ChangePiece(PieceType.Unit, value);
    }

























    //--------------------------------------------------------------------------------------Save

    public void Save(ref Save_UnitData data)
    {
        data.unitSaveDatas = new();
        data.pieceData = new();

        foreach (var (key, item) in unitDataDic)
        {
            UnitSaveData newData = new();
            item.Save(ref newData);

            data.unitSaveDatas.Add(newData);
        }

        PieceSaveData newPiece = new();
        pieceData.Save(ref newPiece);
        data.pieceData = newPiece;
    }

    //Start보다 전
    public void Load(Save_UnitData data)
    {
        foreach (var item in data.unitSaveDatas)
        {
            UnitData unit = new();
            unit.Init(unitLoader.GetByKey(item.Key), upgradeLoader.GetByKey(unitLoader.GetByKey(item.Key).UpgradeKey));
            unit.Load(item);

            unitDataDic.Add(item.Key, unit);
        }

        pieceData.Load(data.pieceData);
    }
}
