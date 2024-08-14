using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : Manager
{
    public string saveLastDate;//마지막접속저장용

    public DateTime lastDate;//마지막접속 DataTime형식으로 변환
    private static DateTime now;
    private TimeSpan timeOffset;//지금 - 마지막을 초로변환

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

        //지금까지 자리를 비운 시간 계산해서 그만큼 재화지급
        int count = (int)timeOffset.TotalSeconds / keyTime >= 30 ? 30 : (int)timeOffset.TotalSeconds / keyTime;

        Debug.Log(count + "번 키 얻음");

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
        timeOffset = now - lastDate;//lastDate를 열쇠를 써서 열쇠가 30개보다 작아졌을떄 갱신

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
