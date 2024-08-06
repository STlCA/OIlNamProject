using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeSlot : MonoBehaviour
{
    public UnitData myUnitData;
    public DataTable_Upgrade myUpgradeData;

    public Image unitImage;
    public Image canUpgradeIcon;
    public TMP_Text levelTxt;
    public TMP_Text pieceTxt;
    public TMP_Text nameTxt;

    public void Init(UnitData item, DataTable_Upgrade dataTable_Upgrade)
    {
        myUnitData = item;

        UpdateText();
    }

    public void UpdateText()
    {
        nameTxt.text = myUnitData.name;
        levelTxt.text = "Lv. " + myUnitData.level.ToString();
        pieceTxt.text = myUnitData.piece.ToString() + " / " + myUpgradeData.NeedPiece[myUnitData.level].ToString();

        if (myUnitData.piece >= myUpgradeData.NeedPiece[myUnitData.level])
            canUpgradeIcon.gameObject.SetActive(true);
        else
            canUpgradeIcon.gameObject.SetActive(false);
    }
}
