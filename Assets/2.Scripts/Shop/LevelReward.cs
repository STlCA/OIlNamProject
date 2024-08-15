using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [Header("Free")]
    public GameObject freeParent;
    private RewardSlot[] freeSlots;
    private Dictionary<int, bool> getFree = new(); //저장

    [Header("Level")]
    public GameObject levelParent;
    private TMP_Text[] levelSlots;

    [Header("GoldenPass")]
    public GameObject passParent;
    private RewardSlot[] goldenSlots;
    private Dictionary<int, bool> getGolden = new(); //저장

    public GameObject nonClickImage;

    private DataTable_LevelPassLoader passDatabase;
    private Sprite[] icons = new Sprite[3];

    private void Start()
    {
        passDatabase = GameManager.Instance.DataManager.dataTable_LevelPassLoader;

        ListInit();
        IconInit();
        DicInit();

        RewardSetting(0);
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
        if (getFree.Count != 0)
            return;

        for (int i = 0; i < 130; ++i)
        {
            getFree.Add((i + 1), false);
            getGolden.Add((i + 1), false);
        }
    }


    //BTN 메인 리워드버튼에서 켜지기전에 부르기
    public void RewardSetting(int count)//1~20 = 0 / 21~40 = 1/41~60 = 2 ...
    {
        /*        int count = GameManager.Instance.Player.Level / 20;*/

        for (int i = 20; i > 0; i--)//20 19 18 17 ~ 1
        {
            int level = i + (20 * count);
            int index = level - 1;

            levelSlots[20 - i].text = "Lv." + level.ToString();

            DataTable_LevelPass data = new DataTable_LevelPass();
            data = passDatabase.ItemsList[index];

            bool canGetLevel = GameManager.Instance.Player.Level >= level;
            bool canGet = !getFree[level];

            freeSlots[20 - i].Init(this, level, LevelRewardType.Free, (RewardType)data.FreeType, data.FreeValue, canGet, icons, canGetLevel);

            canGet = !getGolden[level];

            goldenSlots[20 - i].Init(this, level, LevelRewardType.GoldenPass, (RewardType)data.GoldenType, data.GoldenValue, canGet, icons, canGetLevel);
        }
    }

    public void GetReward(int level, LevelRewardType type)
    {
        switch (type)
        {
            case LevelRewardType.Free:
                getFree[level] = true; break;
            case LevelRewardType.GoldenPass:
                getGolden[level] = true; break;
        }
    }

}

