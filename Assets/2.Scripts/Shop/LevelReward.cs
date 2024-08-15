using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [Header("Free")]
    public GameObject freeParent;
    private RewardSlot[] freeSlots;

    [Header("Level")]
    public GameObject levelParent;
    private TMP_Text[] levelSlots;

    [Header("GoldenPass")]
    public GameObject passParent;
    private RewardSlot[] passSlots;
    public GameObject nonClickImage;

    private DataTable_LevelPassLoader passDatabase;

    private void Start()
    {
        passDatabase = GameManager.Instance.DataManager.dataTable_LevelPassLoader;

        ListInit();
    }

    private void ListInit()
    {
        freeSlots = freeParent.GetComponentsInChildren<RewardSlot>();
        levelSlots = levelParent.GetComponentsInChildren<TMP_Text>();
        passSlots = passParent.GetComponentsInChildren<RewardSlot>();
    }

    //BTN 메인 리워드버튼에서 켜지기전에 부르기
    public void RewardSetting()
    {
        //TODO:20단위 버튼으로 나누기 

        int index = GameManager.Instance.Player.Level - 1;//이거아니고 잘리는 숫자

        for (int i = 0; i < 20; i++)
        {
            levelSlots[i].text = "Lv." + (index + i).ToString();
            //freeSlots[i]
            //passSlots[i]

            switch (passDatabase.ItemsList[i].LevelType)
            {
                case 1://골드 //이미지, LevelValue 텍스트설정, init(), 보상어디까지받았는지 노말용 패스용 레벨별 truefalse 딕셔너리만들기///버튼달아놨는데 스스로를 주고 스스로의 타입에따라 돈받기
                    break;
                case 2://다이아
                    break;
                case 3://모집권
                    break;
            }

            switch (passDatabase.ItemsList[i].GoldenType)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

    }
}
