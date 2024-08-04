using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class EnemyData
{
    public DataTable_Enemy enemyData;
    public DataTable_Chapter chapterData;
    public int hp;
    public Sprite sprite;

    public EnemyData(DataTable_Enemy dataE, DataTable_Chapter dataC)
    {
        enemyData = dataE;
        chapterData = dataC;

        hp = chapterData.EnemyHP;
        sprite = Resources.Load<Sprite>(dataE.Path);
    }
}

public class BossData
{
    public DataTable_Boss bossData;
    public DataTable_Chapter chapterData;
    public int hp;
    public Sprite sprite;

    public BossData(DataTable_Boss data, DataTable_Chapter dataC)
    {
        bossData = data;
        chapterData = dataC;

        hp = chapterData.BossHP;
        sprite = Resources.Load<Sprite>(data.Path);
    }
}

public class Enemy : MonoBehaviour
{
    // Script
    public EnemyMove enemyMove;
    private GameManager gameManager;
    private DataManager dataManager;
    private DataTable_EnemyLoader enemyDatabase;
    private DataTable_BossLoader bossDatabase;
    private DataTable_ChapterLoader chapterDatabase;
    private GameSceneManager gameSceneManager;//수정
    private EnemySpawn enemySpawn;    

    // 마물 정보
    //public DataTable_Enemy enemyData;
    public EnemyData enemyData;
    public BossData bossData;
    private SpriteRenderer image;
    private bool isDead;
    public bool isBoss = false;

    //private void Start()
    //{
    //    if (GameManager.Instance != null)
    //    {
    //        enemyDatabase = GameManager.Instance.DataManager.dataTable_EnemyLoader;
    //    }
    //    else
    //    {
    //        enemyDatabase = dataManager.dataTable_EnemyLoader;
    //    }
    //}

    // 마물 설정 초기화
    public void Init(int enemyID, int chapterID, GameSceneManager GSM, DataManager DM = null)//수정
    {
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            dataManager = gameManager.DataManager;
            enemyDatabase = dataManager.dataTable_EnemyLoader;
            bossDatabase = dataManager.dataTable_BossLoader;
            chapterDatabase = dataManager.dataTable_ChapterLoader;
            enemySpawn = gameManager.EnemySpawn;
            gameSceneManager = GSM;//수정
        }
        else
        {
            dataManager = DM;
            gameSceneManager = GSM;//수정
            enemyDatabase = dataManager.dataTable_EnemyLoader;
            bossDatabase = dataManager.dataTable_BossLoader;
            chapterDatabase = dataManager.dataTable_ChapterLoader;
            enemySpawn = gameManager.EnemySpawn;
        }

        // Script
        enemyMove = GetComponent<EnemyMove>();
        image = GetComponent<SpriteRenderer>();
        //gameManager = GameManager.Instance;
        //dataManager = gameManager.DataManager;
        //this.enemyDatabase = dataManager.dataTable_EnemyLoader;

        // 
        //enemyData = enemyDatabase.GetByKey(id);

        // 소환된 마물이 보스인 경우
        if (enemyID > 500)
        {
            bossData = new BossData(bossDatabase.GetByKey(enemyID), chapterDatabase.GetByKey(chapterID));
            image.sprite = bossData.sprite;
            // 보스 몬스터 크기 조절
            //transform.localScale = new Vector3(0.65f, 0.65f);
            transform.localScale *= 1.2f;
            // 보스 몬스터가 일반 몬스터보다 앞에 보이게
            image.sortingOrder += 5;
            isBoss = true;
        }
        // 일반 마물인 경우
        else
        {
            enemyData = new EnemyData(enemyDatabase.GetByKey(enemyID), chapterDatabase.GetByKey(chapterID));
            image.sprite = enemyData.sprite;
            transform.localScale *= 0.8f;
        }
    }

    // 마물 활성화
    private void Activate()
    {
        //Init();
        isDead = false;
        this.gameObject.SetActive(true);
    }

    // 마물 비활성화
    private void Disable()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        gameSceneManager.ChangeRuby(2);//수정
    }

    // 마물이 공격 받았을 때
    public void EnemyAttacked(float damage)
    {
        float hp;
        int ruby;

        // 보스인 경우
        if (isBoss)
        {
            hp = bossData.hp;
            ruby = bossData.bossData.PlayGoods;
            hp -= damage;
            bossData.hp = (int)hp;
        }
        // 일반 마물일 경우
        else
        {
            hp = enemyData.hp;
            ruby = enemyData.enemyData.PlayGoods;
            hp -= damage;
            enemyData.hp = (int)hp;
        }
        //hp -= damage;

        // 적이 죽었을 때
        if (hp <= 0)
        {
            hp = 0;

            //Disable();
            //*** TODO : (임시방편으로 오브젝트 삭제 만듬->) 후에 아래 코드들 비활성화하고 난 뒤 위의 Disable() 활성화 ***
            gameSceneManager.ChangeRuby(ruby);//수정
            enemySpawn.EnemyDie(this, gameObject);
        }
    }

    // 마물이 필살기에 맞았을 때 (보스 제외)
    public void SpecialAttacked()
    {
        // 보스일 경우 리턴
        if (isBoss)
        {
            return;
        }

        enemyData.hp = 0;
        gameSceneManager.ChangeRuby(enemyData.enemyData.PlayGoods);//수정
        enemySpawn.EnemyDie(this, gameObject);
    }
}
