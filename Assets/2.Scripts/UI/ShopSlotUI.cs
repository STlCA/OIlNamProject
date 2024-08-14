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
    [SerializeField] private TMP_Text itemNameText;     // 아이템 이름
    [SerializeField] private Image itemImage;      // 아이템 아이콘
    [SerializeField] private GameObject itemQuantity;   // 아이템 수량 오브젝트
    [SerializeField] private TMP_Text itemQuantityText; // 아이템 수량
    [SerializeField] private TMP_Text itemCostText;     // 아이템 가격

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

        // 슬롯 세팅
        itemNameText.text = itemData.itemData.ItemName;
        itemImage.sprite = itemData.sprite;
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
        itemCostText.text = itemData.itemData.Cost.ToString("N0");
        if(itemData.itemData.isCash)
        {
            itemCostText.text += " KRW";
        }

        return this;
    }
}
