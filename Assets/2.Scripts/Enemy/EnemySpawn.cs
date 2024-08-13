using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    //[SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private GameObject gameoverPopup;  // ���� ���� UI
    [SerializeField] private GameObject clearUIPopup;   // ���� Ŭ���� UI

    public GameManager gameManager;
    public Player player;
    public DataManager dataManager;//�ӽ�//����
    public GameSceneManager gameSceneManager;//����
    private WaveUI waveUI;
    private LethalEnergy lethalEnergy;//����
    public DataTable_ChapterLoader chapterDatabase;
    private WavePopUp wavePopUp;
    //private PopUpController popUpController;
    private SoundManager soundManager;

    private List<Enemy> enemyList;
    //private int maxPerWave = 40;    // wave �� �ִ� ���� ��
    private int currentCount = 0;     // ���� wqve�� ��ȯ�� ���� ��
    //public int deadEnemyCount = 0;  // ó���� ���� ��
    private float waitSeconds = 0.3f;
    public bool isBossDead = false;
    private bool tooManyEnemy = false;  // ���� �� ��� â üũ

    EnemyMove enemyMove;

    public Image slider;

    private void Awake()
    {
        // ������ ������ �־���� ����Ʈ �Ҵ�
        enemyList = new List<Enemy>();

        waveUI = GetComponent<WaveUI>();
        //enemyMove = enemyPrefab.GetComponent<EnemyMove>();

        lethalEnergy = gameSceneManager.GetComponent<LethalEnergy>();
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            player = gameManager.Player;
            GameManager.Instance.EnemySpawn = this;
        }
        else
        {
            player = GameManager.Instance.Player;
        }
        
        //popUpController = gameManager.GetComponent<PopUpController>();
        chapterDatabase = dataManager.dataTable_ChapterLoader;
        wavePopUp = GetComponent<WavePopUp>();
        soundManager = GameManager.Instance.SoundManager;

        waveUI.Init();

        int maxPerWave = chapterDatabase.GetByKey(waveUI.currentWave).EnemyCount;
        StartCoroutine(SpawnEnemy(maxPerWave, 1));
    }

    // SpawnPoint���� ���� ����
    private IEnumerator SpawnEnemy(int maxPerWave, int waveNum, bool isBoss = false)
    {
        //int maxPerWave = chapterDatabase.GetByKey(waveUI.currentWave).EnemyCount;

        // �̹� Wave�� ���� �����Ǿ�� �� ������ �ִٸ�
        while (currentCount < maxPerWave)
        {
            CreateEnemy(waveNum, isBoss);

            yield return new WaitForSeconds(waitSeconds);
        }
    }

    // ������ �����Ѵ�.
    private void CreateEnemy(int waveNum, bool isBoss = false)
    {
        // ********** TODO : ������Ʈ Ǯ�� ����� ���� �����Ǿ�� ��! ������ �켱 ������Ʈ�� �����ϴ� �����. **********
        // ������ ���� ������ �޾ƿ��� �Ϳ� ���ؼ��� ���� �� ��
        //Instantiate(enemyPrefab, wayPoints[0].position, Quaternion.identity);
        //currentCount++;
        //enemyList.Add(enemyPrefab);

        //enemyMove = enemyPrefab.GetComponent<EnemyMove>();
        //enemyMove.Init(wayPoints);

        int chapterID = waveNum;
        int enemyID = chapterDatabase.GetByKey(chapterID).SpawnEnemy;
        int bossID = chapterDatabase.GetByKey(chapterID).SpawnBoss;

        GameObject clone = Instantiate(enemyPrefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = clone.GetComponent<Enemy>();

        if (!isBoss)
        {
            if (enemyID > -1)
            {
                enemy.Init(enemyID, chapterID, gameSceneManager, dataManager);//����
                enemyList.Add(enemy);
                currentCount++;
            }
        }
        else
        {
            if (bossID > -1)
            {
                // ���� ���� �˾�
                //StartCoroutine(wavePopUp.PopUp("���� ����", Color.red));

                enemy.Init(bossID, chapterID, gameSceneManager, dataManager);
                currentCount++;

                // ���� ���� ���� ����Ʈ
                soundManager.EffectAudioClipPlay(3);

                isBossDead = false;
            }
        }

        enemyMove = enemy.enemyMove;

        enemyMove.Init(enemyID, bossID, wayPoints, isBoss);

        // ������ 100���� �̻��� �� ���� ����
        if (enemyList.Count >= 100)
        {
            GameOver();
        }
        // ������ 70���� �̻��� �Ǿ��� �� ���â �˾�
        else if (enemyList.Count >= 70 && tooManyEnemy == false)
        {
            tooManyEnemy = true;
            StartCoroutine(wavePopUp.PopUp("������ ���� �ʹ� �����ϴ�!!", Color.red));
        }
        // ������ 70���� ���ϰ� �Ǹ�
        else if (enemyList.Count < 70 && tooManyEnemy)
        {
            tooManyEnemy = false;
        }

        UpdateEnemyCountUI();
    }

    // ���� ���� �ڷ�ƾ �����
    public void RestartSpawnEnemy(int enemyCount, int waveNum, bool isBoss = false)
    {
        currentCount = 0;
        //int enemyCount = chapterDatabase.GetByKey(waveNum).EnemyCount;

        StartCoroutine(SpawnEnemy(enemyCount, waveNum, isBoss));
    }

    // ***�ӽ�*** ������ �׾��� ��
    public void EnemyDie(Enemy enemy, GameObject gameObject)
    {
        // ***** TODO : ���� �׾��� �� ó�� ��� �����ϱ� *****
        //currentCount--;
        int exp;

        // ó���� ������ ��������, �������� Ȯ��
        if(enemy.isBoss)
        {
            isBossDead = true;

            // ���� Ŭ���� �˾�
            //StartCoroutine(wavePopUp.PopUp("���� Ŭ����", Color.yellow));

            // 50 Wave�� �� �Ϲ� �����鵵 �� �׾��ٸ�
            if (enemyList.Count == 0 && waveUI.currentWave == 50)
            {
                GameClear();
            }

            exp = enemy.bossData.bossData.Exp;
            waveUI.NextWave();
        }
        else
        {
            //deadEnemyCount++;
            //player.ExpUp(deadEnemyCount);
            exp = enemy.enemyData.enemyData.Exp;
            enemyList.Remove(enemy);
            UpdateEnemyCountUI();
        }
        player.ExpUp(exp);

        Destroy(gameObject);
        lethalEnergy.ChangeEnergy(1);

        int maxPerWave = chapterDatabase.GetByKey(waveUI.currentWave).EnemyCount;

        // �ش� Wave�� ��ȯ�� ������ �� �׾��� ��
        if (currentCount >= maxPerWave && enemyList.Count == 0)
        {
            // ������ ���������� �������� �� ����� ���
            if(isBossDead && waveUI.currentWave == 50)
            {
                GameClear();
            }
            else if (waveUI.isBossWave == false)
            {
                waveUI.NextWave();
            }
        }
    }

    // ���� �� UI ������Ʈ
    private void UpdateEnemyCountUI()
    {
        enemyCountText.text = enemyList.Count.ToString() + " / 100";
        slider.fillAmount = enemyList.Count / 100f;
    }

    // ���� ����
    public void GameOver()
    {
        // ���� ����Ʈ
        soundManager.EffectAudioClipPlay(5);
        GameManager.Instance.PopUpController.UIOnNPause(gameoverPopup);
    }
    
    // ���� Ŭ����
    private void GameClear()
    {
        // ���� ����Ʈ
        soundManager.EffectAudioClipPlay(4);

        GameManager.Instance.PopUpController.UIOnNPause(clearUIPopup);
    }

    public void LethalAttack()
    {
        int temp = enemyList.Count;
        for (int i = 0; i < temp; i++) // ����ã�� �����ϸ鼭 �ε��� �������� �׷�
        {
            Enemy enemy = enemyList[0];
            enemy.SpecialAttacked();
            Debug.Log("�ʻ�� ��� " + i);
        }
    }
}
