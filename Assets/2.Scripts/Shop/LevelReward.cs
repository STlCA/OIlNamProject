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

    //BTN ���� �������ư���� ���������� �θ���
    public void RewardSetting()
    {
        //TODO:20���� ��ư���� ������ 

        int index = GameManager.Instance.Player.Level - 1;//�̰žƴϰ� �߸��� ����

        for (int i = 0; i < 20; i++)
        {
            levelSlots[i].text = "Lv." + (index + i).ToString();
            //freeSlots[i]
            //passSlots[i]

            switch (passDatabase.ItemsList[i].LevelType)
            {
                case 1://��� //�̹���, LevelValue �ؽ�Ʈ����, init(), ����������޾Ҵ��� �븻�� �н��� ������ truefalse ��ųʸ������///��ư�޾Ƴ��µ� �����θ� �ְ� �������� Ÿ�Կ����� ���ޱ�
                    break;
                case 2://���̾�
                    break;
                case 3://������
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
