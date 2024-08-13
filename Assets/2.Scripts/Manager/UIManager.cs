using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Constants;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Manager
{
    //test
    [SerializeField] private TMP_Text goldTxt;
    [SerializeField] private TMP_Text keyTxt1;
    [SerializeField] private TMP_Text diamondTxt;

    public void MainUIUpdate(List<TMP_Text> money)
    {
        goldTxt = money[0];
        keyTxt1 = money[1];
        diamondTxt = money[2];

        MoneyTypeUpdate(MoneyType.Gold, gameManager.Gold);
        MoneyTypeUpdate(MoneyType.KEY, gameManager.Key);
        MoneyTypeUpdate(MoneyType.Diamond, gameManager.Diamond);
    }

    public void MoneyTypeUpdate(MoneyType type, int val)
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
            case MoneyType.Gold:
                goldTxt.text = val.ToString("N0");
                break;
            case MoneyType.KEY:
                keyTxt1.text = val.ToString("00") + " / 30";
                break;
            case MoneyType.Diamond:
                diamondTxt.text = val.ToString("N0");
                break;
        }
    }

}
