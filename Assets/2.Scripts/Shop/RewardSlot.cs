using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    private LevelReward levelReward;                    // 컨트롤러

    [Header("Self")]
    public Image icon;                                  // 보상 아이콘
    public TMP_Text iconValue;                          // 보상 수량 텍스트
    public Image normalBox;                             // 슬롯 색 바꾸기위한
    public GameObject canGetGO;                         // 얻을 수 있을때 버튼
    public GameObject getGO;                            // 획득함 이미지
    public GameObject getGOText;                        // 획득함 글자

    private int level;                                  // 레벨
    private LevelRewardType levelRewardType;            // 골든인지 일반인지
    private RewardType rewardType;                      // 골드 다이아 모집권.
    private int rewardValue;                            // 보상 수량

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

        if (canGet && canGetLevel)//얻을수있으면
        {
            normalBox.color = Color.white;
            canGetGO.SetActive(true);

            getGO.SetActive(false);
            getGOText.SetActive(false);
        }
        else if (!canGetLevel)//레벨이 안됨//else if 순서바뀌면안됨
        {
            normalBox.color = Color.gray;

            canGetGO.SetActive(false);

            getGO.SetActive(true);
            getGOText.SetActive(false);
        }
        else if (!canGet)//이미 얻음
        {
            canGetGO.SetActive(false);

            getGO.SetActive(true);
            getGOText.SetActive(true);
        }
        else
            Debug.Log("예외발생");
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
