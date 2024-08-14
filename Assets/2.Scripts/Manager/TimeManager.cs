using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : Manager
{
    public string saveLastDate;//���������������

    public DateTime lastDate;//���������� DataTime�������� ��ȯ
    private static DateTime now;
    private TimeSpan timeOffset;//���� - �������� �ʷκ�ȯ

    public TMP_Text offsetTxt;

    private int keyTime;

    private void Start()
    {
        keyTime = 360;

        now = DateTime.Now;

        //Temp
        saveLastDate = "2024-08-14T03:06:01";

        if (saveLastDate.Length == 0)
            saveLastDate = DateTime.Now.ToString("s");

        lastDate = Convert.ToDateTime(saveLastDate);
        timeOffset = now - lastDate;

        //���ݱ��� �ڸ��� ��� �ð� ����ؼ� �׸�ŭ ��ȭ����
        int count = (int)timeOffset.TotalSeconds / keyTime >= 30 ? 30 : (int)timeOffset.TotalSeconds / keyTime;

        Debug.Log(count + "�� Ű ����");

        for (int i = 0; i < count; i++)
        {
            GetKey();
        }

        if (GameManager.Instance.Key >= 30)
        {
            offsetTxt.text = "";
            lastDate = DateTime.Now;
        }
        else
        {
            count = (int)timeOffset.TotalSeconds % keyTime;
            offsetTxt.text = ((keyTime - count) / 60).ToString("00") + " : " + ((keyTime - count) % 60).ToString("00");
            Debug.Log(offsetTxt.text);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.Key >= 30)
        {
            offsetTxt.text = "";
            return;
        }

        now = DateTime.Now;
        timeOffset = now - lastDate;//lastDate�� ���踦 �Ἥ ���谡 30������ �۾������� ����

/*        Debug.Log((int)timeOffset.TotalSeconds);*/

        offsetTxt.text = ((keyTime - (int)timeOffset.TotalSeconds) / 60).ToString("00") + " : " + ((keyTime - (int)timeOffset.TotalSeconds) % 60).ToString("00");

        if (timeOffset.Seconds >= keyTime)
        {
            GetKey();
            lastDate = DateTime.Now;
        }
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

        GameManager.Instance.MoneyChange(MoneyType.KEY, 1);
    }
}
