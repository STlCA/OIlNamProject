using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Script
    public EnemyMove enemyMove;
    private GameManager gameManager;
    private DataManager dataManager;
    private DataTable_EnemyLoader enemyDatabase;
    private GameSceneManager gameSceneManager;//수정

    private DataTable_Enemy enemy;
    private SpriteRenderer image;
    private bool isDead;

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
    public void Init(int id, GameSceneManager GSM, DataManager DM = null/*, DataTable_EnemyLoader enemyDatabase*/)//수정
    {
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            dataManager = gameManager.DataManager;
            enemyDatabase = dataManager.dataTable_EnemyLoader;
            gameSceneManager = GSM;//수정
        }
        else
        {
            dataManager = DM;
            gameSceneManager = GSM;//수정
            enemyDatabase = dataManager.dataTable_EnemyLoader;
        }

        // Script
        enemyMove = GetComponent<EnemyMove>();
        //gameManager = GameManager.Instance;
        //dataManager = gameManager.DataManager;
        //this.enemyDatabase = dataManager.dataTable_EnemyLoader;

        // 
        enemy = enemyDatabase.GetEnemyByKey(id);
        image = GetComponent<SpriteRenderer>();
        image.sprite = enemy.sprite;
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
        gameSceneManager.ChangeGold(2);//수정
    }

    // 마물이 공격 받았을 때
    public void EnemyAttacked(float damage)
    {
        float hp = enemy.HP;
        hp -= damage;

        // 적이 죽었을 때
        if (hp <= 0)
        {
            hp = 0;

            Disable();
        }
    }
}
