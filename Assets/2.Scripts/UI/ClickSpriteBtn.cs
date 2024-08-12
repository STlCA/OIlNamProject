using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ClickSpriteBtn : MonoBehaviour, IPointerClickHandler
{
    public UnitSpawnController controller;
    public UnitButtonType type;
    public GameObject ui = null;

    private Vector3 pos;

    public void Init(UnitSpawnController controller, Vector3 pos)
    {
        this.controller = controller;
        this.pos = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(type)
        {
            case UnitButtonType.EffectOpen:
                if(controller.effectList.Count != 0)
                {
                    controller.effectList[0].SetActive(false);
                    controller.effectList.Clear();
                }
                ui.SetActive(true);
                controller.effectList.Add(ui);
                break;
            case UnitButtonType.Close:
                if (controller.onUnitPopUP.Count != 0)
                {
                    controller.onUnitPopUP[1].GetComponent<SortingGroup>().sortingOrder = 15;
                    controller.onUnitPopUP[0].SetActive(false);
                    controller.onUnitPopUP.Clear();
                }

                controller.spriteCanvas.SetActive(false);
                controller.unitBG.SetActive(false);
                break;
            case UnitButtonType.Sell:
                controller.UnitSell(pos);
                break;
            case UnitButtonType.Upgrade:
                controller.Upgrade(pos);
                break;
            default:
                Debug.Log("타입설정안됨");
                break;
        }
    }
}
