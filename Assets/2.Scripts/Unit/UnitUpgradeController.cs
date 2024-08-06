using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitUpgradeController : MonoBehaviour
{
    private UnitManager unitManager;
    private DataTable_UpgradeLoader upgradeLoader;

    [Header("UpgradeSlot")]
    public GameObject slots;
    private UnitUpgradeSlot[] upgradeSlots;
    private Dictionary<int, UnitData> slotDic;

    [Header("UpgradeUI")]
    public UnitUpgradeUI unitUpgradeUI;


    void Start()
    {
        unitManager = GameManager.Instance.UnitManager;
        upgradeLoader = GameManager.Instance.DataManager.dataTable_UpgradeLoader;

        upgradeSlots = slots.GetComponentsInChildren<UnitUpgradeSlot>();

        if (upgradeSlots.Length == 0)
            Debug.Log("getcomponenets 못하는중");
    }

    //강화탭 이동할때//BTN
    public void OnUnitUpgradeTab()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Intro);

        NormalSetUpgradeSlots();

        slots.SetActive(true);
    }

    private void NormalSetUpgradeSlots()
    {
        slotDic.Clear();

        int index = 0;

        foreach (var (key, item) in unitManager.unitDataDic)
        {
            slotDic.Add(index, item);
            upgradeSlots[index].Init(item, upgradeLoader.GetByKey(key));

            index++;
        }
    }

    //강화창 켤때//BTN
    public void OnUnitTab(int slotNum)//슬롯에 달아서 버튼클릭할때 스스로를 주기
    {
        //매개변수에 go를 받아서
        //UnitUpgradeSlot slot = go.GetComponentInChildren<UnitUpgradeSlot>();

        unitUpgradeUI.Init(this, slotNum, slotDic[slotNum], upgradeLoader.GetByKey(slotDic[slotNum].key));
        unitUpgradeUI.gameObject.SetActive(true);
    }

    //슬롯텍스트 업데이트
    public void UpdateSlot(int slotNum)
    {
        //순서바뀌면안됨
        slotDic[slotNum].Upgrade();
        upgradeSlots[slotNum].UpdateText();
    }
}
