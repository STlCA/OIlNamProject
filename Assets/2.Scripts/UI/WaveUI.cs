using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private int wave = 50;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TimerUI timerUI;
    private EnemySpawn enemySpawn;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WaveUI = this;
        }
        
        timerUI = timerUI.GetComponent<TimerUI>();
        enemySpawn = GetComponent<EnemySpawn>();

        StartWave();
    }

    // Wave 시작
    private void StartWave()
    {
        timerUI.Init();
        UpdateWaveUI();
    }

    // 다음 Wave로 진입
    public void NextWave()
    {
        // 넘어갈 다음 일반 Wave가 있을 때
        currentWave++;
        UpdateWaveUI();
        timerUI.SetTimer();
        enemySpawn.RestartSpawnEnemy();
        // 다음이 보스 Wave일 때

        // 최종 보스 Wave를 종료할 때

    }

    // wave UI 업데이트
    private void UpdateWaveUI()
    {
        waveText.text = "WAVE " + currentWave.ToString();
    }
}
