using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitGameData : MonoBehaviour, IPointerClickHandler
{
    private UnitSpawnController controller;
    private UnitAnimation unitAnimation;

    [Header("RangeCollider")]
    public CircleCollider2D rangeCollider;//직접스스로를 넣어주기
    public GameObject rangeGO;

    [Header("UI")]
    public GameObject unitCanvas;

    [Header("Self")]
    public GameObject skillGO;


    private List<Enemy> enemyList = new();
    private Enemy findEnemy;

    //----------------------------------------------------------------Data

    private UnitData myUnitData;
    private DataTable_UnitStep myStepData;

    public float Range
    {
        get { return range; }
        private set
        {
            range = value;
            rangeCollider.radius = range;
        }
    }
    public float range;

    public int Step // if(Step >= 2)//0 = 1성일때, 1 = 2성일때
    {
        get { return step; }
        private set
        {
            step = value;
            SellGold = myStepData.SellGold[step];
        }
    }
    private int step;
    public int SellGold { get; private set; }

    //------------speed
    public float speedData;//메인에서 강화된 유닛공격력--
    public float stepSpeed;//스탭별 공격력이 합쳐진 공격력 (250% = 2.5)--

    public float fixSpeedStack; // 누적공격력 증가량 (2% = 2)--
    public float fixSpeed; //누적공격력스택이 포함된 공격력

    public float speedStack; // 버프 공격력 증가량 (40% = 40)--
    public float speed; //버프 공격력스택이 포함된 공격력

    public float deltaSpeed;//delta더해서 speed와 비교하는용--

    //----------------atk
    public float atkData;//메인에서 강화된 유닛공격력--
    public float stepAtk;//스탭별 공격력이 합쳐진 공격력 (250% = 2.5)--

    public float fixAtkStack; // 누적공격력 증가량 (2% = 2)--
    public float fixAtk; //누적공격력스택이 포함된 공격력

    public float atkStack; // 버프 공격력 증가량 (40% = 40)--
    public float atk; //버프 공격력스택이 포함된 공격력

    //------------------------------------------------------------------------

    public void Init(UnitData unitData, DataTable_UnitStep stepData, UnitSpawnController controller)
    {
        this.controller = controller;

        myUnitData = unitData;
        myStepData = stepData;

        //대문자가맞음
        Range = unitData.range / 100;
        Step = 0;

        speedData = unitData.speed;
        fixSpeedStack = 0;
        speedStack = 0;
        SpeedChange();
        deltaSpeed = speed;

        atkData = unitData.atk;
        fixAtkStack = 0;
        atkStack = 0;
        ATKChange();

        unitAnimation = GetComponentInChildren<UnitAnimation>();
        unitAnimation.TypeSet(unitData.type);

        unitCanvas = transform.parent.GetChild(1).gameObject;
    }

    //스피드 스택 쌓을곳에서 부르기 = 버프
    public void SpeedStackChange(int changeVal, bool isFixChange = false)
    {
        if (isFixChange)
            fixSpeedStack += changeVal;
        else
            speedStack += changeVal;

        SpeedChange();
    }
    public void ATKStackChange(int changeVal, bool isFixChange = false)
    {
        if (isFixChange)
            fixAtkStack += changeVal;
        else
            atkStack += changeVal;

        ATKChange();
    }

    //합성 업그레이드
    public void UpgradeData()
    {
        step++;

        SpeedChange();
        ATKChange();

        //TODO : 아이콘바꾸기
    }

    private void SpeedChange()
    {
        stepSpeed = speedData * (myStepData.StepSpeed[step] / 100f);
        fixSpeed = stepSpeed + ((stepSpeed / 100) * fixSpeedStack);
        speed = fixSpeed + ((fixSpeed / 100) * speedStack);
    }

    private void ATKChange()
    {
        stepAtk = atkData * (myStepData.StepATK[step] / 100f);
        fixAtk = stepAtk + ((stepAtk / 100) * fixAtkStack);
        atk = fixAtk + ((fixAtk / 100) * atkStack);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("마물 인식중");

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

    private void Update()
    {
        deltaSpeed += Time.deltaTime;

        if (deltaSpeed >= speed)
        {
            findEnemy = FindEnemy();

            if (findEnemy != null)
            {
                Debug.Log("소리남");
                GameManager.Instance.SoundManager.GameAudioClipPlay(0);//공격사운드
                unitAnimation.AttackEffect();//마물이 맞은 이펙트
                deltaSpeed = 0;
            }
        }
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

    public void Attack()//animation에서 호출하기
    {
        if (findEnemy == null)
            return;

        Debug.Log("Attack호출됨");

        //skillGO.transform.position = findEnemy.transform.position;
        //unitAnimation.AttackSkillEffect();//타이밍해결할수있으면 공격끝나고 호출
        findEnemy.EnemyAttacked(atk);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭됨");
        //controller.spriteCanvas.SetActive(true);

        gameObject.layer = 6;
        unitCanvas.SetActive(true);
        //끌떄는 0

        /*        if (controller.onUnitPopUP.Count != 0)
                {
                    if (controller.onUnitPopUP[0] != btnUI)
                    {
                        controller.onUnitPopUP[0].SetActive(false);
                        controller.onUnitPopUP[1].SetActive(false);

                        controller.onUnitPopUP.Clear();

                        controller.onUnitPopUP.Add(btnUI);
                        controller.onUnitPopUP.Add(rangeGO);
                    }
                }
                else
                {
                    controller.onUnitPopUP.Add(btnUI);
                    controller.onUnitPopUP.Add(rangeGO);
                }

                UIOnOff();*/
    }
/*
    public void UIOnOff()
    {
        if (btnUI == null)
            return;

        if (controller.onUnitPopUP[0].activeSelf == true)
        {
            controller.onUnitPopUP[0].SetActive(false);
            controller.onUnitPopUP[1].SetActive(false);

            controller.onUnitPopUP.Clear();
        }
        else
        {
            if (controller.CanUpgradeCheck(myData.id) && myData.step < 2)
                nonClickImage.gameObject.SetActive(false);
            else
                nonClickImage.gameObject.SetActive(true);

            controller.onUnitPopUP[0].SetActive(true);
            controller.onUnitPopUP[1].SetActive(true);
        }

    mySprite.layer = 0;
    }

    public void UIOff()
    {
        controller.onUnitPopUP[0].SetActive(false);
        controller.onUnitPopUP[1].SetActive(false);

        controller.onUnitPopUP.Clear();
    mySprite.layer = 0;
        *//*        btnUI.gameObject.SetActive(false);
                rangeGO.SetActive(false);*//*
    }*/
}
