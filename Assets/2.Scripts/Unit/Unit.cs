using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UnitData
{
    public int id;
    public float range;
    public float fixSpeed;
    public float currentSpeed;
    public float time;
    public int step;
    public float fixAtk;
    public float currentAtk;
    public float plusAtk;    

    public void Init(int id, float range, float speed, float atk, int step = 0)
    {
        this.id = id;
        this.range = range;
        fixSpeed = speed;
        fixAtk = atk;
        time = speed;

        if (step == 0)
            step = 0;
        this.step += step;

        currentSpeed = fixSpeed;
        currentAtk = fixAtk;
    }
    public void SpeedChange(int changeVal)
    {
        if (changeVal == 0)
        {
            currentSpeed = fixSpeed;
            return;
        }

        currentSpeed += fixSpeed / 100 * changeVal;
    }
    public void ATKChange(int changeVal, bool isFixChange = false)//fix인애 fix아닌애 두번부르기 fix먼저 부르기
    {
        if (isFixChange)
        {
            fixAtk += fixAtk / 100 * changeVal;
            return;
        }

        if (changeVal == 0)
        {
            currentAtk = fixAtk;
            return;
        }

        currentAtk += fixAtk / 100 * changeVal;
    }
}

public class Unit : MonoBehaviour, IPointerClickHandler
{
    public UnitController controller; // 나중에private
    private UnitAnimation unitAnimation;

    [Header("Upgrade")]
    public GameObject btnUI;
    public Image nonClickImage;
    public List<Image> iconImage;

    [Header("Self")]
    public GameObject skillGO;
    public GameObject rangeGO;

    public UnitData myData;
    /*    public int id;
        public float range = 0;
        public float speed = 0;
        public float time = 0;
        private int step = 0;*/
    private CircleCollider2D rangeCollider;

    private List<Enemy> enemyList = new();
    private Enemy findEnemy;


    private void Start()
    {
        if (GameManager.Instance != null)
            controller = GameManager.Instance.UnitController;

        unitAnimation = GetComponent<UnitAnimation>();

        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.radius = myData.range;
        rangeGO.transform.localScale = new Vector3(myData.range, myData.range);
    }

    private void Update()
    {
        myData.time += Time.deltaTime;


        if (myData.time >= myData.currentSpeed)
        {

            findEnemy = FindEnemy();
            if (findEnemy != null)
            {
                Debug.Log("몹");

                unitAnimation.AttackEffect();
                myData.time = 0;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (myData.step == 3)
            return;

        UIOnOff();
    }

    public void UIOnOff()
    {
        if (btnUI == null)
            return;

        if (btnUI.activeSelf == true)
        {
            btnUI.SetActive(false);
            rangeGO.SetActive(false);
        }
        else
        {
            if (controller.CanUpgradeCheck(myData.id))
                nonClickImage.gameObject.SetActive(false);
            else
                nonClickImage.gameObject.SetActive(true);

            btnUI.SetActive(true);
            rangeGO.SetActive(true);
        }
    }

    public void UIOff()
    {
        btnUI.gameObject.SetActive(false);
        rangeGO.SetActive(false);
    }

    public void UnitUpgrade()
    {
        controller.UnitUpgrade(myData.id, transform.position);
        iconImage[myData.step - 1].gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");

        Enemy monster = collision.GetComponent<Enemy>();

        if (monster != null)
        {
            enemyList.Add(monster);
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
        if (findEnemy == null)
            return;

        skillGO.transform.position = findEnemy.transform.position;
        unitAnimation.AttackSkillEffect();//타이밍해결할수있으면 공격끝나고 호출
        Debug.Log("Attack호출됨");
        findEnemy.EnemyAttacked(myData.currentAtk);
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
