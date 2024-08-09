using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameUIRay : MonoBehaviour, IPointerClickHandler
{
    public UnitSpawnController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("배경클릭중");

        if (controller.onUnitPopUP.Count != 0)
        {
            controller.onUnitPopUP[0].layer = 0;
            controller.onUnitPopUP[1].SetActive(false);
            controller.onUnitPopUP[2].SetActive(false);

            controller.onUnitPopUP.Clear();
        }

        gameObject.SetActive(false);
    }
}
