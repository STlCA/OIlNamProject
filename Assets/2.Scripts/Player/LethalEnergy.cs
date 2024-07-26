using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class LethalEnergy : MonoBehaviour
{
    public Slider energySlider;
    public TMP_Text energyText;

    private string text;

    private void Start()
    {
        LethalEnergyInit();
    }

    public float Energy
    {
        get { return energy; }
        private set
        {
            energy += value;

            if (energy >= 100)
                energy = 100;

            energySlider.value = energy / 100;
            TextChange();
        }
    }
    private float energy = 0;

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

    public void LethalEnergyInit()
    {
        Energy = 0;
    }

    public void ChangeEnergy(float val)
    {
        Energy = val;
    }

    public void UseLethal()//버튼 연결
    {
        if (Energy >= 100)
            ChangeEnergy(-100);
    }
}
