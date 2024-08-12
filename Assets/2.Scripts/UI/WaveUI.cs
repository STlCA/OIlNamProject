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

    private int plus;//����
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

        StartWave();
    }

    // Wave ����
    private void StartWave()
    {
        timerUI.Init();
        UpdateWaveUI();
    }

    // ���� Wave�� ����
    public void NextWave()
    {
        int ruby = chapterDatabase.GetByKey(currentWave).Gold;

        if(plus > 0)
        {
            gameSceneManager.unitSpawnController.ATKChange(-plus,false,true);
            plus = 0;
        }

        if (gameSceneManager.happyEnergy.isAnswer)
        {
            gameSceneManager.happyEnergy.isAnswer = false;
            plus = gameSceneManager.happyEnergy.value;

            if (plus == 2)
            {
                gameSceneManager.unitSpawnController.ATKChange(plus, true,true);
                plus = 0;
            }
            else
            {
                gameSceneManager.unitSpawnController.ATKChange(plus,false,true);
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

        // �Ѿ ���� Wave�� ���� ��
        if (currentWave <= maxWave)
        {
            // Wave ���Խ� ��� ����
            gameSceneManager.ChangeRuby(ruby);

            int tmpWave = currentWave % 10;
            int setTime = chapterDatabase.GetByKey(currentWave).Time;
            int daughterWave = chapterDatabase.GetByKey(currentWave).Message;

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
                if (isBossWave && currentWave < 30)
                {
                    GameManager.Instance.SoundManager.BGMChange(2);
                }
                else if (isBossWave && currentWave > 30)
                {
                    GameManager.Instance.SoundManager.BGMChange(3);
                }

                isBossWave = false;

                // wave �ȳ� �˾�
                //StartCoroutine(wavePopUp.PopUp(currentWave.ToString() + " WAVE ����!", Color.yellow));

                int enemyCount = chapterDatabase.GetByKey(currentWave).EnemyCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(enemyCount, currentWave);
            }
            // ������ ���� Wave�� ��
            else if (tmpWave == 0/* && currentWave != 50*/)
            {
                // ���� BGM ���
                if (currentWave != 50)
                {
                    GameManager.Instance.SoundManager.BGMChange(4);
                }
                else
                {
                    GameManager.Instance.SoundManager.BGMChange(5);
                }

                isBossWave = true;

                int bossCount = chapterDatabase.GetByKey(currentWave).BossCount;

                timerUI.SetTimer(setTime);
                enemySpawn.RestartSpawnEnemy(bossCount, currentWave, true);
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
        waveText.text = "WAVE " + currentWave.ToString();
    }
}
