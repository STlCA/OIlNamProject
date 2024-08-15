using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeUI : MonoBehaviour
{
    private UnitUpgradeController upgradeController;

    private UnitData myUnitData;
    private DataTable_Upgrade myUpgradeData;

    private int slotNum;

    public int key;

    public Image unitImage;
    public Image tierImage;
    public Image btnFalseImage;

    public TMP_Text nameTxt;
    public TMP_Text tierTxt;
    public TMP_Text levelTxt;
    public TMP_Text pieceTxt;
    public TMP_Text atkTxt;
    public TMP_Text plusAtkTxt;
    public TMP_Text speedTxt;
    //public TMP_Text plusSpeedTxt;
    public TMP_Text upgradeGoldTxt;

    public void Init(UnitUpgradeController controller, int slot, UnitData unitData, DataTable_Upgrade upgradeData)
    {
        upgradeController = controller;
        slotNum = slot;

        myUnitData = null;
        myUnitData = unitData;

        myUpgradeData = null;
        myUpgradeData = upgradeData;

        key = unitData.key;

        unitImage.sprite = unitData.profile;
        unitImage.SetNativeSize();

        switch (unitData.tier)
        {
            case 1:
                tierImage.color = Color.red;
                break;
            case 2:
                tierImage.color = Color.blue;
                break;
            case 3:
                tierImage.color = Color.gray;
                break;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        if (myUnitData.piece >= myUpgradeData.NeedPiece[myUnitData.level] && myUpgradeData.UseGold[myUnitData.level] <= GameManager.Instance.Gold)
            btnFalseImage.gameObject.SetActive(false);
        else
            btnFalseImage.gameObject.SetActive(true);

        nameTxt.text = myUnitData.name;

        switch (myUnitData.tier)
        {
            case 1:
                tierTxt.text = "S Ƽ��";
                tierTxt.color = Color.black;
                break;
            case 2:
                tierTxt.text = "A Ƽ��";
                tierTxt.color = Color.white;
                break;
            case 3:
                tierTxt.text = "B Ƽ��";                
                tierTxt.color = Color.black;
                break;
        }

        levelTxt.text = "Lv. " + myUnitData.level+1.ToString();
        pieceTxt.text = myUnitData.piece.ToString() + " / " + myUpgradeData.NeedPiece[myUnitData.level].ToString();
        atkTxt.text = "���ݷ� " + myUnitData.atk.ToString();
        plusAtkTxt.text = "+" + myUpgradeData.ATK[myUnitData.level].ToString();
        plusAtkTxt.color = Color.cyan;
        speedTxt.text = myUnitData.speed.ToString();
        //plusSpeedTxt.text = "+" + upgradeData.Speed[unitData.level].ToString();
        upgradeGoldTxt.text = myUpgradeData.UseGold[myUnitData.level].ToString();
    }


    //��ȭ��ư �������� //BTN
    public void ClickUnitUpgrade()
    {
        if (GameManager.Instance.Gold < myUpgradeData.UseGold[myUnitData.level])
            return;
        GameManager.Instance.Gold = -myUpgradeData.UseGold[myUnitData.level];//��� ����������
        upgradeController.UpdateSlot(myUnitData.tier,slotNum);//��罽�� ������Ʈ�ؾ��ϰ�
        UpdateText();//��ȭâ ������Ʈ

        SaveSystem.Save();
    }
}
