using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    enum MoveStates
    {
        Left = 1,
        Right = 2, 
        Up = 3,
        Down = 4
    }

    [SerializeField] private float speed;
    private Vector3 direction;

    [SerializeField] private Transform[] wayPoints;
    private int wayPointCount;
    private int currentIndex;

    private Enemy enemy;
    private Animator animator;

    // 마물 초기화
    public void Init(int enemyID, int bossID, Transform[] _wayPoints, bool isBoss = false)
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        if (!isBoss)
        {
            if (enemyID > -1)
            {
                speed = enemy.enemyData.enemyData.Speed;
                animator.runtimeAnimatorController = enemy.enemyData.animator;
            }
        }
        else
        {
            speed = enemy.bossData.bossData.Speed;
            animator.runtimeAnimatorController = enemy.bossData.animator;
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
            if (Vector3.Distance(this.transform.position, wayPoints[currentIndex].position) < speed * Time.deltaTime * 2)
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

        //direction = (wayPoints[currentIndex].position - this.transform.position).normalized;
        //MoveDirection(direction);
        MoveDirection();
    }

    // 마물의 움직임 애니메이션 방향 설정
    private void MoveDirection(/*Vector3 _direction*/)
    {
        //direction = _direction;
        direction = (wayPoints[currentIndex].position - this.transform.position).normalized;

        // 오른쪽
        if (direction.x > 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Right);
        }
        // 왼쪽
        else if (direction.x < 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Left);
        }
        // 위
        else if (direction.y > 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Up);
        }
        //아래
        else if (direction.y < 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Down);
        }
    }
}
