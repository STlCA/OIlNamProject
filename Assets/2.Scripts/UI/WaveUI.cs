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
    //private WavePopUp wavePopUp;

    private int atkPlus;//����
    private int speedPlus;//����
    int ruby;
    public int currentStageWave;
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
        //wavePopUp = GetComponent<WavePopUp>();

        ruby = chapterDatabase.GetByKey(currentWave).Gold;

        if (GameManager.Instance.Stage == 1)
        {
            currentStageWave = currentWave;
        }
        else
        {
            currentStageWave = currentWave + 100;
        }

        StartWave();
    }

    // Wave ����
    private void StartWave()
    {
        timerUI.Init();
        // Wave ���Խ� ��� ����
        gameSceneManager.ChangeRuby(ruby);
        UpdateWaveUI();
    }

    // ���� Wave�� ����
    public void NextWave()
    {
        //int ruby = chapterDatabase.GetByKey(currentWave).Gold;

        if (atkPlus > 0)
        {
            gameSceneManager.unitSpawnController.ATKChange(-atkPlus, false, true);
            gameSceneManager.unitSpawnController.SpeedChange(speedPlus, false, true);
            gameSceneManager.unitSpawnController.wavePlus.SetActive(false);
            gameSceneManager.unitSpawnController.waveSpeedPlus.SetActive(false);
            atkPlus = 0;
            speedPlus = 0;
        }

        if (gameSceneManager.happyEnergy.isAnswer)//�������� ��ưŬ��
        {
            gameSceneManager.happyEnergy.isAnswer = false;
            atkPlus = gameSceneManager.happyEnergy.atkValue;
            speedPlus = gameSceneManager.happyEnergy.speedValue;

            if (atkPlus == 2)//����2�� = ����
            {
                gameSceneManager.unitSpawnController.ATKChange(atkPlus, true, false);
                gameSceneManager.unitSpawnController.SpeedChange(-speedPlus, true, false);
                atkPlus = 0;
                speedPlus = 0;
            }
            else //�Ͻ���
            {
                gameSceneManager.unitSpawnController.ATKChange(atkPlus, false, true);
                gameSceneManager.unitSpawnController.SpeedChange(-speedPlus, false, true);
            }
        }

        gameSceneManager.happyEnergy.HappyEnergyCheck();

        //gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.DeleteChange);
        //gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.DeleteChange);
        //
        //gameSceneManager.happyEnergy.HappyEnergyCheck();//�ӵ�, ���ݷ� nowChange true���� �ٲٴ°��߿� ���� �Ʒ��ο;���
        //
        //gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.FixChange,true);
        //gameSceneManager.unitController.PlusSpeed(0, Constants.PlusChangeType.NormalChange,true);
        //
        //gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.FixChange, true);
        //gameSceneManager.unitController.PlusATK(0, Constants.PlusChangeType.NormalChange, true);


        currentWave++;
        //currentStageWave++;
        if (GameManager.Instance.Stage == 1)
        {
            currentStageWave = currentWave;
        }
        else
        {
            currentStageWave = currentWave + 100;
        }

        // �Ѿ ���� Wave�� ���� ��
        if (currentWave <= maxWave)
        {
            // Wave ���Խ� ��� ����
            gameSceneManager.ChangeRuby(ruby);

            int tmpWave = currentWave % 10;
            int setTime = chapterDatabase.GetByKey(currentStageWave).Time;
            int daughterWave = chapterDatabase.GetByKey(currentStageWave).Message;

            UpdateWaveUI();

            // �� �˾� ȣ��
            if (daughterWave > 0)
            {
                happyEnergy.SetPopUp();
            }

            //timerUI.SetTimer(setTime);
            //enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);

            // ������ �Ϲ� Wave�� ��
            if (tmpWave != 0)
            {
                // 1 �������� BGM
                if (isBossWave && currentStageWave < 30)
                {
                    GameManager.Instance.SoundManager.BGMChange(2);
                }
                else if (isBossWave && currentStageWave > 30 && currentStageWave < 100)
                {
                    GameManager.Instance.SoundManager.BGMChange(3);
                }
                // 2 �������� BGM
                if (isBossWave && currentStageWave < 130)
                {
                    GameManager.Instance.SoundManager.BGMChange(9);
                }
                else if (isBossWave && currentStageWave > 130)
                {
                    GameManager.Instance.SoundManager.BGMChange(10);
                }

                isBossWave = false;

                // wave �ȳ� �˾�
                //StartCoroutine(wavePopUp.PopUp(currentWave.ToString() + " WAVE ����!", Color.yellow));

                int enemyCount = chapterDatabase.GetByKey(currentStageWave).EnemyCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(enemyCount, currentStageWave);
            }
            // ������ ���� Wave�� ��
            else if (tmpWave == 0/* && currentWave != 50*/)
            {
                // 1 �������� ���� BGM ���
                if (currentStageWave != 50 && currentStageWave < 100)
                {
                    GameManager.Instance.SoundManager.BGMChange(5);
                }
                else if (currentStageWave == 50)
                {
                    GameManager.Instance.SoundManager.BGMChange(4);
                }
                // 2 �������� ���� BGM ���
                else if (currentStageWave != 150 && currentStageWave > 100)
                {
                    GameManager.Instance.SoundManager.BGMChange(11);
                }
                else
                {
                    GameManager.Instance.SoundManager.BGMChange(12);
                }

                isBossWave = true;

                int bossCount = chapterDatabase.GetByKey(currentStageWave).BossCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(bossCount, currentStageWave, true);
            }
            // ������ 50 Wave�� ��
            //else
            //{
            //    isBossWave = true;

            //    int enemyCount = chapterDatabase.GetByKey(currentWave).EnemyCount;
            //    int bossCount = chapterDatabase.GetByKey(currentWave).BossCount;

            //    timerUI.SetTimer(setTime);
            //    enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);
            //    enemySpawn.RestartSpawnEnemy(bossCount, currentWave, true);

            //    GameManager.Instance.SoundManager.BGMChange(2);
            //}
        }
    }

    // wave UI ������Ʈ
    private void UpdateWaveUI()
    {
        waveText.text = "WAVE " + currentWave.ToString() + " / 50";
    }
}
