using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Constants;
using System;

public class UIManager : Manager
{
    [Header("GoldUI")]
    public TMP_Text goldTxt;
    public TMP_Text keyTxt;
    public TMP_Text crystalTxt;

    public void GoldTypeUpdate(GoldType type, int val)
    {
        string valStr = val.ToString();

        int temp = 0;

        if (valStr.Length >= 4)
        {
            temp = valStr.Length;

            while (temp >= 3)
            {
                temp = valStr.Length - 3;
                valStr.Insert(temp, ",");
            }
        }

        switch (type)
        {
            case GoldType.GOLD:
                goldTxt.text = valStr;
                break;
            case GoldType.KEY:
                keyTxt.text = valStr;
                break;
            case GoldType.CRYSTAL:
                crystalTxt.text = valStr;
                break;
        }
    }
}
