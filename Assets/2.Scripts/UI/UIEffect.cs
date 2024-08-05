using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using TMPro;
using Unity.VisualScripting;

public class UIEffect : MonoBehaviour
{
    [Header("Type")]
    public UIEffectType type;

    [Header("Effect")]
    public bool blink = false;
    public bool info = false;

    private TMP_Text text = null;
    private Image image = null;
    private GameObject go = null;

    //Blink
    private float alpha = 1f;
    private float blinkSpeed = -1f;
    private Color color = Color.black;

    //info
    private RectTransform rect = null;
    public bool on = false;

    private void Start()
    {
        switch (type)
        {
            case UIEffectType.TEXT:
                text = GetComponent<TMP_Text>();
                go = text.gameObject; break;
            case UIEffectType.IMAGE:
                image = GetComponent<Image>();
                go = image.gameObject; break;
            case UIEffectType.OBJECT:
                go = gameObject;
                image = GetComponentInChildren<Image>();
                text = GetComponentInChildren<TMP_Text>();
                rect = GetComponent<RectTransform>();
                break;
        }

        alpha = 1f;
        blinkSpeed = -1f;
    }

    private void Update()
    {
        if (blink)
            BlinkSet();
        else if (info && !on)
            DownTop(go);
        else if (on)
            DownTop(go, true);
    }

    public void BlinkSet()
    {
        go.SetActive(true);

        switch (type)
        {
            case UIEffectType.TEXT:
                Blink(text); break;
            case UIEffectType.IMAGE:
                Blink(image); break;
            case UIEffectType.OBJECT:
                go = gameObject;

                break;
        }
    }

    public void Blink(TMP_Text txt)
    {
        alpha += Time.deltaTime * blinkSpeed;

        if (alpha >= 1f)
            blinkSpeed *= -1;
        else if (alpha <= 0f)
            blinkSpeed *= -1;

        color.a = alpha;
        text.color = color;

    }

    public void Blink(Image img)
    {
        alpha += Time.deltaTime * blinkSpeed;

        if (alpha >= 1f)
            blinkSpeed *= -1;
        else if (alpha <= 0f)
            blinkSpeed *= -1;

        color.a = alpha;
        image.color = color;
    }

    public void DownTop(GameObject go, bool isOn = false)
    {
        if (isOn)
        {
            alpha = 1f;
            rect.anchoredPosition = new Vector2(0, 0);
            on = false;
        }

        alpha += Time.deltaTime * blinkSpeed;

        Vector2 temp = new Vector2();
        temp.y = Mathf.Lerp(rect.anchoredPosition.y, 100, Time.deltaTime);

        rect.anchoredPosition = temp;

        color = text.color;
        color.a = alpha;
        text.color = color;

        color = image.color;
        color.a = alpha;
        image.color = color;

        if (alpha <= 0f)
        {
            on = true;
            gameObject.SetActive(false);
        }
    }
}
