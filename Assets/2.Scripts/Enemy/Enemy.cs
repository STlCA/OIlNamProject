using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class EnemyData
{
    public DataTable_Enemy enemyData;
    public DataTable_Chapter chapterData;
    public int maxHP;
    public int hp;
    public Sprite sprite;
    public RuntimeAnimatorController animator;

    public EnemyData(DataTable_Enemy dataE, DataTable_Chapter dataC)
    {
        enemyData = dataE;
        chapterData = dataC;

        hp = chapterData.EnemyHP;
        maxHP = chapterData.EnemyHP;
        sprite = Resources.Load<Sprite>(dataE.Path);
        animator = Resources.Load<RuntimeAnimatorController>(dataE.Anim);
    }
}

public class BossData
{
    public DataTable_Boss bossData;
    public DataTable_Chapter chapterData;
    public int maxHP;
    public int hp;
    public Sprite sprite;
    public RuntimeAnimatorController animator;

    public BossData(DataTable_Boss data, DataTable_Chapter dataC)
    {
        bossData = data;
        chapterData = dataC;

        hp = chapterData.BossHP;
        maxHP = chapterData.BossHP;
        sprite = Resources.Load<Sprite>(data.Path);
        animator = Resources.Load<RuntimeAnimatorController>(data.Anim);
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
    private GameSceneManager gameSceneManager;//����
    private EnemySpawn enemySpawn;
    //private EnemyHPBar enemyHPBar;

    [SerializeField] DamageNumber damagePrefab;

    // ���� ����
    //public DataTable_Enemy enemyData;
    public EnemyData enemyData;
    public BossData bossData;
    private SpriteRenderer image;
    //private bool isDead;
    public bool isBoss = false;
    [SerializeField] private Slider enemyHpSlider;
    //[SerializeField] private GameObject enemyHpSliderUI;
    //private Slider enemyHpSlider;

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

    // ���� ���� �ʱ�ȭ
    public void Init(int enemyID, int chapterID, GameSceneManager GSM, DataManager DM = null)//����
    {
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            dataManager = gameManager.DataManager;
            enemyDatabase = dataManager.dataTable_EnemyLoader;
            bossDatabase = dataManager.dataTable_BossLoader;
            chapterDatabase = dataManager.dataTable_ChapterLoader;
            enemySpawn = gameManager.EnemySpawn;
            gameSceneManager = GSM;//����
        }
        else
        {
            dataManager = DM;
            gameSceneManager = GSM;//����
            enemyDatabase = dataManager.dataTable_EnemyLoader;
            bossDatabase = dataManager.dataTable_BossLoader;
            chapterDatabase = dataManager.dataTable_ChapterLoader;
            enemySpawn = gameManager.EnemySpawn;
        }

        // Script
        enemyMove = GetComponent<EnemyMove>();
        image = GetComponent<SpriteRenderer>();
        //enemyHPBar = enemyHpSliderUI.GetComponent<EnemyHPBar>();
        //gameManager = GameManager.Instance;
        //dataManager = gameManager.DataManager;
        //this.enemyDatabase = dataManager.dataTable_EnemyLoader;

        // 
        //enemyData = enemyDatabase.GetByKey(id);

        // ��ȯ�� ������ ������ ���
        if (enemyID > 500)
        {
            bossData = new BossData(bossDatabase.GetByKey(enemyID), chapterDatabase.GetByKey(chapterID));
            image.sprite = bossData.sprite;
            // ���� ���� ũ�� ����
            //transform.localScale = new Vector3(0.65f, 0.65f);
            transform.localScale *= 1.2f;
            // ���� ���Ͱ� �Ϲ� ���ͺ��� �տ� ���̰�
            image.sortingOrder++;
            isBoss = true;
        }
        // �Ϲ� ������ ���
        else
        {
            enemyData = new EnemyData(enemyDatabase.GetByKey(enemyID), chapterDatabase.GetByKey(chapterID));
            image.sprite = enemyData.sprite;
            transform.localScale *= 0.7f;
        }
    }

    // ���� Ȱ��ȭ
    private void Activate()
    {
        //Init();
        //isDead = false;
        this.gameObject.SetActive(true);
    }

    // ���� ��Ȱ��ȭ
    private void Disable()
    {
        //isDead = true;
        this.gameObject.SetActive(false);
        gameSceneManager.ChangeRuby(2);//����
    }

    // ������ ���� �޾��� ��
    public void EnemyAttacked(float damage)
    {
        float hp;
        float maxHP;
        int ruby;

        // ������ ���
        if (isBoss)
        {
            //gameManager.SoundManager.EffectAudioClipPlay(19,0.1f);

            hp = bossData.hp;
            maxHP = bossData.maxHP;
            ruby = bossData.bossData.PlayGoods;
            hp -= damage;
            bossData.hp = (int)hp;
        }
        // �Ϲ� ������ ���
        else
        {
            //gameManager.SoundManager.EffectAudioClipPlay(20, 0.1f);
            hp = enemyData.hp;
            maxHP = enemyData.maxHP;
            ruby = enemyData.enemyData.PlayGoods;
            hp -= damage;
            enemyData.hp = (int)hp;
        }
        //hp -= damage;

        // ���� hp��
        enemyHpSlider.value = hp / maxHP;
        //enemyHPBar.UpdateHPBar();

        // ������
        AttackedEffect(damage);
        //DamageNumber damageNumber = damagePrefab.Spawn(transform.position, damage);
        //image.color = Color.red;

        // ���� �׾��� ��
        if (hp <= 0)
        {
            hp = 0;

            //Disable();
            //*** TODO : (�ӽù������� ������Ʈ ���� ����->) �Ŀ� �Ʒ� �ڵ�� ��Ȱ��ȭ�ϰ� �� �� ���� Disable() Ȱ��ȭ ***
            gameSceneManager.ChangeRuby(ruby);//����
            enemySpawn.EnemyDie(this, gameObject);
        }
    }

    // ������ �ǰ� �ִϸ��̼� �� ȿ��
    private void AttackedEffect(float damage)
    {
        DamageNumber damageNumber = damagePrefab.Spawn(transform.position, damage);
        StartCoroutine(RedEffect());
    }

    // ���� �ǰ� ȿ��
    public IEnumerator RedEffect()
    {
        image.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        image.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        image.color = Color.white;
    }

    // ������ �ʻ�⿡ �¾��� �� (���� ����)
    public void SpecialAttacked()
    {
        // ������ ��� ����
        if (isBoss)
        {
            return;
        }

        enemyData.hp = 0;
        gameSceneManager.ChangeRuby(enemyData.enemyData.PlayGoods);//����
        enemySpawn.EnemyDie(this, gameObject);
    }
}
