using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private int time;
    private float curTime;

    private int minute;
    private int second;

    private WaveUI waveUI;
    public DataManager dataManager;//임시//수정
    public DataTable_ChapterLoader chapterDatabase;

    private Coroutine coTimer = null;

    private void Awake()
    {
        //time = 30;

        //SetTimer();
    }

    public void Init()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TimerUI = this;
            waveUI = GameManager.Instance.WaveUI;
        }

        chapterDatabase = dataManager.dataTable_ChapterLoader;

        time = chapterDatabase.GetByKey(waveUI.currentWave).Time;

        SetTimer(time);
    }

    // 타이머 시작
    public void SetTimer(int seconds)
    {
        if (coTimer != null)
        {
            StopCoroutine(coTimer);
        }
        coTimer = StartCoroutine(CoRunTimer(seconds));
    }

    // nn초동안 돌릴 타이머
    private IEnumerator CoRunTimer(int seconds)
    {
        curTime = seconds;

        while (curTime > 0)
        {
            UpdateTimerUI();
            yield return null;
        }

        if (curTime <= 0)
        {
            //SetTimer();
            waveUI.NextWave();
        }
    }

    // 타이머 UI 업데이트
    private void UpdateTimerUI()
    {
        if (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            timeText.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }
}
