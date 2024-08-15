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
    // ���� �� ����
    [SerializeField] private TMP_Text itemNameText;     // ������ �̸�
    [SerializeField] private Image itemImage;           // ������ ������
    [SerializeField] private GameObject itemQuantity;   // ������ ���� ������Ʈ
    [SerializeField] private TMP_Text itemQuantityText; // ������ ����
    [SerializeField] private GameObject itemValue;      // ������ ���� �� ��ġ ������Ʈ ==>> FreeItemSlot������ �ش� �κ��� ��� ������ ���Ƿ� �ƹ��ų� �ʿ���°� �Ҵ������ ��!!!
    [SerializeField] private TMP_Text itemValueText;    // ������ ���� �� ��ġ ǥ��
    [SerializeField] private TMP_Text itemCostText;     // ������ ����

    // �˾�
    [SerializeField] private Image popupImage;          // �˾�â ������ �̹���
    [SerializeField] private GameObject popupQuantity;  // �˾�â ������ ���� ������Ʈ
    [SerializeField] private TMP_Text popupQuantityText;// �˾�â ������ ����
    [SerializeField] private TMP_Text descriptionText;  // �˾�â ������ ����
    [SerializeField] private GameObject popupCostIcon;  // �˾�â ������ ���� ��ȭ ������
    [SerializeField] private TMP_Text popupCostText;    // �˾�â ������ ����

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

        // **** ���� ���� ****
        // ** �̸�
        itemNameText.text = itemData.itemData.ItemName;
        // ** �̹���
        itemImage.sprite = itemData.sprite;
        // ** ����
        // ���� ���� ��ǰ�̸�
        if(itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
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
        // ** ����
        itemCostText.text = itemData.itemData.Cost.ToString("N0");
        if(itemData.itemData.isCash)
        {
            itemCostText.text += " KRW";
        }

        return this;
    }

    // �˾�â ����
    public void PopupSet()
    {
        // ** �̹���
        popupImage.sprite = itemImage.sprite;
        // ** ����
        // ���� ���� ��ǰ�̸�
        if (itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            popupQuantity.SetActive(true);
            popupQuantityText.text = itemQuantityText.text;
        }
        // ��Ʈ ��ǰ�̸�
        else
        {
            popupQuantity.SetActive(false);
        }
        // ** ����
        descriptionText.text = itemData.itemData.Description;
        // ** ��ȭ ������
        if (itemData.itemData.isCash)
        {
            popupCostIcon.SetActive(false);
        }
        else
        {
            popupCostIcon.SetActive(true);
        }
        // ** ����
        popupCostText.text = itemCostText.text;
    }
}
