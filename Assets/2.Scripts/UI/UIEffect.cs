using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using TMPro;

public class UIEffect : MonoBehaviour
{
    [Header("Type")]
    public UIEffectType type;

    [Header("ON/OFF")]
    public bool on = false;

    [Header("Effect")]
    public bool blink = false;

    private TMP_Text text = null;
    private Image image = null;
    private GameObject go = null;

    //Blink
    private float alpha = 1f;
    private float blinkSpeed = -1f;
    private Color color = Color.black;

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
            //case UIEffectType.OBJECT:
            //    go = gameObject; break;
        }

        if (on) go.SetActive(true);
        else go.SetActive(false);
    }

    private void Update()
    {
        BlinkSet();
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
                //case UIEffectType.OBJECT:
                //    go = gameObject; break;
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
}
