using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Save_TimeData
{
    public string LastTime;
}

public class TimeManager : Manager
{
    //public string saveLastDate;//���������������
    //saveLastDate = "2024-08-14T03:06:01";

    public DateTime lastDate = default;//���������� DataTime�������� ��ȯ
    private static DateTime now;
    private TimeSpan timeOffset;//���� - �������� �ʷκ�ȯ

    public TMP_Text offsetTxt;

    private int keyTime;
    private bool keyTimerStart = false;

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        keyTime = 360;
        now = DateTime.Now;
    }

    private void Start()
    {
        if (lastDate != default)
            StartKeyCheck();//���ݱ��� �ڸ��� ��� �ð� ����ؼ� �׸�ŭ ��ȭ����  
    }

    private void StartKeyCheck()
    {
        timeOffset = now - lastDate;

        int count = (int)timeOffset.TotalSeconds / keyTime >= 30 ? 30 : (int)timeOffset.TotalSeconds / keyTime;

        Debug.Log(count + "�� Ű ����");

        for (int i = 0; i < count; i++)
        {
            GetKey();
        }

        if (GameManager.Instance.Key < 30)
        {
            keyTimerStart = true;

            while (timeOffset >= new TimeSpan(00, 06, 00))
            {
                timeOffset -= new TimeSpan(00, 06, 00);
            }

            lastDate = DateTime.Now - timeOffset;
        }
        else
            lastDate = DateTime.MaxValue;
    }

    private void Update()
    {
        if (keyTimerStart)
            KeyTimerUpdate();
    }

    private void KeyTimerUpdate()
    {
        now = DateTime.Now;

        timeOffset = now - lastDate;

        offsetTxt.text = ((keyTime - (int)timeOffset.TotalSeconds) / 60).ToString("00") + " : " + ((keyTime - (int)timeOffset.TotalSeconds) % 60).ToString("00");

        if ((int)timeOffset.TotalSeconds >= keyTime)
        {
            GetKey();
        }
    }

    public void KeyTimerStart(bool keyTimer)//���� ��������� true 30�������� false
    {
        keyTimerStart = keyTimer;

        if (!keyTimerStart)//Ÿ�̸� �ȵ��ư�����
        {
            offsetTxt.text = "";
            lastDate = DateTime.MaxValue;
        }
        else if (keyTimerStart)
            lastDate = DateTime.Now;
        else
            Debug.Log("lastDate�״��");
    }

    public void TextUIInit(TMP_Text offsetTxt)
    {
        this.offsetTxt = offsetTxt;
    }

    private bool CanGetKey()
    {
        if (GameManager.Instance == null)
            return false;

        return true;
    }

    private void GetKey()
    {
        if (!CanGetKey())
            return;

        lastDate = DateTime.Now;
        GameManager.Instance.MoneyChange(MoneyType.KEY, 1);
    }

    //--------------------------------------------------------------Save

    public void Save(ref Save_TimeData saveData)
    {
        if (lastDate == DateTime.MaxValue)
            saveData.LastTime = "";
        else if (lastDate < DateTime.MaxValue)
            saveData.LastTime = lastDate.ToString("s");
    }

    public void Load(Save_TimeData saveData)
    {
        if (saveData.LastTime.Length <= 0)
            lastDate = DateTime.MaxValue;
        else
            lastDate = Convert.ToDateTime(saveData.LastTime);
    }
}
