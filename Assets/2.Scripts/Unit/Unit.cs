using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UnitData
{
    public int id;
    public float range;
    public float speed;
    public int step;

    public float fixSpeedData;//바꾸지x
    public float fixSpeed;
    public float currentSpeed;

    public float fixAtk;
    public float currentAtk;

    public void Init(int id, float range, float speed, float atk, int step = 0)
    {
        this.id = id;
        this.range = range;
        fixSpeedData = speed;
        this.speed = speed;
        fixAtk = atk;

        if (step == 0)
            step = 0;
        this.step += step;

        currentAtk = fixAtk;
    }

    public void SpeedChange(int changeVal, bool isFixChange = false)//value는 총 계산된 퍼센트
    {
        if (changeVal == 0)
        {
            if (isFixChange)
                fixSpeed = fixSpeedData;
            else
                speed = fixSpeed;

            return;
        }

        if (isFixChange)
            fixSpeed = fixSpeedData + fixSpeedData / 100 * changeVal;
        else
            speed = fixSpeed + fixSpeed / 100 * changeVal;        
    }

    public void ATKChange(int changeVal, bool isFixChange = false)
    {
        if (changeVal == 0)
            return;

        if (isFixChange)
            fixAtk += fixAtk / 100 * changeVal;
        else
            currentAtk += currentAtk / 100 * changeVal;
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
        myData.speed += Time.deltaTime;


        if (myData.speed >= myData.currentSpeed)
        {

            findEnemy = FindEnemy();
            if (findEnemy != null)
            {
                Debug.Log("몹");

                unitAnimation.AttackEffect();
                myData.speed = 0;
            }
        }

        Debug.Log(enemyList.Count);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (controller.onUnitPopUP.Count != 0)
        {
            controller.onUnitPopUP[0].SetActive(false);
            controller.onUnitPopUP[1].SetActive(false);
            controller.onUnitPopUP.Clear();
        }

        if (myData.step == 2)
            return;

        UIOnOff();
    }

    public void UIOnOff()
    {
        Debug.Log("여긴들어옴");

        if (btnUI == null)
            return;

/*        controller.onUnitPopUP.Add(btnUI);
        controller.onUnitPopUP.Add(rangeGO);*/

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

    public void SellUnit()
    {

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
