using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject go;

    public void OnPointerClick(PointerEventData eventData)
    {
        go.SetActive(false);
    }
}
