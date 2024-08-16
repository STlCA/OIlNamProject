using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private List<ShopSlotUI> slots = new();

    [SerializeField] private DataManager dataManager;
    private ShopSlotUI[] shopSlotUI;
    private DataTable_ShopLoader shopData;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShopUI = this;
        }

        shopSlotUI = new ShopSlotUI[slots.Count];
        for (int i = 0; i < slots.Count; i++)
        {
            shopSlotUI[i] = slots[i];
        }

        shopData = dataManager.dataTable_ShopLoader;

        Init();
    }

    public void Init()
    {
        slots[0] = shopSlotUI[0].Init(shopData.ItemsDict.Count + 1100);

        for (int i = 1; i < slots.Count; i++)
        {
            slots[i] = shopSlotUI[i].Init(shopData.GetByKey(i + 1100).key);
        }
    }
}
