using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;

    [SerializeField] private Transform[] wayPoints;
    private int wayPointCount;
    private int currentIndex;

    private Enemy enemy;

    private void Awake()
    {

    }

    // 마물 초기화
    public void Init(int enemyID, int bossID, Transform[] _wayPoints, bool isBoss = false)
    {
        enemy = GetComponent<Enemy>();
        if (!isBoss)
        {
            if (enemyID > -1)
            {
                speed = enemy.enemyData.enemyData.Speed;
            }
        }
        else
        {
            speed = enemy.bossData.bossData.Speed;
        }
        
        currentIndex = 0;

        // 웨이포인트 할당
        wayPointCount = _wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = _wayPoints;

        this.transform.position = wayPoints[currentIndex].position;

        StartCoroutine(MoveWayPoint());
    }

    private void Update()
    {
        Move();
    }

    // 마물의 움직임
    private void Move()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    // 마물이 향할 WayPoint
    private IEnumerator MoveWayPoint()
    {
        NextWayPoint();

        while (true)
        {
            if (Vector3.Distance(this.transform.position, wayPoints[currentIndex].position) < 0.05f * speed)
            {
                NextWayPoint();
            }

            yield return null;
        }
    }

    // 마물의 다음 WayPoint 설정
    private void NextWayPoint()
    {
        // 다음 wayPoint가 있을 때
        if (currentIndex < wayPointCount - 1)
        {
            this.transform.position = wayPoints[currentIndex].position;
            currentIndex++;
        }
        // 마지막 wayPoint에 도착했을 때
        else
        {
            this.transform.position = wayPoints[currentIndex].position;
            currentIndex = 0;
        }

        direction = (wayPoints[currentIndex].position - this.transform.position).normalized;
        MoveDirection(direction);
    }

    // 마물의 움직임 방향 설정
    private void MoveDirection(Vector3 _direction)
    {
        direction = _direction;
    }
}
