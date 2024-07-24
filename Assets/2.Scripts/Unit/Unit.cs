using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IPointerClickHandler
{
    public UnitController controller; // ³ªÁß¿¡private

    public GameObject btnUI;
    public Image nonClickImage;

    private Image iconImage;

    public int id;

    private void Start()
    {
        if (GameManager.Instance != null)
            controller = GameManager.Instance.UnitController;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIOnOff(btnUI);
    }

    public void UIOnOff(GameObject ui)
    {
        if (ui == null)
            return;

        if (ui.activeSelf == true)
            ui.SetActive(false);
        else
        {
            if (controller.CanUpgradeCheck(id))
                nonClickImage.gameObject.SetActive(false);
            else
                nonClickImage.gameObject.SetActive(true);

            ui.SetActive(true);
        }
    }

    public void UnitUpgrade()
    {

    }
}
