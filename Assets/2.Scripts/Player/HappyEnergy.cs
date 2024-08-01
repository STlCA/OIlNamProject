using Constants;
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
    public DataTable_MessageLoader messageDatabase;
    private DataTable_Message currentMessage = new();
    public DataManager tempDataManager;

    [Header("EnergyBar")]
    public Slider energySlider;
    public TMP_Text energyText;

    [Header("PopUP")]
    public GameObject popUp;
    public GameObject clickFalse;
    public TMP_Text message;
    public GameObject slot1;
    private TMP_Text[] text1;
    public GameObject slot2;
    private TMP_Text[] text2;
    public GameObject slot3;
    private TMP_Text[] text3;


    private string text;

    /*
     *  확률때 필요
        private float totalTime;
        private int tryCount;
        private float tryTime;
        private float percent;*/

    private bool onPopup = false;
    private bool onBad = false;
    private bool onHappy = false;
    private bool onLove = false;

    public int Energy
    {
        get { return energy; }
        private set
        {
            energy += value;

            if (energy >= totalEnergy)
                energy = totalEnergy;

            energySlider.value = (float)energy / totalEnergy;
            PercentChange();
            TextChange();
            /*EnergyCheck();*/
        }
    }
    private int energy;

    private int totalEnergy;
    private float currentEnergyPercent;


    private void PercentChange()
    {
        currentEnergyPercent = (float)energy / totalEnergy * 100;
    }

    private void TextChange()
    {
        text = "";
        for (int i = 0; i < currentEnergyPercent.ToString().Length; i++)
        {
            text += currentEnergyPercent.ToString()[i];
            text += "\n";
        }
        text += "%";

        energyText.text = text;
    }

    private void HappyEnergyInit()
    {
        totalEnergy = 200;
        Energy = 190;
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            messageDatabase = GameManager.Instance.DataManager.dataTable_MessageLoader;
        }
        else
        {
            messageDatabase = tempDataManager.dataTable_MessageLoader;
        }

        HappyEnergyInit();

        text1 = slot1.GetComponentsInChildren<TMP_Text>();
        text2 = slot2.GetComponentsInChildren<TMP_Text>();
        text3 = slot3.GetComponentsInChildren<TMP_Text>();
    }

    private void Update()
    {
        /*        if (!onPopup)
                    totalTime += Time.deltaTime;

                if (totalTime >= tryTime)
                {
                    CallTry();
                    totalTime = 0;
                }*/
    }

    /*    private void EnergyCheck()
        {
            if (1 <= currentEnergyPercent && currentEnergyPercent <= 30)
                SetTryValue(3, 10);
            else if (31 <= currentEnergyPercent && currentEnergyPercent <= 60)
                SetTryValue(5, 25);
            else if (61 <= currentEnergyPercent && currentEnergyPercent <= 80)
                SetTryValue(7, 40);
            else if (81 <= currentEnergyPercent && currentEnergyPercent <= 100)
                SetTryValue(10, 55);
        }*/

    public void ChangeHappyEnergy(int val)
    {
        Energy = val;
    }

    /*    private void SetTryValue(int tryCount, float percent)
        {
            this.tryCount = tryCount;
            this.percent = percent;
            tryTime = 60 / tryCount;
        }*/

    /*    private void CallTry()
        {
            Debug.Log("계산 돌아가는중");

            int random = Random.Range(0, 100);

            if (random <= percent)
            {
                onPopup = true;
                SetPopUp();
            }
        }*/

    public void SetPopUp()//웨이브체크해서부르기 TODO
    {
        if (onPopup)
            return;
        else
        {
            Debug.Log("켜짐");
            SetMessage();
            popUp.gameObject.SetActive(true);
            onPopup = true;
        }

    }

    private void SetMessage()
    {
        List<int> list = new();

        foreach (var item in messageDatabase.ItemsList)
        {
            list.Add(item.key);
        }

        int temp = Random.Range(0, list.Count);


        currentMessage = messageDatabase.GetByKey(list[temp]);

        message.text = currentMessage.Message;

        text1[0].text = currentMessage.Info1_1;
        text1[1].text = currentMessage.Info1_2;        
        text1[2].text = currentMessage.Answer1;
        text1[3].text = currentMessage.Energy1.ToString();
        text1[4].text = currentMessage.Price1.ToString();

        text2[0].text = currentMessage.Info2;
        text2[1].text = currentMessage.Answer2;
        text2[2].text = currentMessage.Energy2.ToString();
        text2[3].text = currentMessage.Price2.ToString();

        text3[0].text = currentMessage.Info3;
        text3[1].text = currentMessage.Answer3;
        text3[2].text = currentMessage.Energy3.ToString();
        text3[3].text = currentMessage.Price3.ToString();

    }

    public void HappyEnergyCheck()//20미만일때 마물이동속도+5%
    {
        if (onBad)
        {
            onBad = false;
            gameSceneManager.unitController.PlusSpeed(-5, PlusChangeType.NormalChange, false);
        }            
        else if (onHappy)
        {
            onHappy = false;
            gameSceneManager.unitController.PlusATK(-10, PlusChangeType.NormalChange, false);

        }
        else if (onLove)
        {
            onLove = false;
            gameSceneManager.unitController.PlusATK(-30, PlusChangeType.NormalChange, false);
        }


        //이걸부르는곳에서 gameSceneManager.unitController.BadEnergy(5);쓰기
        if (currentEnergyPercent <= 20)
        {
            //마물
            onBad = true;
            gameSceneManager.unitController.PlusSpeed(5, PlusChangeType.NormalChange, true);
        }
        else if (75 <= currentEnergyPercent && currentEnergyPercent > 100)
        {
            onHappy = true;
            gameSceneManager.unitController.PlusATK(10, PlusChangeType.NormalChange, true);
        }
        else if (currentEnergyPercent >= 100)
        {
            onLove = true;
            gameSceneManager.unitController.PlusATK(30, PlusChangeType.NormalChange, true);
        }
        
    }

    public void ClickMessage(int num)
    {
        switch (num)
        {
            case 1:
                if (gameSceneManager.Gold < (currentMessage.Price1 * -1))
                {
                    StopCoroutine("CoClickFalse");
                    StartCoroutine("CoClickFalse");
                    return;
                }
                gameSceneManager.ChangeGold(currentMessage.Price1);
                ChangeHappyEnergy(currentMessage.Energy1);
                gameSceneManager.unitController.PlusATK(20, PlusChangeType.NormalChange,false);
                Debug.Log("//공격력20프로증가 //웨이브끝나면 끝");
                popUp.gameObject.SetActive(false);
                onPopup = false;
                break;
            case 2:
                if (gameSceneManager.Gold < (currentMessage.Price2 * -1))
                {
                    StopCoroutine("CoClickFalse");
                    StartCoroutine("CoClickFalse");
                    return;
                }
                gameSceneManager.ChangeGold(currentMessage.Price2);
                ChangeHappyEnergy(currentMessage.Energy2);
                gameSceneManager.unitController.PlusATK(2, PlusChangeType.FixChange, false);
                Debug.Log("//공+2 마물이동-2 보스이동-2 //누적");
                popUp.gameObject.SetActive(false);
                onPopup = false;
                break;
            case 3:
                gameSceneManager.ChangeGold(currentMessage.Price3);
                ChangeHappyEnergy(currentMessage.Energy3);
                popUp.gameObject.SetActive(false);
                onPopup = false;
                break;
            default:
                Debug.Log("버튼 메세지 번호 설정 안됨");
                break;
        }
    }

    private IEnumerator CoClickFalse()
    {
        clickFalse.SetActive(true);

        yield return new WaitForSecondsRealtime(2);

        clickFalse.SetActive(false);
    }
}
