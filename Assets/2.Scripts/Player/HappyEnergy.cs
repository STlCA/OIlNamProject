using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HappyEnergy : MonoBehaviour
{
    [Header("EnergyBar")]
    public Slider energySlider;
    public TMP_Text energyText;

    [Header("PopUP")]
    public GameObject popUp;

    private string text;

    private float totalTime;
    private int tryCount;
    private float tryTime;
    private float percent;

    public int Energy
    {
        get { return energy; }
        private set
        {
            energy += value;

            if (energy >= 100)
                energy = 100;

            energySlider.value = energy / 100;
            TextChange();
            EnergyCheck();
        }
    }
    private int energy;

    private void TextChange()
    {
        text = "";
        for (int i = 0; i < energy.ToString().Length; i++)
        {
            text += energy.ToString()[i];
            text += "\n";
        }
        text += "%";

        energyText.text = text;
    }

    private void HappyEnergyInit()
    {
        Energy = 50;
    }

    private void Start()
    {
        HappyEnergyInit();
    }

    private void Update()
    {
        totalTime += Time.deltaTime;

        if (totalTime >= tryCount)
        {
            CallTry();
            totalTime = 0;
        }
    }

    private void EnergyCheck()
    {
        if (1 <= Energy && Energy <= 30)
        {
            if (Energy <= 20)
                Debug.Log("해로운 효과");
            else
                Debug.Log("해로운 효과 끄기");

            SetTryValue(3, 10);
        }
        else if (31 <= Energy && Energy <= 60)
            SetTryValue(5, 25);
        else if (61 <= Energy && Energy <= 80)
            SetTryValue(7, 40);
        else if (81 <= Energy && Energy <= 90)
            SetTryValue(10, 55);
        else
        {
            if (Energy >= 100)
                Debug.Log("이로운 효과");
            else
                Debug.Log("이로운 효과 끄기");

            SetTryValue(10, 55);
        }
    }

    public void ChangeHappyEnergy(int val)
    {
        Energy = val;
    }

    private void SetTryValue(int tryCount, float percent)
    {
        this.tryCount = tryCount;
        this.percent = percent;
        tryTime = 60 / tryCount;
    }

    private void CallTry()
    {
        int random = Random.Range(0, 100);

        if (random <= percent)
            SetPopUp();

    }

    private void SetPopUp()
    {
        popUp.gameObject.SetActive(true);
    }
}
