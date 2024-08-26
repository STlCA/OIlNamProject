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

    // ���� �ʱ�ȭ
    public void Init(int enemyID, int bossID, Transform[] _wayPoints, bool isBoss = false)
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponentInChildren<Animator>();

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

        // ��������Ʈ �Ҵ�
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

    // ������ ������
    private void Move()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    // ������ ���� WayPoint
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

    // ������ ���� WayPoint ����
    private void NextWayPoint()
    {
        // ���� wayPoint�� ���� ��
        if (currentIndex < wayPointCount - 1)
        {
            this.transform.position = wayPoints[currentIndex].position;
            currentIndex++;
        }
        // ������ wayPoint�� �������� ��
        else
        {
            this.transform.position = wayPoints[currentIndex].position;
            currentIndex = 0;
        }

        //direction = (wayPoints[currentIndex].position - this.transform.position).normalized;
        //MoveDirection(direction);
        MoveDirection();
    }

    // ������ ������ �ִϸ��̼� ���� ����
    private void MoveDirection(/*Vector3 _direction*/)
    {
        //direction = _direction;
        direction = (wayPoints[currentIndex].position - this.transform.position).normalized;

        // ������
        if (direction.x > 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Right);
        }
        // ����
        else if (direction.x < 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Left);
        }
        // ��
        else if (direction.y > 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Up);
        }
        //�Ʒ�
        else if (direction.y < 0)
        {
            animator.SetInteger("AnimState", (int)MoveStates.Down);
        }
    }
}
