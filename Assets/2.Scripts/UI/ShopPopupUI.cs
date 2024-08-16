using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private Image popupImage;          // 팝업창 아이템 이미지
    [SerializeField] private GameObject popupQuantity;  // 팝업창 아이템 수량 오브젝트
    [SerializeField] private TMP_Text popupQuantityText;// 팝업창 아이템 수량
    [SerializeField] private TMP_Text descriptionText;  // 팝업창 아이템 설명
    [SerializeField] private GameObject popupCostIcon;  // 팝업창 아이템 구입 재화 아이콘
    [SerializeField] private TMP_Text popupCostText;    // 팝업창 아이템 가격

    private ItemData itemData;

    // 팝업창 세팅
    public void PopupSet(ItemData itemData)
    {
        this.itemData = itemData;

        // ** 이미지
        popupImage.sprite = itemData.sprite;
        // ** 수량
        // 만약 단일 상품이면
        if (itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            popupQuantity.SetActive(true);
            popupQuantityText.text = "X " + itemData.itemData.PCount1.ToString();
        }
        // 세트 상품이면
        else
        {
            popupQuantity.SetActive(false);
        }
        // ** 설명
        descriptionText.text = itemData.itemData.Description;
        // ** 재화 아이콘
        if (itemData.itemData.isCash)
        {
            popupCostIcon.SetActive(false);
        }
        else
        {
            popupCostIcon.SetActive(true);
        }
        // ** 가격
        popupCostText.text = itemData.itemData.Cost.ToString("N0");
        if (itemData.itemData.isCash)
        {
            popupCostText.text += " KRW";
        }
    }

    // 구매 확인 버튼
    public void PurchaseOnClick()
    {
        // 인앱 결제 상품
        if (itemData.itemData.isCash && itemData.itemData.MoneyType == -1)
        {
            GameManager.Instance.IAPManager.BuyProduct(itemData.itemData.key.ToString());
        }
        // 골드로 구매
        else if (itemData.itemData.MoneyType == 1501)
        {
            if (GameManager.Instance.Gold >= itemData.itemData.Cost)
            {
                GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, -itemData.itemData.Cost);
                GameManager.Instance.MoneyChange(Constants.MoneyType.KEY, itemData.itemData.PCount1);
            }
            else
                mainSceneManager.falseGold.SetActive(true);
        }
        // 다이아로 구매
        else if (itemData.itemData.MoneyType == 1502)
        {
            if (itemData.itemData.PID1 == 1501)
            {
                if (GameManager.Instance.Diamond >= itemData.itemData.Cost)
                {
                    GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, -itemData.itemData.Cost);
                    GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, itemData.itemData.PCount1);
                }
                else
                    mainSceneManager.falseDiamond.SetActive(true);
            }
            else if (itemData.itemData.PID1 == 1504)
            {
                if (GameManager.Instance.Diamond >= itemData.itemData.Cost)
                {
                    GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, -itemData.itemData.Cost);
                    GameManager.Instance.UnitManager.ChangeUnitPiece(itemData.itemData.PCount1);
                }
                else
                    mainSceneManager.falseDiamond.SetActive(true);
            }
        }
        // 광고는 어떻게 해야할지 생각좀 해보자.
    }
}
