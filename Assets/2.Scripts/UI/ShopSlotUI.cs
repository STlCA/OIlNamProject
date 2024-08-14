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
    [SerializeField] private TMP_Text itemNameText;     // ������ �̸�
    [SerializeField] private Image itemImage;      // ������ ������
    [SerializeField] private GameObject itemQuantity;   // ������ ���� ������Ʈ
    [SerializeField] private TMP_Text itemQuantityText; // ������ ����
    [SerializeField] private TMP_Text itemCostText;     // ������ ����

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

        // ���� ����
        itemNameText.text = itemData.itemData.ItemName;
        itemImage.sprite = itemData.sprite;
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
        itemCostText.text = itemData.itemData.Cost.ToString("N0");
        if(itemData.itemData.isCash)
        {
            itemCostText.text += " KRW";
        }

        return this;
    }
}
