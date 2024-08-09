using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour
{
    private UnitSpawnController controller;
    private Vector3 key;

    public void Init(UnitSpawnController controller,Vector3 key)
    {
        this.controller = controller;
        this.key = key;
    }

    //BTN
    public void Upgrade()
    {
        controller.Upgrade(key);
    }

    //BTN
    public void UnitSell()//컨트롤러부르기
    {
        controller.UnitSell(key);
    }
}
