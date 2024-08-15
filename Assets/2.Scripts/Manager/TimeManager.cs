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
    //public string saveLastDate;//마지막접속저장용
    //saveLastDate = "2024-08-14T03:06:01";

    public DateTime lastDate = default;//마지막접속 DataTime형식으로 변환
    private static DateTime now;
    private TimeSpan timeOffset;//지금 - 마지막을 초로변환

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
            StartKeyCheck();//지금까지 자리를 비운 시간 계산해서 그만큼 재화지급  
    }

    private void StartKeyCheck()
    {
        timeOffset = now - lastDate;

        int count = (int)timeOffset.TotalSeconds / keyTime >= 30 ? 30 : (int)timeOffset.TotalSeconds / keyTime;

        Debug.Log(count + "번 키 얻음");

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

    public void KeyTimerStart(bool keyTimer)//열쇠 사용했을때 true 30개됐을때 false
    {
        keyTimerStart = keyTimer;

        if (!keyTimerStart)//타이머 안돌아가도댐
        {
            offsetTxt.text = "";
            lastDate = DateTime.MaxValue;
        }
        else if (keyTimerStart)
            lastDate = DateTime.Now;
        else
            Debug.Log("lastDate그대로");
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
