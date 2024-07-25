using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private float time;
    private float curTime;

    private int minute;
    private int second;

    private Coroutine coTimer = null;

    private void Awake()
    {
        time = 30;

        SetTimer();
    }

    // 타이머 시작
    private void SetTimer()
    {
        if (coTimer != null)
        {
            StopCoroutine(coTimer);
        }
        coTimer = StartCoroutine(CoRunTimer());
    }

    // 30초동안 돌릴 타이머
    private IEnumerator CoRunTimer()
    {
        curTime = time;

        while (curTime > 0)
        {
            UpdateTimerUI();
            yield return null;
        }

        if (curTime <= 0)
        {
            SetTimer();
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
