using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameUIRay : MonoBehaviour, IPointerClickHandler
{
    public UnitController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (controller.onUnitPopUP.Count != 0)
        {
            controller.onUnitPopUP[0].SetActive(false);
            controller.onUnitPopUP[1].SetActive(false);

            controller.onUnitPopUP.Clear();
        }
    }
}
