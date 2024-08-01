using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    //[SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private GameObject gameoverPopup;  // 게임 오버 UI
    [SerializeField] private GameObject clearUIPopup;   // 게임 클리어 UI

    public GameManager gameManager;
    public Player player;
    public DataManager dataManager;//임시//수정
    public GameSceneManager gameSceneManager;//수정
    private WaveUI waveUI;
    private LethalEnergy lethalEnergy;//수정
    public DataTable_ChapterLoader chapterDatabase;
    //private PopUpController popUpController;

    private List<Enemy> enemyList;
    //private int maxPerWave = 40;    // wave 당 최대 마물 수
    private int currentCount = 0;
    public int deadEnemyCount = 0;  // 처리한 마물 수

    EnemyMove enemyMove;

    private void Awake()
    {
        // 생성된 마물을 넣어놓을 리스트 할당
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

        waveUI.Init();

        int enemyCount = chapterDatabase.GetByKey(waveUI.currentWave).EnemyCount;
        StartCoroutine(SpawnEnemy(enemyCount, 1));
    }

    // SpawnPoint에서 마물 생성
    private IEnumerator SpawnEnemy(int maxPerWave, int waveNum)
    {
        //int maxPerWave = chapterDatabase.GetByKey(waveUI.currentWave).EnemyCount;

        // 이번 Wave에 아직 생성되어야 할 마물이 있다면
        while (currentCount < maxPerWave)
        {
            CreateEnemy(waveNum);

            yield return new WaitForSeconds(0.6f);
        }
    }

    // 마물을 생성한다.
    private void CreateEnemy(int waveNum)
    {
        // ********** TODO : 오브젝트 풀링 사용을 위해 수정되어야 함! 지금은 우선 오브젝트를 생성하는 방법임. **********
        // 마물에 대한 정보를 받아오는 것에 대해서도 생각 할 것
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

        if (enemyID > -1)
        {
            enemy.Init(enemyID, chapterID, gameSceneManager, dataManager);//수정
            enemyList.Add(enemy);
            currentCount++;
        }
        if (bossID > -1)
        {
            enemy.Init(bossID, chapterID, gameSceneManager, dataManager);
        }

        enemyMove = enemy.enemyMove;

        enemyMove.Init(enemyID, bossID, wayPoints);

        if (enemyList.Count >= 100)
        {
            GameOver();
        }

        UpdateEnemyCountUI();
    }

    // 마물 생성 코루틴 재시작
    public void RestartSpawnEnemy(int enemyCount, int waveNum)
    {
        currentCount = 0;
        //int enemyCount = chapterDatabase.GetByKey(waveNum).EnemyCount;

        StartCoroutine(SpawnEnemy(enemyCount, waveNum));
    }

    // ***임시*** 마물이 죽었을 때
    public void EnemyDie(Enemy enemy, GameObject gameObject)
    {
        // ***** TODO : 마물 죽었을 때 처리 방법 수정하기 *****
        //currentCount--;
        deadEnemyCount++;
        player.ExpUp(deadEnemyCount);
        enemyList.Remove(enemy);
        Destroy(gameObject);
        UpdateEnemyCountUI();
        lethalEnergy.ChangeEnergy(1);

        // 소환된 마물이 다 죽었을 때
        if (enemyList.Count == 0)
        {
            waveUI.NextWave();
        }
    }

    // 마물 수 UI 업데이트
    private void UpdateEnemyCountUI()
    {
        enemyCountText.text = enemyList.Count.ToString() + " / 100";
    }

    // 마물이 100마리가 넘어가면 게임 오버
    private void GameOver()
    {
        GameManager.Instance.PopUpController.PauseUIOn(gameoverPopup);

    }
    
    // 게임 클리어
    private void GameClear()
    {
        GameManager.Instance.PopUpController.PauseUIOn(clearUIPopup);
    }
}
