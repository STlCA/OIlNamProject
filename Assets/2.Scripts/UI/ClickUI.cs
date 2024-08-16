using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject go;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(0);
        go.SetActive(false);
    }
}
