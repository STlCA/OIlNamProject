using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Manager
{
    [SerializeField] private TMP_Text nameTxt;
    //[SerializeField] private TMP_Text levelTxt;
    [SerializeField] private TMP_Text expTxt;
    [SerializeField] private TMP_Text setUINameTxt;
    [SerializeField] private TMP_InputField nameInputField;

    public string UserName { get; private set; }
    public int Level { get; private set; }
    public float ExpPercent
    {
        get
        {
            if (CurrentExp / FullExp == 1)
                return 100;
            else
                return CurrentExp / FullExp;
        }
        private set { }
    }
    public float FullExp { get; private set; }
    public float CurrentExp { get; private set; }

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        UserName = "5팀가자 ";
        Level = 1;
        FullExp = 2000;
        CurrentExp = 0;
    }

    public void PlayerUIInit(List<TMP_Text> player, TMP_InputField input)
    {
        nameTxt = player[0];
        expTxt = player[1];
        setUINameTxt = player[2];

        nameInputField = input;

        SetUserName(UserName);
    }

    public void SetUserName(string name)
    {
        UserName = name;

        nameTxt.text = name + " Lv." + Level;
        setUINameTxt.text = name;        
        expTxt.text = ExpPercent.ToString("00.00") + "%";

        //TODO : SaveSystem에 저장
    }

    public void ExpUp(int exp)
    {
        CurrentExp += exp;

        while (CurrentExp >= FullExp)
        {
            CurrentExp -= FullExp;
            Level += 1;
        }

        if (Level >= 100)
        {
            Level = 100;
            CurrentExp = FullExp;
        }
    }
}