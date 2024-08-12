using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using Random = UnityEngine.Random;

[Serializable]
public class CanUpgradeClass
{
    public int count;
    public List<Vector3> keys = new();
}

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
    public List<GameObject> onUnitPopUP = new();

    public Dictionary<int, CanUpgradeClass> step0 = new();
    public Dictionary<int, CanUpgradeClass> step1 = new();

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

    // Transform Vector3�� ��ȯ
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
        if (!gameSceneManager.CanUseRuby())//�����ڶ��
        {
            rubyFalse.SetActive(true);
            return;
        }
        if (canSpawnPoints.Count <= 0)//�������ڸ���������
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

        spawnData.Add(point, unitGameData);

        if (step0.ContainsKey(newUnit.key))
        {
            step0[newUnit.key].count++;
            step0[newUnit.key].keys.Add(point);
        }
        else
        {
            CanUpgradeClass newClass = new CanUpgradeClass();
            newClass.count = 1;
            newClass.keys.Add(point);
            step0.Add(newUnit.key, newClass);
        }

        switch(newUnit.tier)
        {
            case 1:
                infoTxt.text = "<color=red>SƼ�� "+newUnit.name + "</color>" + "<color=black>" + " ������ ȹ���Ͽ����ϴ�.</color>"; break;
            case 2:
                infoTxt.text = "<color=blue>AƼ�� " + newUnit.name + "</color>" + "<color=black>" + " ������ ȹ���Ͽ����ϴ�.</color>"; break;
            case 3:
                infoTxt.text = "<#525252>BƼ�� " + newUnit.name + "</color>" + "<color=black>" + " ������ ȹ���Ͽ����ϴ�.</color>"; break;
        }
    }

    public void Upgrade(Vector3 key)
    {
        int id = spawnData[key].key;

        if (spawnData[key].Step == 0)
        {
            if(step0[id].count >= 3)
            {
                step0[id].count -= 3;

                List<Vector3> temp = new();

                for (int i = 0; i < 3; i++)
                {
                    temp.Add(step0[id].keys[i]);
                }

                foreach(var t in temp)
                {
                    if(t != key)
                    {
                        Destroy(spawnData[t].myGO);
                        spawnData.Remove(t);
                        canSpawnPoints.Add(t);
                        step0[id].keys.Remove(t);
                    }
                    else
                        step0[id].keys.Remove(t);
                }

                if (step1.ContainsKey(id))
                {
                    step1[id].count++;
                    step1[id].keys.Add(key);
                }
                else
                {
                    CanUpgradeClass newClass = new CanUpgradeClass();
                    newClass.count = 1;
                    newClass.keys.Add(key);
                    step1.Add(id, newClass);
                }

                spawnData[key].UpgradeData();
            }      

        }
        else if (spawnData[key].Step == 1)
        {
            if (step1[id].count >= 3)
            {
                step1[id].count -= 3;

                List<Vector3> temp = new();

                for (int i = 0; i < 3; i++)
                {
                    temp.Add(step1[id].keys[i]);
                }

                foreach (var t in temp)
                {
                    if (t != key)
                    {
                        Destroy(spawnData[t].myGO);
                        spawnData.Remove(t);
                        canSpawnPoints.Add(t);
                        step1[id].keys.Remove(t);
                    }
                    else
                        step1[id].keys.Remove(t);
                }

                spawnData[key].UpgradeData();
            }
        }
        else
        {
            Debug.Log("���׷��̵� ����2�ܰ���");
        }
    }

    public void UnitSell(Vector3 key)
    {
        canSpawnPoints.Add(key);
        gameSceneManager.ChangeRuby(spawnData[key].SellGold);
        Destroy(spawnData[key].myGO);
        spawnData.Remove(key);

        onUnitPopUP.Clear();
        spriteCanvas.SetActive(false);
    }

    public void SpeedChange(int val, bool isFixChange = false, bool isnow = false)
    {
        foreach(var (key, data) in spawnData)
        {
            data.SpeedStackChange(val, isFixChange, isnow);
        }
    }
    public void ATKChange(int val, bool isFixChange = false, bool isnow = false)
    {
        foreach (var (key, data) in spawnData)
        {
            data.ATKStackChange(val, isFixChange, isnow);
        }
    }
}
