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
    public TMP_Text levelTxt;
    public TMP_Text pieceTxt;
    public TMP_Text nameTxt;

    public void Init(UnitData unitData, DataTable_Upgrade upgradeData)
    {
        this.unitData = null;
        this.unitData = unitData;

        unitImage.sprite = unitData.sprite;
        levelTxt.text = "Lv. " + unitData.level.ToString();
        pieceTxt.text = unitData.piece.ToString() + " / " + upgradeData.NeedPiece[unitData.level].ToString();
        nameTxt.text = unitData.name;
    }
}
