using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemData
{
    public DataTable_Shop itemData;
    public Sprite sprite;

    public ItemData(DataTable_Shop data)
    {
        itemData = data;
        sprite = Resources.Load<Sprite>(data.Path);
    }
}

public class ShopSlotUI : MonoBehaviour
{
    // 상점 내 슬롯
    [SerializeField] private TMP_Text itemNameText;     // 아이템 이름
    [SerializeField] private Image itemImage;           // 아이템 아이콘
    [SerializeField] private GameObject itemQuantity;   // 아이템 수량 오브젝트
    [SerializeField] private TMP_Text itemQuantityText; // 아이템 수량
    [SerializeField] private GameObject itemValue;      // 아이템 구매 시 가치 오브젝트 ==>> FreeItemSlot에서는 해당 부분이 없어서 오류가 나므로 아무거나 필요없는거 할당해줘야 함!!!
    [SerializeField] private TMP_Text itemValueText;    // 아이템 구매 시 가치 표기
    [SerializeField] private TMP_Text itemCostText;     // 아이템 가격

    // 팝업
    [SerializeField] private Image popupImage;          // 팝업창 아이템 이미지
    [SerializeField] private GameObject popupQuantity;  // 팝업창 아이템 수량 오브젝트
    [SerializeField] private TMP_Text popupQuantityText;// 팝업창 아이템 수량
    [SerializeField] private TMP_Text descriptionText;  // 팝업창 아이템 설명
    [SerializeField] private GameObject popupCostIcon;  // 팝업창 아이템 구입 재화 아이콘
    [SerializeField] private TMP_Text popupCostText;    // 팝업창 아이템 가격

    private DataTable_ShopLoader shopData;
    private ItemData itemData;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShopSlotUI = this;
        }
    }

    public ShopSlotUI Init(int itemID)
    {
        shopData = GameManager.Instance.DataManager.dataTable_ShopLoader;

        itemData = new ItemData(shopData.GetByKey(itemID));

        // **** 슬롯 세팅 ****
        // ** 이름
        itemNameText.text = itemData.itemData.ItemName;
        // ** 이미지
        itemImage.sprite = itemData.sprite;
        // ** 수량
        // 만약 단일 상품이면
        if(itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            itemQuantity.SetActive(true);
            itemQuantityText.text = "X " + itemData.itemData.PCount1.ToString();
        }
        // 세트 상품이면
        else
        {
            itemQuantity.SetActive(false);
        }
        // ** 상품 가치
        if (itemData.itemData.Value != "-1")
        {
            itemValue.SetActive(true);
            itemValueText.text = itemData.itemData.Value;
        }
        else
        {
            itemValue.SetActive(false);
        }
        // ** 가격
        itemCostText.text = itemData.itemData.Cost.ToString("N0");
        if(itemData.itemData.isCash)
        {
            itemCostText.text += " KRW";
        }

        return this;
    }

    // 팝업창 세팅
    public void PopupSet()
    {
        // ** 이미지
        popupImage.sprite = itemImage.sprite;
        // ** 수량
        // 만약 단일 상품이면
        if (itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            popupQuantity.SetActive(true);
            popupQuantityText.text = itemQuantityText.text;
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
        popupCostText.text = itemCostText.text;
    }
}
