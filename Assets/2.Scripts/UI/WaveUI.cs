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
    public bool isBossWave = false;

    private EnemySpawn enemySpawn;
    private DataTable_ChapterLoader chapterDatabase;
    private HappyEnergy happyEnergy;
    [SerializeField] private GameSceneManager gameSceneManager;

    //private void Start()
    //{
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.WaveUI = this;
    //    }

    //    timerUI = timerUI.GetComponent<TimerUI>();
    //    enemySpawn = GetComponent<EnemySpawn>();

    //    StartWave();
    //}

    public void Init()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WaveUI = this;
        }

        timerUI = timerUI.GetComponent<TimerUI>();
        enemySpawn = GetComponent<EnemySpawn>();
        chapterDatabase = enemySpawn.chapterDatabase;
        happyEnergy = gameSceneManager.GetComponent<HappyEnergy>();

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
        int ruby = chapterDatabase.GetByKey(currentWave).Gold;

        enemySpawn.gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.DeleteChange);
        enemySpawn.gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.DeleteChange);

        enemySpawn.gameSceneManager.happyEnergy.HappyEnergyCheck();//속도, 공격력 nowChange true여서 바꾸는것중에 제일 아래로와야함

        enemySpawn.gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.FixChange,true);
        enemySpawn.gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.NormalChange,true);

        enemySpawn.gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.FixChange, true);
        enemySpawn.gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.NormalChange, true);


        currentWave++;

        // 넘어갈 다음 Wave가 있을 때
        if (currentWave <= maxWave)
        {
            // Wave 진입시 루비 지급
            gameSceneManager.ChangeRuby(ruby);

            int tmpWave = currentWave % 10;
            int setTime = chapterDatabase.GetByKey(currentWave).Time;
            int daughterWave = chapterDatabase.GetByKey(currentWave).Message;

            UpdateWaveUI();

            // 딸 팝업 호출
            if (daughterWave > 0)
            {
                happyEnergy.SetPopUp();
            }

            //timerUI.SetTimer(setTime);
            //enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);

            // 다음이 일반 Wave일 때
            if (tmpWave != 0)
            {
                isBossWave = false;

                int enemyCount = chapterDatabase.GetByKey(currentWave).EnemyCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);
            }
            // 다음이 보스 Wave일 때
            else if (tmpWave == 0 && currentWave != 50)
            {
                isBossWave = true;

                int bossCount = chapterDatabase.GetByKey(currentWave).BossCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(bossCount, currentWave, true);
            }
            // 다음이 50 Wave일 때
            else
            {
                isBossWave = true;

                int enemyCount = chapterDatabase.GetByKey(currentWave).EnemyCount;
                int bossCount = chapterDatabase.GetByKey(currentWave).BossCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);
                enemySpawn.RestartSpawnEnemy(bossCount, currentWave, true);
            }
        }
    }

    // wave UI 업데이트
    private void UpdateWaveUI()
    {
        waveText.text = "WAVE " + currentWave.ToString();
    }
}
