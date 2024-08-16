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

    [Header("DaughterPopUP")]
    public GameObject popUp;
    public GameObject clickFalse;
    public TMP_Text message;
    public GameObject slot1;
    private TMP_Text[] text1;
    public GameObject slot2;
    private TMP_Text[] text2;
    public GameObject slot3;
    private TMP_Text[] text3;
    public List<Image> icons;
    private List<Sprite> sprites = new();

    private TMP_Text[][] texts;
    private int[] turn;

    private string text;

    /*
     *  확률때 필요
        private float totalTime;
        private int tryCount;
        private float tryTime;
        private float percent;*/

    private bool onBad = false;
    private bool onSad = false;
    private bool onHappy = false;
    private bool onLove = false;

    private float currentSpeed;

    public bool isAnswer = false;
    public int atkValue;
    public int speedValue;

    public int Energy
    {
        get { return energy; }
        private set
        {
            energy += value;

            if (energy >= totalEnergy)
                energy = totalEnergy;
            else if (energy < 0)
                energy = 0;

            energySlider.value = (float)energy / totalEnergy;
            PercentChange();

            //text = currentEnergyPercent.ToString("F0") + "%";

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
        //text = "";
        //for (int i = 0; i < currentEnergyPercent.ToString().Length; i++)
        //{
        //    text += currentEnergyPercent.ToString()[i];
        //    text += "\n";
        //}
        text = currentEnergyPercent.ToString();
        text += "%";

        energyText.text = text;
    }

    private void HappyEnergyInit()
    {
        totalEnergy = 200;
        Energy = 100;
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

        texts = new TMP_Text[3][] { text1, text2, text3 };

        sprites.Add(Resources.Load<Sprite>("PopUpIcon/Message1"));
        sprites.Add(Resources.Load<Sprite>("PopUpIcon/Message2"));
        sprites.Add(Resources.Load<Sprite>("PopUpIcon/Message3"));
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
        }

        private void CallTry()
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
        GameManager.Instance.SoundManager.EffectAudioClipPlay(10);

        SetMessage();
        popUp.gameObject.SetActive(true);

        currentSpeed = Time.timeScale;
        Time.timeScale = 0;
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

        List<int> slot = new() { 0, 1, 2 };

        turn = new int[slot.Count];

        temp = Random.Range(0, slot.Count);
        turn[slot[temp]] = 0;//0번효과

        texts[slot[temp]][0].text = currentMessage.Info1_1;
        texts[slot[temp]][0].fontSize = 25;
        texts[slot[temp]][1].text = currentMessage.Info1_2;
        texts[slot[temp]][1].fontSize = 20;
        texts[slot[temp]][2].text = currentMessage.Answer1;
        texts[slot[temp]][3].text = currentMessage.Energy1.ToString();
        texts[slot[temp]][4].text = currentMessage.Price1.ToString();
        texts[slot[temp]][5].text = "답장하기";
        icons[slot[temp]].sprite = sprites[0];

        slot.Remove(slot[temp]);

        temp = Random.Range(0, slot.Count);
        turn[slot[temp]] = 1;

        texts[slot[temp]][0].text = currentMessage.Info2;
        texts[slot[temp]][0].fontSize = 35;
        texts[slot[temp]][1].text = "";
        texts[slot[temp]][2].text = currentMessage.Answer2;
        texts[slot[temp]][3].text = currentMessage.Energy2.ToString();
        texts[slot[temp]][4].text = currentMessage.Price2.ToString();
        texts[slot[temp]][5].text = "답장하기";
        icons[slot[temp]].sprite = sprites[1];

        slot.Remove(slot[temp]);

        temp = Random.Range(0, slot.Count);
        turn[slot[temp]] = 2;

        texts[slot[temp]][0].text = currentMessage.Info3;
        texts[slot[temp]][0].fontSize = 40;
        texts[slot[temp]][1].text = "";
        texts[slot[temp]][2].text = currentMessage.Answer3;
        texts[slot[temp]][3].text = currentMessage.Energy3.ToString();
        texts[slot[temp]][4].text = "+" + currentMessage.Price3.ToString();
        texts[slot[temp]][5].text = "무시하기";
        icons[slot[temp]].sprite = sprites[2];
    }

    public void HappyEnergyCheck()//20미만일때 마물이동속도+5%
    {
        if (onBad)
        {
            onBad = false;
            gameSceneManager.unitSpawnController.ATKChange(5);
            gameSceneManager.unitSpawnController.SpeedChange(-10);
            //gameSceneManager.unitController.PlusSpeed(-5, PlusChangeType.NormalChange, false);
        }
        else if (onSad)
        {
            onBad = false;
            gameSceneManager.unitSpawnController.SpeedChange(-5);
            //gameSceneManager.unitController.PlusSpeed(-5, PlusChangeType.NormalChange, false);
        }
        else if (onHappy)
        {
            onHappy = false;
            gameSceneManager.unitSpawnController.ATKChange(-10);
            //gameSceneManager.unitController.PlusATK(-10, PlusChangeType.NormalChange, false);

        }
        else if (onLove)
        {
            gameSceneManager.heart.SetActive(false);
            onLove = false;
            gameSceneManager.unitSpawnController.ATKChange(-20);
            gameSceneManager.unitSpawnController.SpeedChange(10);
            //gameSceneManager.unitController.PlusATK(-30, PlusChangeType.NormalChange, false);
        }


        //이걸부르는곳에서 gameSceneManager.unitController.BadEnergy(5);쓰기
        if (currentEnergyPercent <= 15)
        {
            GameManager.Instance.SoundManager.EffectAudioClipPlay(11);

            //마물
            onBad = true;
            gameSceneManager.unitSpawnController.ATKChange(-5);
            gameSceneManager.unitSpawnController.SpeedChange(10);
            //gameSceneManager.unitController.PlusSpeed(5, PlusChangeType.NormalChange, false);
        }
        else if (currentEnergyPercent <= 30)
        {
            GameManager.Instance.SoundManager.EffectAudioClipPlay(11);

            //마물
            onSad = true;
            gameSceneManager.unitSpawnController.SpeedChange(5);
            //gameSceneManager.unitController.PlusSpeed(5, PlusChangeType.NormalChange, false);
        }
        else if (75 <= currentEnergyPercent && currentEnergyPercent < 100)
        {
            onHappy = true;
            gameSceneManager.unitSpawnController.ATKChange(10);
            //gameSceneManager.unitController.PlusATK(10, PlusChangeType.NormalChange, false);
        }
        else if (currentEnergyPercent >= 100)
        {
            GameManager.Instance.SoundManager.EffectAudioClipPlay(7);
            gameSceneManager.heart.SetActive(true);

            onLove = true;
            gameSceneManager.unitSpawnController.ATKChange(20);
            gameSceneManager.unitSpawnController.SpeedChange(-10);
            //gameSceneManager.unitController.PlusATK(30, PlusChangeType.NormalChange, false);
        }

    }

    public void ClickMessage(int num)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(9);

        switch (num)
        {
            case 1:
                CallEffect(0);
                break;
            case 2:
                CallEffect(1);
                break;
            case 3:
                CallEffect(2);
                break;
            default:
                Debug.Log("버튼 메세지 번호 설정 안됨");
                break;
        }
    }

    private void CallEffect(int num)
    {
        switch (turn[num])
        {
            case 0:
                if (gameSceneManager.Ruby < (currentMessage.Price1 * -1))
                {
                    StopCoroutine("CoClickFalse");
                    StartCoroutine("CoClickFalse");
                    return;
                }
                gameSceneManager.ChangeRuby(currentMessage.Price1);
                ChangeHappyEnergy(currentMessage.Energy1);
                Debug.Log("//공격력20프로증가 //웨이브끝나면 끝");
                isAnswer = true;
                atkValue = 20;
                speedValue = -10;
                //gameSceneManager.unitSpawnController.ATKChange(20);
                //gameSceneManager.unitController.PlusATK(20, PlusChangeType.NormalChange, false);
                popUp.gameObject.SetActive(false);
                break;
            case 1:
                if (gameSceneManager.Ruby < (currentMessage.Price2 * -1))
                {
                    StopCoroutine("CoClickFalse");
                    StartCoroutine("CoClickFalse");
                    return;
                }
                gameSceneManager.ChangeRuby(currentMessage.Price2);
                ChangeHappyEnergy(currentMessage.Energy2);
                isAnswer = true;
                atkValue = 2;
                speedValue = -2;
                //gameSceneManager.unitSpawnController.ATKChange(2,true);
                //gameSceneManager.unitController.PlusATK(2, PlusChangeType.FixChange, false);
                Debug.Log("//공+2 마물이동-2 보스이동-2 //누적");
                popUp.gameObject.SetActive(false);
                break;
            case 2:
                gameSceneManager.ChangeRuby(currentMessage.Price3);
                ChangeHappyEnergy(currentMessage.Energy3);
                popUp.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("버튼 메세지 번호 설정 안됨");
                break;

        }

        Time.timeScale = currentSpeed;

        if (currentEnergyPercent >= 100)
            gameSceneManager.heart.SetActive(true);
        else
            gameSceneManager.heart.SetActive(false);
    }

    private IEnumerator CoClickFalse()
    {
        clickFalse.SetActive(true);

        yield return new WaitForSecondsRealtime(2);

        clickFalse.SetActive(false);
    }
}
