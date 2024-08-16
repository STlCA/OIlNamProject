using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private Image popupImage;          // �˾�â ������ �̹���
    [SerializeField] private GameObject popupQuantity;  // �˾�â ������ ���� ������Ʈ
    [SerializeField] private TMP_Text popupQuantityText;// �˾�â ������ ����
    [SerializeField] private TMP_Text descriptionText;  // �˾�â ������ ����
    [SerializeField] private GameObject popupCostIcon;  // �˾�â ������ ���� ��ȭ ������
    [SerializeField] private TMP_Text popupCostText;    // �˾�â ������ ����

    private ItemData itemData;

    // �˾�â ����
    public void PopupSet(ItemData itemData)
    {
        this.itemData = itemData;

        // ** �̹���
        popupImage.sprite = itemData.sprite;
        // ** ����
        // ���� ���� ��ǰ�̸�
        if (itemData.itemData.PCount2 < 0 && itemData.itemData.PCount3 < 0)
        {
            popupQuantity.SetActive(true);
            popupQuantityText.text = "X " + itemData.itemData.PCount1.ToString();
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
        popupCostText.text = itemData.itemData.Cost.ToString("N0");
        if (itemData.itemData.isCash)
        {
            popupCostText.text += " KRW";
        }
    }

    // ���� Ȯ�� ��ư
    public void PurchaseOnClick()
    {
        // �ξ� ���� ��ǰ
        if (itemData.itemData.isCash && itemData.itemData.MoneyType == -1)
        {
            GameManager.Instance.IAPManager.BuyProduct(itemData.itemData.key.ToString());
        }
        // ���� ����
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
        // ���̾Ʒ� ����
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
        // ����� ��� �ؾ����� ������ �غ���.
    }
}
