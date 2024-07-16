using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Manager
{
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text setNametxt;
    //[SerializeField] private TMP_Text levelTxt;
    [SerializeField] private TMP_Text expTxt;

    public TMP_Text userNameTxt;
    public TMP_InputField nameInputField;

    public string UserName { get; private set; }
    public int Level { get; private set; }
    public float ExpPercent
    {
        get
        {
            if (CurrentExt / FullExp == 1)
                return 100;
            else
                return CurrentExt / FullExp;
        }
        private set { }
    }
    public float FullExp { get; private set; }
    public float CurrentExt { get; private set; }

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        UserName = "딸천재오일남";
        Level = 1;
        FullExp = 100;
        CurrentExt = 1;
    }

    public void PlayerUIUpdate(List<TMP_Text> player)
    {
        nameTxt = player[0];
        setNametxt = player[1];
        //levelTxt = player[2];
        expTxt = player[2];

        nameTxt.text = UserName + "Lv." + Level;
        setNametxt.text = UserName;
        //levelTxt.text = Level.ToString();
        expTxt.text = ExpPercent.ToString("00.00") + "%";
    }

    public void SetUserName()
    {
        if (nameInputField.text.Length > 0)
        {
            userNameTxt.text = nameInputField.text;
            nameInputField.text = "";
        }
        else
            return;

        //TODO : SaveSystem에 저장
    }

}