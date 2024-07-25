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
    public List<Image> iconImage;

    public int id;
    private int step = 0;

    private void Start()
    {
        if (GameManager.Instance != null)
            controller = GameManager.Instance.UnitController;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (step == 3)
            return;

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

    public void UIOff()
    {
        btnUI.gameObject.SetActive(false);
    }

    public void UnitUpgrade()
    {
        controller.UnitUpgrade(id,transform.position);
        iconImage[step].gameObject.SetActive(true);

        id++;
        step++;
    }
}
