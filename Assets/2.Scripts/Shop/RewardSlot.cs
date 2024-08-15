using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    private LevelReward levelReward;                    // ��Ʈ�ѷ�

    [Header("Self")]
    public Image icon;                                  // ���� ������
    public TMP_Text iconValue;                          // ���� ���� �ؽ�Ʈ
    public Image normalBox;                             // ���� �� �ٲٱ�����
    public GameObject canGetGO;                         // ���� �� ������ ��ư
    public GameObject getGO;                            // ȹ���� �̹���
    public GameObject getGOText;                        // ȹ���� ����

    private int level;                                  // ����
    private LevelRewardType levelRewardType;            // ������� �Ϲ�����
    private RewardType rewardType;                      // ��� ���̾� ������.
    private int rewardValue;                            // ���� ����

    public void Init(LevelReward levelReward, int level, LevelRewardType levelRewardType, RewardType type, int value, bool canGet, Sprite[] icons, bool canGetLevel)
    {
        this.levelReward = levelReward;
        
        this.level = level;
        this.levelRewardType = levelRewardType;
        rewardType = type;
        rewardValue = value;

        switch (rewardType)
        {
            case RewardType.Gold:
                icon.sprite = icons[0]; break;
            case RewardType.Diamond:
                icon.sprite = icons[1]; break;
            case RewardType.EnforceBook:
                icon.sprite = icons[2]; break;
        }

        icon.SetNativeSize();
        iconValue.text = rewardValue.ToString("N0");

        if (canGet && canGetLevel)//������������
        {
            normalBox.color = Color.white;
            canGetGO.SetActive(true);

            getGO.SetActive(false);
            getGOText.SetActive(false);
        }
        else if (!canGetLevel)//������ �ȵ�//else if �����ٲ��ȵ�
        {
            normalBox.color = Color.gray;

            canGetGO.SetActive(false);

            getGO.SetActive(true);
            getGOText.SetActive(false);
        }
        else if (!canGet)//�̹� ����
        {
            canGetGO.SetActive(false);

            getGO.SetActive(true);
            getGOText.SetActive(true);
        }
        else
            Debug.Log("���ܹ߻�");
    }

    //BTN
    public void GetReward()
    {
        //sound

        getGO.SetActive(true);
        getGOText.SetActive(true);

        switch (rewardType)
        {
            case RewardType.Gold:
                GameManager.Instance.MoneyChange(MoneyType.Gold, rewardValue);
                break;
            case RewardType.Diamond:
                GameManager.Instance.MoneyChange(MoneyType.Diamond, rewardValue);
                break;
            case RewardType.EnforceBook:
                GameManager.Instance.UnitManager.ChangeUnitPiece(rewardValue);
                break;
        }

        canGetGO.SetActive(false);

        levelReward.GetReward(level, levelRewardType);
    }


}
