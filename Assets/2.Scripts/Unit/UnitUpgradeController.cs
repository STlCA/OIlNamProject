using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UnitUpgradeController : MonoBehaviour
{
    private UnitManager unitManager;
    private DataTable_UpgradeLoader upgradeLoader;

    [Header("UpgradeSlot")]
    public GameObject slots;
    private UnitUpgradeSlot[] upgradeSlots;


    void Start()
    {
        unitManager = GameManager.Instance.UnitManager;
        upgradeLoader = GameManager.Instance.DataManager.dataTable_UpgradeLoader;

        upgradeSlots = slots.GetComponentsInChildren<UnitUpgradeSlot>();

        if (upgradeSlots.Length == 0)
            Debug.Log("getcomponenets 못하는중");
    }

    public void OnUnitUpgradeUI()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Intro);

        NormalSetUpgradeSlots();

        slots.SetActive(true);
    }

    private void NormalSetUpgradeSlots()
    {
        int index = 0;

        foreach (var (key, item) in unitManager.unitDataDic)
        {
            upgradeSlots[index].Init(item, upgradeLoader.GetByKey(key));

            index++;
        }
    }
}
