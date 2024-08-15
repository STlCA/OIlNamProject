using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class CanUpgradeClass
{
    public int count;
    public List<Vector3> pos = new();
}

public class UnitSpawnController : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    private UnitManager unitManager;
    private DataTable_UnitStepLoader unitStepLoader;

    [Header("EffectIcon")]
    public GameObject wavePlus;
    public GameObject fixPlus;
    public TMP_Text fixTxt;
    public TMP_Text fixTxt2;
    public GameObject waveSpeedPlus;

    public List<GameObject> effectList = new();//clickspritebtn스크립트에서 씀
    private int fixStack = 0;

    [Header("SpawnPoint")]
    public GameObject spawnGO;
    private Transform[] spawnArray;
    private List<Vector3> canSpawnPoints;

    [Header("FalseUI")]
    public GameObject spawnFalse;
    public GameObject rubyFalse;
    public GameObject upgradeFalse;

    [Header("UI")]
    public TMP_Text infoTxt;
    public GameObject spriteCanvas;
    public GameObject unitBG;

    [Header("UnitData")]
    public Image baseImage;
    public Image unitImage;
    public Image borderImage;
    public TMP_Text nameText;
    public TMP_Text atkTxt;
    public TMP_Text speedTxt;

    public Dictionary<Vector3, UnitGameData> spawnData = new();
    public List<GameObject> onUnitPopUP = new();

    public Dictionary<int, CanUpgradeClass> step0 = new();
    public Dictionary<int, CanUpgradeClass> step1 = new();

    private float[] percents;
    private float totalPercent;

    [Header("StackCheck")]
    public float fixSpeedStack; // 누적공격력 증가량 (2% = 2)--
    public float speedStack; // 버프 공격력 증가량 (40% = 40)--
    public float fixAtkStack; // 누적공격력 증가량 (2% = 2)--
    public float atkStack; // 버프 공격력 증가량 (40% = 40)--

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
        foreach (var t in percents)
        {
            totalPercent += t;
        }
    }

    public int RandomTier()
    {
        float random = Random.value * totalPercent;

        for (int i = 0; i < percents.Length; i++)
        {
            random -= percents[i];

            if (random <= 0)
                return i + 1;//index 0 = Stier(Excel = 1)
        }

        return 0;
    }

    public void PercentChange(float s, float a, float b)
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
        unitGameData.Init(newUnit, unitStepLoader.GetByKey(newUnit.stepKey), this, point, go);
        unitGameData.FirstStackChange(fixSpeedStack, speedStack, fixAtkStack, atkStack);

        spawnData.Add(point, unitGameData);

        if (step0.ContainsKey(newUnit.key))//업그레이드용
        {
            step0[newUnit.key].count++;
            step0[newUnit.key].pos.Add(point);
        }
        else
        {
            CanUpgradeClass newClass = new CanUpgradeClass();
            newClass.count = 1;
            newClass.pos.Add(point);
            step0.Add(newUnit.key, newClass);
        }

        switch (newUnit.tier)
        {
            case 1:
                infoTxt.text = "<color=red>S티어 " + newUnit.name + "</color>" + "<color=black>" + " 영웅을 획득하였습니다.</color>"; break;
            case 2:
                infoTxt.text = "<color=blue>A티어 " + newUnit.name + "</color>" + "<color=black>" + " 영웅을 획득하였습니다.</color>"; break;
            case 3:
                infoTxt.text = "<#525252>B티어 " + newUnit.name + "</color>" + "<color=black>" + " 영웅을 획득하였습니다.</color>"; break;
        }
    }

    public void Upgrade(Vector3 key)
    {
        int id = spawnData[key].key;

        switch (spawnData[key].Step)
        {
            case 0:
                if (step0[id].count >= 3)
                {
                    List<Vector3> temp = new();
                    int count = 0;

                    for (int i = 0; i < step0[id].count; i++)
                    {
                        if (step0[id].pos[i] != key)
                        {
                            temp.Add(step0[id].pos[i]);
                            count++;
                        }
                        if (count == 2)
                            break;                        
                    }
                    temp.Add(key);

                    step0[id].count -= 3;

                    foreach (var t in temp)
                    {
                        if (t != key)
                        {
                            Destroy(spawnData[t].myGO);
                            spawnData.Remove(t);
                            canSpawnPoints.Add(t);
                            step0[id].pos.Remove(t);
                        }
                        else//삭제안하고 1단계로 옮기기
                            step0[id].pos.Remove(t);
                    }

                    if (step1.ContainsKey(id))
                    {
                        step1[id].count++;
                        step1[id].pos.Add(key);
                    }
                    else
                    {
                        CanUpgradeClass newClass = new CanUpgradeClass();
                        newClass.count = 1;
                        newClass.pos.Add(key);
                        step1.Add(id, newClass);
                    }

                    spawnData[key].UpgradeData();
                }
                break;
            case 1:
                if (step1[id].count >= 3)
                {
                    List<Vector3> temp = new(3);
                    int count = 0;

                    for (int i = 0; i < step1[id].count; i++)
                    {
                        if (step1[id].pos[i] != key)
                        {
                            temp.Add(step1[id].pos[i]);
                            count++;
                        }
                        if (count == 2)
                            break;
                    }
                    temp.Add(key);

                    step1[id].count -= 3;

                    foreach (var t in temp)
                    {
                        if (t != key)
                        {
                            Destroy(spawnData[t].myGO);
                            spawnData.Remove(t);
                            canSpawnPoints.Add(t);
                            step1[id].pos.Remove(t);
                        }
                        else
                            step1[id].pos.Remove(t);
                    }

                    spawnData[key].UpgradeData();
                }
                break;
            case 2:
                upgradeFalse.SetActive(true);
                Debug.Log("업그레이드 스탭2단계임");
                break;
        }
    }

    public void UnitSell(Vector3 key)
    {
        //스폰포인트
        //스폰데이터
        //스탭별카운트

        canSpawnPoints.Add(key);
        gameSceneManager.ChangeRuby(spawnData[key].SellGold);

        switch (spawnData[key].Step)
        {
            case 0:
                step0[spawnData[key].key].count--;
                step0[spawnData[key].key].pos.Remove(key);
                break;
            case 1:
                step1[spawnData[key].key].count--;
                step1[spawnData[key].key].pos.Remove(key);
                break;
        }

        Destroy(spawnData[key].myGO);
        spawnData.Remove(key);

        UnitUIClear();
    }

    private void UnitUIClear()
    {
        onUnitPopUP.Clear();
        spriteCanvas.SetActive(false);
        unitBG.SetActive(false);
    }

    public void SpeedChange(int val, bool isFixChange = false, bool isWave = false)
    {
        //새로운 유닛도 알아야하니까
        if (isFixChange)
            fixSpeedStack += val;
        else
            speedStack += val;

        foreach (var (key, data) in spawnData)
        {
            data.SpeedStackChange(val, isFixChange);
        }
    }

    public void ATKChange(int val, bool isFixChange = false, bool isWave = false)
    {
        //새로운 유닛도 알아야하니까
        if (isFixChange)
            fixAtkStack += val;
        else
            atkStack += val;

        //아이콘변경
        if (isWave)
        {
            wavePlus.SetActive(true);
            waveSpeedPlus.SetActive(true);
        }
        if (isFixChange && val > 0)
        {
            fixPlus.SetActive(true);
            fixStack++;
            fixTxt.text = "x" + fixStack.ToString();
            fixTxt2.text = fixStack.ToString();
        }

        foreach (var (key, data) in spawnData)
        {
            data.ATKStackChange(val, isFixChange);
        }
    }

    public void UnitInfoSet(Vector3 pos)
    {
        UnitData unitData = spawnData[pos].myUnitData;

        switch (unitData.tier)
        {
            case 1:
                baseImage.color = new Color(0.9019608f, 0.5294118f, 0.5294118f);
                borderImage.color = Color.red;
                nameText.text = "<color=red>S티어 </color>" + unitData.name;
                break;
            case 2:
                baseImage.color = new Color(0.5843138f, 0.627451f, 0.9019608f);
                borderImage.color = Color.blue;
                nameText.text = "<color=blue>A티어 </color>" + unitData.name;
                break;
            case 3:
                baseImage.color = Color.white;
                borderImage.color = Color.white;
                nameText.text = "<#525252>B티어 </color>" + unitData.name;
                break;
        }

        unitImage.sprite = unitData.profile;
        unitImage.SetNativeSize();
        atkTxt.text = spawnData[pos].fixAtk.ToString();
        speedTxt.text = spawnData[pos].fixSpeed.ToString();
    }
}
