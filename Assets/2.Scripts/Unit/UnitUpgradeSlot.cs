using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeSlot : MonoBehaviour
{
    public UnitData unitData;

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

    public void Init(UnitData unitData, DataTable_Upgrade upgradeData)
    {
        this.unitData = null;
        this.unitData = unitData;

        unitImage.sprite = unitData.sprite;

        switch (unitData.tier)
        {
            case 1:
                tierImage.color = Color.red;
                break;
            case 2:
                tierImage.color = Color.blue;
                break;
            case 3:
                tierImage.color = Color.black;
                break;
        }

        if (unitData.piece >= upgradeData.NeedPiece[unitData.level] && upgradeData.UseGold[unitData.level] <= GameManager.Instance.Gold)
            btnFalseImage.gameObject.SetActive(false);
        else
            btnFalseImage.gameObject.SetActive(true);

            nameTxt.text = unitData.name;
        tierTxt.text = unitData.tier.ToString();
        levelTxt.text = "Lv. " + unitData.level.ToString();
        pieceTxt.text = unitData.piece.ToString() + " / " + upgradeData.NeedPiece[unitData.level].ToString();
        atkTxt.text = "°ø°Ý·Â\n" + unitData.atk.ToString();
        plusAtkTxt.text = "+" + upgradeData.ATK[unitData.level].ToString();
        speedTxt.text = unitData.speed.ToString();
        //plusSpeedTxt.text = "+" + upgradeData.Speed[unitData.level].ToString();
        upgradeGoldTxt.text = upgradeData.UseGold[unitData.level].ToString();
    }
}
