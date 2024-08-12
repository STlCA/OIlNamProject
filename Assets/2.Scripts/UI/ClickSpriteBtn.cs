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
            case UnitButtonType.Open:
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
                UnitSell();
                break;
            case UnitButtonType.Upgrade:
                Upgrade();
                break;
            default:
                Debug.Log("타입설정안됨");
                break;
        }
    }
    public void Upgrade()
    {
        controller.Upgrade(pos);
    }

    //BTN
    public void UnitSell()//컨트롤러부르기
    {
        controller.UnitSell(pos);
    }
}
