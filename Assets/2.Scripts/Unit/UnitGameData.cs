using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UnitGameData : MonoBehaviour, IPointerClickHandler
{
    public UnitSpawnController controller;
    private UnitAnimation unitAnimation;

    [Header("Self")]
    public GameObject myGO;//Init에서 받아오기
    private GameObject unitRootGO;//내스스로
    private CircleCollider2D rangeCollider;
    private SpriteRenderer shadowSR;

    private GameObject spriteGO;
    private GameObject rangeGO;
    private GameObject skillGO;
    private GameObject sellBtn;
    private GameObject upgradeBtn;
    private GameObject star1;
    private GameObject star2;


    //----------------------------------------------------------------Enemy

    private List<Enemy> enemyList = new();
    private Enemy findEnemy;

    //----------------------------------------------------------------Data

    public Vector3 pos;
    public int key;


    public UnitData myUnitData;
    private DataTable_UnitStep myStepData;

    public float Range
    {
        get { return range; }
        private set
        {
            range = value;
            rangeCollider.radius = range;
            rangeGO.transform.localScale = new Vector3(range, range);//되는지확인
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

            if (step == 1)
                star1.SetActive(true);
            else if (step == 2)
            {
                star1.SetActive(false);
                star2.SetActive(true);
            }
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

    private void ChildInit()
    {
        unitRootGO = gameObject; //15와 25를 왔다갔다
        spriteGO = myGO.transform.GetChild(1).GetChild(0).gameObject;
        rangeCollider = GetComponent<CircleCollider2D>();
        shadowSR = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();

        rangeGO = spriteGO.transform.GetChild(0).gameObject;
        sellBtn = spriteGO.transform.GetChild(1).gameObject;
        upgradeBtn = spriteGO.transform.GetChild(2).gameObject;

        star1 = myGO.transform.GetChild(1).GetChild(1).gameObject;
        star2 = myGO.transform.GetChild(1).GetChild(2).gameObject;
        skillGO = myGO.transform.GetChild(1).GetChild(3).gameObject;

        sellBtn.GetComponent<ClickSpriteBtn>().Init(controller, pos);
        upgradeBtn.GetComponent<ClickSpriteBtn>().Init(controller, pos);

        star1.SetActive(false);
        star2.SetActive(false);

        switch (myUnitData.tier)
        {
            case 1:
                shadowSR.color = new Color(1f, 0f, 0f,0.5f);
                break;
            case 2:
                shadowSR.color = new Color(0f, 0f, 1f, 0.5f);
                break;
            case 3:
                shadowSR.color = new Color(1f, 1f, 1f, 0.7f);
                break;
        }
    }

    public void Init(UnitData unitData, DataTable_UnitStep stepData, UnitSpawnController controller, Vector3 pos, GameObject go)
    {
        this.controller = controller;
        this.pos = pos;
        key = unitData.key;
        myGO = go;

        myUnitData = unitData;
        myStepData = stepData;

        ChildInit();

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
        unitAnimation.TypeSet(unitData.type,skillGO.GetComponentInChildren<Animator>(),myUnitData.tier);        

        spriteGO.SetActive(false);
    }

    public void FirstStackChange(float speedfixVal, float speedVal, float atkFixVal, float atkVal)
    {
        fixSpeedStack += speedfixVal;
        speedStack += speedVal;

        fixAtkStack+= atkFixVal;
        atkStack += atkVal;

        SpeedChange();
        ATKChange();
    }

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
        Step++;

        SpeedChange();
        ATKChange();
    }

    private void SpeedChange()
    {
        stepSpeed = speedData * (myStepData.StepSpeed[Step] / 100f);
        fixSpeed = stepSpeed + ((stepSpeed / 100) * fixSpeedStack);
        speed = fixSpeed + ((fixSpeed / 100) * speedStack);
    }

    private void ATKChange()
    {
        stepAtk = atkData * (myStepData.StepATK[Step] / 100f);
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
                //gameObject.AddComponent<AudioSource>().PlayOneShot(GameManager.Instance.SoundManager.gameAudioList[0]);
                GameManager.Instance.SoundManager.GameAudioClipPlay(0);//공격사운드

                //skillGO.transform.position = new Vector3(-0.5f, 0);
                //skillGO.transform.position = findEnemy.transform.position;
                unitAnimation.AttackEffect();
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

        //skillGO.transform.position = findEnemy.transform.position;
        //unitAnimation.AttackSkillEffect();//타이밍해결할수있으면 공격끝나고 호출
        findEnemy.EnemyAttacked(atk);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.spriteCanvas.SetActive(true);        
        controller.unitBG.SetActive(true);
        controller.UnitInfoSet(pos);

        if (controller.onUnitPopUP.Count != 0)
        {
            if (controller.onUnitPopUP[0] != spriteGO)
            {
                controller.onUnitPopUP[1].GetComponent<SortingGroup>().sortingOrder = 15;
                controller.onUnitPopUP[0].SetActive(false);

                controller.onUnitPopUP.Clear();

                controller.onUnitPopUP.Add(spriteGO);
                controller.onUnitPopUP.Add(unitRootGO);

                controller.onUnitPopUP[0].SetActive(true);
                controller.onUnitPopUP[1].GetComponent<SortingGroup>().sortingOrder = 25;
            }
        }
        else
        {
            controller.onUnitPopUP.Add(spriteGO);
            controller.onUnitPopUP.Add(unitRootGO);

            controller.onUnitPopUP[0].SetActive(true);
            controller.onUnitPopUP[1].GetComponent<SortingGroup>().sortingOrder = 25;
        }
    }
}

