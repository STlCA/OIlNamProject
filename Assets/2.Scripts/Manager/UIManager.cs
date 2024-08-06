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
    [SerializeField] private TMP_Text pointTxt;
    [SerializeField] private TMP_Text keyTxt1;
    [SerializeField] private TMP_Text rubyTxt;

    public void MainUIUpdate(List<TMP_Text> money)
    {
        pointTxt = money[0];
        keyTxt1 = money[1];
        rubyTxt = money[2];

        MoneyTypeUpdate(MoneyType.Point, gameManager.Point);
        MoneyTypeUpdate(MoneyType.KEY, gameManager.Key);
        MoneyTypeUpdate(MoneyType.Ruby, gameManager.Gold);
    }

    public void MoneyTypeUpdate(MoneyType type, int val)
    {
        if (pointTxt == null)
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
            case MoneyType.Point:
                pointTxt.text = val.ToString("N0");
                break;
            case MoneyType.KEY:
                keyTxt1.text = val.ToString("00") + " / 20";
                break;
            case MoneyType.Ruby:
                rubyTxt.text = val.ToString("N0");
                break;
        }
    }

}
