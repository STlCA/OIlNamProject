using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Constants;
using System;
using UnityEngine.UI;

public class UIManager : Manager
{   
    //test
    [SerializeField] private TMP_Text goldTxt;
    [SerializeField] private TMP_Text keyTxt1;
    [SerializeField] private TMP_Text keyTxt2;
    [SerializeField] private TMP_Text crystalTxt;

    public void MainUIUpdate(List<TMP_Text> gold)
    {
        goldTxt = gold[0];
        keyTxt1 = gold[1];
        keyTxt2 = gold[2];
        crystalTxt = gold[3];

        GoldTypeUpdate(GoldType.GOLD, gameManager.Gold);
        GoldTypeUpdate(GoldType.KEY, gameManager.Key);
        GoldTypeUpdate(GoldType.CRYSTAL, gameManager.Crystal);
    }

    public void GoldTypeUpdate(GoldType type, int val)
    {
        if (goldTxt == null)
            return;

/*        string valStr = val.ToString();

        int temp = 0;

        if (valStr.Length >= 4)
        {
            temp = valStr.Length;

            while (temp >= 3)
            {
                temp = valStr.Length - 3;
                valStr.Insert(temp, ",");
            }
        }*/

        switch (type)
        {
            case GoldType.GOLD:
                goldTxt.text = val.ToString("N");
                break;
            case GoldType.KEY:
                keyTxt1.text = val.ToString("N");
                keyTxt2.text = val.ToString("N");
                break;
            case GoldType.CRYSTAL:
                crystalTxt.text = val.ToString("N");
                break;
        }
    }
}
