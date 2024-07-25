using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    //[SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private List<Enemy> enemyList;
    private int wavePerMax = 30;
    private int currentCount = 0;

    EnemyMove enemyMove;

    private void Awake()
    {
        // 생성된 마물을 넣어놓을 리스트 할당
        enemyList = new List<Enemy>();

        //enemyMove = enemyPrefab.GetComponent<EnemyMove>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // SpawnPoint에서 마물 생성
    private IEnumerator SpawnEnemy()
    {
        // 이번 Wave에 아직 생성되어야 할 마물이 있다면
        while (currentCount <= wavePerMax)
        {
            CreateEnemy();

            yield return new WaitForSeconds(2.0f);
        }

        currentCount = 0;
    }

    // 마물을 생성한다.
    private void CreateEnemy()
    {
        // ********** TODO : 오브젝트 풀링 사용을 위해 수정되어야 함! 지금은 우선 오브젝트를 생성하는 방법임. **********
        // 마물에 대한 정보를 받아오는 것에 대해서도 생각 할 것
        //Instantiate(enemyPrefab, wayPoints[0].position, Quaternion.identity);
        //currentCount++;
        //enemyList.Add(enemyPrefab);

        //enemyMove = enemyPrefab.GetComponent<EnemyMove>();
        //enemyMove.Init(wayPoints);

        GameObject clone = Instantiate(enemyPrefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.Init(1);
        enemyMove = enemy.enemyMove;

        enemyMove.Init(wayPoints);
        enemyList.Add(enemy);
    }
}
