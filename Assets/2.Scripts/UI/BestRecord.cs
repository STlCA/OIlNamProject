using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text bestRecordText;
    public int bestRecord;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BestRecord = this;
        }

        Init();
    }

    public void Init()
    {
        if (GameManager.Instance.Stage == 1)
        {
            bestRecord = GameManager.Instance.BestScore1;
        }
        else
        {
            bestRecord = GameManager.Instance.BestScore2;
        }

        UpdateBestRecordUI(bestRecord);
    }

    public void BestScoreUpdate()
    {
        if (GameManager.Instance.Stage == 1)
        {
            bestRecord = GameManager.Instance.BestScore1;
        }
        else
        {
            bestRecord = GameManager.Instance.BestScore2;
        }

        UpdateBestRecordUI(bestRecord);
    }

    // �ְ� ��� UI ������Ʈ
    public void UpdateBestRecordUI(int waveNum)
    {
        bestRecordText.text = "�ְ� ��� : " + bestRecord.ToString() + " WAVE";
    }
}
