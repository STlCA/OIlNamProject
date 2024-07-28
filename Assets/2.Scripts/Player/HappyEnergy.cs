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
    public GameSceneManager gameSceneManager;

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

            if (energy >= totalEnergy)
                energy = totalEnergy;

            energySlider.value = energy / totalEnergy;
            TextChange();
            EnergyCheck();
            PercentChange();
        }
    }
    private int energy;

    private int totalEnergy;
    private float currentEnergyPercent;


    private void PercentChange()
    {
        currentEnergyPercent = energy / totalEnergy * 100;
    }

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
        Energy = 100;
        totalEnergy = 200;
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
        if (1 <= currentEnergyPercent && currentEnergyPercent <= 30)
        {
            if (currentEnergyPercent <= 20)
            {
                gameSceneManager.unitController.BadEnergy(5);
                Debug.Log("해로운 효과");
            }
            else
            {
                gameSceneManager.unitController.BadEnergy(0);
                Debug.Log("해로운 효과 끄기");
            }

            SetTryValue(3, 10);
        }
        else if (31 <= currentEnergyPercent && currentEnergyPercent <= 60)
            SetTryValue(5, 25);
        else if (61 <= currentEnergyPercent && currentEnergyPercent <= 80)
            SetTryValue(7, 40);
        else if (81 <= currentEnergyPercent && currentEnergyPercent <= 90)
            SetTryValue(10, 55);
        else
        {
            if (currentEnergyPercent >= 100)
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

    public float GetHappyEnergyPercent()//20미만일때 마물이동속도+5%
    {
        return currentEnergyPercent;
    }
}
