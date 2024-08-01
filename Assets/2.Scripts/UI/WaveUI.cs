using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private int maxWave = 50;
    [SerializeField] public int currentWave = 1;
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
        enemySpawn.gameSceneManager.happyEnergy.HappyEnergyCheck();//속도, 공격력 nowChange true여서 바꾸는것중에 제일 아래로와야함

        currentWave++;

        // 넘어갈 다음 Wave가 있을 때
        if(currentWave < maxWave)
        {
            int tmpWave = currentWave % 10;

            UpdateWaveUI();

            // 다음이 일반 Wave일 때
            if (tmpWave != 0)
            {
                timerUI.SetTimer(30);
                enemySpawn.RestartSpawnEnemy();
            }
            // 다음이 보스 Wave일 때
            else
            {
                timerUI.SetTimer(60);
                // **** TODO : 보스몬스터 소환 구현하기 ****
            }
        }
    }

    // wave UI 업데이트
    private void UpdateWaveUI()
    {
        waveText.text = "WAVE " + currentWave.ToString();
    }
}
