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

public class GoodsData
{
    public DataTable_Goods goodsData;
    public Sprite sprite;

    public GoodsData(DataTable_Goods data)
    {
        goodsData = data;
        sprite = Resources.Load<Sprite>(data.Path);
    }
}

public class ShopSlotUI : MonoBehaviour
{
    // ���� �� ����
    [SerializeField] private TMP_Text itemNameText;     // ������ �̸�
    [SerializeField] private Image itemImage;           // ������ ������
    [SerializeField] private GameObject itemQuantity;   // ������ ���� ������Ʈ
    [SerializeField] private TMP_Text itemQuantityText; // ������ ����
    [SerializeField] private GameObject itemValue;      // ������ ���� �� ��ġ ������Ʈ ==>> FreeItemSlot������ �ش� �κ��� ��� ������ ���Ƿ� �ƹ��ų� �ʿ���°� �Ҵ������ ��!!!
    [SerializeField] private TMP_Text itemValueText;    // ������ ���� �� ��ġ ǥ��
    [SerializeField] private GameObject itemCost;       // ������ ���� ��ȭ ������
    [SerializeField] private Image itemCostIcon;        // ������ ���� ��ȭ ������
    [SerializeField] private TMP_Text itemCostText;     // ������ ����

    [SerializeField] private ShopPopupUI popupUI;
    private DataTable_ShopLoader shopDataL;
    private DataTable_GoodsLoader goodsDataL;
    private ItemData itemData;
    private GoodsData goodsData;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShopSlotUI = this;
        }
    }

    public ShopSlotUI Init(int itemID)
    {
        shopDataL = GameManager.Instance.DataManager.dataTable_ShopLoader;
        goodsDataL = GameManager.Instance.DataManager.dataTable_GoodsLoader;

        itemData = new ItemData(shopDataL.GetByKey(itemID));

        // **** ���� ���� ****
        // ** �̸�
        itemNameText.text = itemData.itemData.ItemName;
        // ** �̹���
        itemImage.sprite = itemData.sprite;
        // ** ����
        // ���� ���� ��ǰ�̸�
        if (itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            itemQuantity.SetActive(true);
            itemQuantityText.text = "X " + itemData.itemData.PCount1.ToString();
        }
        // ��Ʈ ��ǰ�̸�
        else
        {
            itemQuantity.SetActive(false);
        }
        // ** ��ǰ ��ġ
        if (itemData.itemData.Value != "-1")
        {
            itemValue.SetActive(true);
            itemValueText.text = itemData.itemData.Value;
        }
        else
        {
            itemValue.SetActive(false);
        }
        // ** ��ȭ ������
        if (!itemData.itemData.isCash && !itemData.itemData.isAd)
        {
            goodsData = new GoodsData(goodsDataL.GetByKey(itemData.itemData.MoneyType));
            itemCostIcon.sprite = goodsData.sprite;
            itemCost.SetActive(true);
        }
        else
        {
            itemCost.SetActive(false);
        }
        // ** ����
        itemCostText.text = itemData.itemData.Cost.ToString("N0");
        if (itemData.itemData.isCash)
        {
            itemCostText.text += " KRW";
        }

        return this;
    }

    // �˾� ����
    public void PopupSet()
    {
        popupUI.PopupSet(itemData, itemCostIcon);
    }
}
