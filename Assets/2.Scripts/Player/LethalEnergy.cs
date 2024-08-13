using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class LethalEnergy : MonoBehaviour
{
    public GameSceneManager gameSceneManager;

    public Slider energySlider;
    public TMP_Text energyText;

    [Header("Animation")]
    public GameObject lethalBtnAnim;
    public GameObject lethalAnim;

    private string text;

    private void Start()
    {
        gameSceneManager = GetComponent<GameSceneManager>();

        LethalEnergyInit();
    }

    public float Energy
    {
        get { return energy; }
        private set
        {
            energy += value;

            if (energy >= 200)
            {
                energy = 200;
                lethalBtnAnim.SetActive(true);                
            }
            else if (energy < 0)
                energy = 0;

            percent = energy / 200 * 100;
            energySlider.value = energy / 200;

            //text = percent.ToString("F0") + "%";

            TextChange();
        }
    }
    private float energy = 0;
    private float percent;

    private void TextChange()
    {
        //text = "";
        //for (int i = 0; i < percent.ToString("F0").Length; i++)
        //{
        //    text += percent.ToString()[i];
        //    text += "\n";
        //}
        text = percent.ToString();
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
        if (Energy < 200)
            return;

        lethalAnim.SetActive(true);
        lethalBtnAnim.SetActive(false);

        ChangeEnergy(-200);

        gameSceneManager.unitSpawnController.SpeedChange(-20, false);
        gameSceneManager.unitSpawnController.SpeedChange(20, false);

        GameManager.Instance.EnemySpawn.LethalAttack1();

        StartCoroutine("CoCancelLethal");
    }

    public IEnumerator CoCancelLethal()
    {
        yield return new WaitForSeconds(20);
        gameSceneManager.unitSpawnController.SpeedChange(20, false);
        gameSceneManager.unitSpawnController.SpeedChange(-20, false);
    }
}
