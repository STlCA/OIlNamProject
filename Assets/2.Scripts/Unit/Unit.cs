using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IPointerClickHandler
{
    public UnitController controller; // 나중에private
    private UnitAnimation unitAnimation;

    [Header("Upgrade")]
    public GameObject btnUI;
    public Image nonClickImage;
    public List<Image> iconImage;

    [Header("Skill")]
    public GameObject skillGO;

    public int id;
    public float range = 0;
    public float speed = 0;
    private float time = 0;
    private int step = 0;
    private CircleCollider2D rangeCollider;

    private List<Enemy> enemyList = new();

    private void Start()
    {
        if (GameManager.Instance != null)
            controller = GameManager.Instance.UnitController;

        unitAnimation = GetComponent<UnitAnimation>();

        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.radius = range;
    }

    private void Update()
    {
        if (time <= speed)
            time += Time.deltaTime;
        }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (step == 3)
            return;

        UIOnOff(btnUI);
    }

    public void UIOnOff(GameObject ui)
    {
        if (ui == null)
            return;

        if (ui.activeSelf == true)
            ui.SetActive(false);
        else
        {
            if (controller.CanUpgradeCheck(id))
                nonClickImage.gameObject.SetActive(false);
            else
                nonClickImage.gameObject.SetActive(true);

            ui.SetActive(true);
        }
    }

    public void UIOff()
    {
        btnUI.gameObject.SetActive(false);
    }

    public void UnitUpgrade()
    {
        controller.UnitUpgrade(id, transform.position);
        iconImage[step].gameObject.SetActive(true);

        id++;
        step++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");

        Enemy monster = collision.GetComponent<Enemy>();

        if (monster != null)
        {
            enemyList.Add(monster);

            if (time >= speed)
            {
                Debug.Log("몹");
                time = 0;

                //skillGO.transform.position = monster.transform.position;                
                unitAnimation.AttackEffect();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy monster = collision.GetComponent<Enemy>();

        if (monster != null)
            enemyList.Remove(monster);
    }

    public void Attack()//animation에서 호출하기
    {
        Debug.Log("Attack호출됨");
        //Enemy enemy = FindEnemy();
        //enemy.Attack();
    }

    private Enemy FindEnemy()
    {
        if (enemyList.Count == 0)
            return null;
        if (enemyList.Count == 1)
            return enemyList[0];

        Enemy enemy = null;

        float min = float.MaxValue;
        float current = 0;

        foreach (Enemy monster in enemyList)
        {
            current = Vector3.Distance(transform.position, monster.transform.position);

            if (min > current)
            {
                enemy = monster;
                min = current;
            }
        }

        return enemy;
    }
}
