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
    public GameObject slots;//�����־����
    private UnitUpgradeSlot[] upgradeSlots;
    private Dictionary<int, UnitData> slotDic = new();

    [Header("UpgradeUI")]
    public UnitUpgradeUI unitUpgradeUI;


    void Start()
    {
        unitManager = GameManager.Instance.UnitManager;
        upgradeLoader = GameManager.Instance.DataManager.dataTable_UpgradeLoader;

        if(slots != null )
            upgradeSlots = slots.GetComponentsInChildren<UnitUpgradeSlot>();

        if (upgradeSlots == null || upgradeSlots.Length == 0)
            Debug.Log("getcomponenets ���ϴ���");
    }

    //��ȭ�� �̵��Ҷ�//BTN
    public void OnUnitTab()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Intro);

        NormalSetUpgradeSlots();
    }

    private void NormalSetUpgradeSlots()
    {
        slotDic.Clear();

        int index = 0;

        foreach (var (key, item) in unitManager.unitDataDic)
        {
            slotDic.Add(index, item);
            upgradeSlots[index].Init(item, upgradeLoader.GetByKey(item.upgradeKey));

            index++;
        }

        unitManager.UnitPieceTextUpdate();
    }

    //��ȭâ �Ӷ�//BTN
    public void OnUpgradeUI(GameObject go)//���Կ� �޾Ƽ� ��ưŬ���Ҷ� �����θ� �ֱ�
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(2);

        int slotNum = int.Parse(go.name.ToString());

        //�Ű������� go�� �޾Ƽ�
        //UnitUpgradeSlot slot = go.GetComponentInChildren<UnitUpgradeSlot>();

        unitUpgradeUI.Init(this, slotNum, slotDic[slotNum], upgradeLoader.GetByKey(slotDic[slotNum].upgradeKey));
        unitUpgradeUI.gameObject.SetActive(true);
    }

    //���� ������Ʈ
    public void UpdateSlot(int tier,int slotNum)
    {
        //�����ٲ��ȵ� 1.���Լ� ���ְ��ٲ�� 2.�����ؽ�Ʈ�ٲ��
        unitManager.ChangePiece((PieceType)tier, -slotDic[slotNum].Upgrade());//���߿� unitManager.UsePiece �ʿ��������
        //^ Ƽ� �ǽ� �ٲٱ� , ��� needPiece���� ��ȯ, �ǽ��ٲٱ�

        foreach(var slot in upgradeSlots)
        {
            if (slot.myUnitData.tier == tier)
                slot.UpdateText();
        }
    }

    //��íBTN
    public void GachaUnitPieceBtn(int count)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(9);
        unitManager.GachaUnitPiece(count);
        SaveSystem.Save();
    }
}
