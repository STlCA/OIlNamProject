using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [Header("Level")]
    public GameObject levelParent;
    private TMP_Text[] levelSlots;

    [Header("Free")]
    public GameObject freeParent;
    private RewardSlot[] freeSlots;
    private Dictionary<int, bool> getFree = new(); //저장

    [Header("GoldenPass")]
    public GameObject passParent;
    public GameObject infoTxtBox;
    private RewardSlot[] goldenSlots;
    private Dictionary<int, bool> getGolden = new(); //저장

    [Header("UI")]
    public GameObject nonClickImage;
    public GameObject shopFalse;
    public GameObject buyInfoUI;

    private DataTable_LevelPassLoader passDatabase;    
    private Sprite[] icons = new Sprite[3];

    private void Start()
    {
        passDatabase = GameManager.Instance.DataManager.dataTable_LevelPassLoader;
        GameManager.Instance.LevelReward = this;

        ListInit();
        IconInit();
        DicInit();

        RewardSetting(0);

        GoldenPassSetting();
    }

    public void GoldenPassSetting()
    {
        if(PlayerEvent.GoldenPass)
        {
            nonClickImage.SetActive(false);
            infoTxtBox.SetActive(false);
            buyInfoUI.SetActive(false);
        }
        else
        {
            infoTxtBox.SetActive(true);
            nonClickImage.SetActive(true);
        }
    }

    private void ListInit()
    {
        freeSlots = freeParent.GetComponentsInChildren<RewardSlot>();
        levelSlots = levelParent.GetComponentsInChildren<TMP_Text>();
        goldenSlots = passParent.GetComponentsInChildren<RewardSlot>();
    }

    private void IconInit()
    {
        icons[0] = Resources.Load<Sprite>("Icon/Gold");
        icons[1] = Resources.Load<Sprite>("Icon/Diamond");
        icons[2] = Resources.Load<Sprite>("Icon/Enforcebook");
    }

    private void DicInit()
    {
        if (GameManager.Instance.GetFree.Count == 0)
        {
            for (int i = 0; i < passDatabase.ItemsList.Count; ++i)
            {
                getFree.Add((i + 1), false);
                getGolden.Add((i + 1), false);
            }

            GameManager.Instance.SetLevelReward(getFree, getGolden);
        }
        else
        {
            getFree = GameManager.Instance.GetFree;
            getGolden = GameManager.Instance.GetGolden;
        }           
    }


    //BTN 메인 리워드버튼에서 켜지기전에 부르기
    public void RewardSetting(int count)//1~30 = 0 / 31~60 = 1/61~90 = 2 ...
    {
        /*        int count = GameManager.Instance.Player.Level / 20;*/

        GameManager.Instance.SoundManager.EffectAudioClipPlay(0);

        for (int i = 30; i > 0; i--)//20 19 18 17 ~ 1
        {
            int level = i + (30 * count);
            int index = level - 1;

            levelSlots[30 - i].text = "Lv." + level.ToString();

            DataTable_LevelPass data = new DataTable_LevelPass();
            data = passDatabase.ItemsList[index];

            bool canGetLevel = GameManager.Instance.Player.Level >= level;
            bool canGet = !getFree[level];

            freeSlots[30 - i].Init(this, level, LevelRewardType.Free, (RewardType)data.FreeType, data.FreeValue, canGet, icons, canGetLevel);

            canGet = !getGolden[level];

            goldenSlots[30 - i].Init(this, level, LevelRewardType.GoldenPass, (RewardType)data.GoldenType, data.GoldenValue, canGet, icons, canGetLevel);
        }
    }

    public void BuyGoldenPass()
    {
        //shopFalse.SetActive(false);
        GameManager.Instance.IAPManager.BuyProduct("1112");
    }

    public void GetReward(int level, LevelRewardType type)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(8);

        switch (type)
        {
            case LevelRewardType.Free:
                getFree[level] = true; break;
            case LevelRewardType.GoldenPass:
                getGolden[level] = true; break;
        }
    }

}

