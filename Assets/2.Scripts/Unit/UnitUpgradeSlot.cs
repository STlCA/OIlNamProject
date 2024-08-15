using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeSlot : MonoBehaviour
{
    public UnitData myUnitData;
    public DataTable_Upgrade myUpgradeData;

    public Image slotImage;
    public Image borderImage;
    public Image unitImage;
    public Image canUpgradeIcon;
    public TMP_Text levelTxt;
    public TMP_Text pieceTxt;
    public TMP_Text nameTxt;

    public void Init(UnitData item, DataTable_Upgrade dataTable_Upgrade)
    {
        myUnitData = item;
        myUpgradeData = dataTable_Upgrade;

        UpdateText();
    }

    public void UpdateText()
    {
        unitImage.sprite = myUnitData.profile;
        unitImage.SetNativeSize();

        nameTxt.text = myUnitData.name;
        levelTxt.text = "Lv. " + myUnitData.level.ToString();

        if (myUnitData.level >= 14)
        {
            pieceTxt.text = "최고단계";
            canUpgradeIcon.gameObject.SetActive(false);
        }
        else
        {
            pieceTxt.text = myUnitData.piece.ToString() + " / " + myUpgradeData.NeedPiece[myUnitData.level].ToString();

            if (myUnitData.piece >= myUpgradeData.NeedPiece[myUnitData.level])
                canUpgradeIcon.gameObject.SetActive(true);
            else
                canUpgradeIcon.gameObject.SetActive(false);
        }

        switch (myUnitData.tier)
        {
            case 1:
                slotImage.color = new Color(0.9019608f, 0.5294118f, 0.5294118f);
                borderImage.color = Color.red;
                break;
            case 2:
                slotImage.color = new Color(0.5843138f, 0.627451f, 0.9019608f);
                borderImage.color = Color.blue;
                break;
            case 3:
                slotImage.color = Color.white;
                borderImage.color = Color.white;
                break;
        }
    }
}
