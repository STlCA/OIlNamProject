using Constants;
using System;
using UnityEngine;

[Serializable]
public struct Save_MoneyData
{
    public int Gold;
    public int Key;
    public int Diamond;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //======================Manager

    public DataManager DataManager { get { return dataManager; } }
    private DataManager dataManager;

    public PopUpController PopUpController { get { return popUpController; } }
    private PopUpController popUpController;

    public SoundManager SoundManager { get { return soundManager; } }
    private SoundManager soundManager;

    public UIManager UIManager { get { return uiMnager; } }
    private UIManager uiMnager;

    public SceneEffect SceneEffect { get { return sceneEffect; } }
    private SceneEffect sceneEffect;

    public PlayerEvent PlayerEvent { get { return playerEvent; } }
    private PlayerEvent playerEvent;

    public TimeManager TimeManager { get { return timeManager; } }
    private TimeManager timeManager;

    //====================== Enemy, UI
    public EnemySpawn EnemySpawn;
    public WaveUI WaveUI;
    public TimerUI TimerUI;

    public BestRecord BestRecord;
    public int BestScore1 { get; private set; } = 0;
    public int BestScore2 { get; private set; } = 0;

    public GameObject ExitGameUI;

    //====================== Player,Unit
    public Player Player { get { return player; } }
    private Player player;

    public UnitManager UnitManager { get { return unitManager; } }
    private UnitManager unitManager;

    //====================== Money
    public int Gold
    {
        get { return gold; }
        set
        {
            gold += value;
            uiMnager.MoneyTypeUpdate(MoneyType.Gold, gold);
        }
    }
    private int gold;

    public int Key
    {
        get { return key; }
        set
        {
            if (key >= 30 && value < 0)//키를 사용했을때 원래 키가 30~ 이라면
                timeManager.KeyTimerStart(true);          

            key += value;

            if (key >= 30)
            {
                key = 30;
                timeManager.KeyTimerStart(false);
            }
            uiMnager.MoneyTypeUpdate(MoneyType.KEY, key);
        }
    }
    private int key;

    public int Diamond
    {
        get { return diamond; }
        set
        {
            diamond += value;
            uiMnager.MoneyTypeUpdate(MoneyType.Diamond, diamond);
        }
    }
    private int diamond;

    //================================================

    public int Stage { get; private set; } = 1;

    private void Awake()
    {
        #region 싱글톤
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        #endregion

        //Find
        playerEvent = GetManager<PlayerEvent>();
        dataManager = GetManager<DataManager>();
        popUpController = GetFind<PopUpController>();
        soundManager = GetManager<SoundManager>();
        uiMnager = GetManager<UIManager>();
        player = GetManager<Player>();
        sceneEffect = GetManager<SceneEffect>();
        unitManager = GetManager<UnitManager>();
        timeManager = GetManager<TimeManager>();


        //Init
        playerEvent.Init(this);
        dataManager.Init(this);
        soundManager.Init(this);
        uiMnager.Init(this);
        player.Init(this);
        sceneEffect.Init(this);
        unitManager.Init(this);//datamanager보다 아래
        timeManager.Init(this);

        //Gold
        if (!SaveSystem.SaveFile())
            MoneyInit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGameUI.SetActive(true);
    }

    private void MoneyInit()
    {
        Gold = 100;
        Key = 30;
        Diamond = 100;
    }

    public void MoneyChange(MoneyType type, int val)
    {
        switch (type)
        {
            case MoneyType.Gold:
                Gold = val; break;
            case MoneyType.KEY:
                Key = val; break;
            case MoneyType.Diamond:
                Diamond = val; break;
        }
    }

    // 최고 기록 갱신
    public void UpdateRecord(int currentWave, int stage)
    {
        if (stage == 1)
        {
            BestScore1 = currentWave;
        }
        else
        {
            BestScore2 = currentWave;
        }
    }

    public T GetManager<T>() where T : Manager
    {
        T t = GetComponentInChildren<T>();
        //t.StateInit(this);
        return t;
    }

    public T GetFind<T>() where T : MonoBehaviour
    {
        T t = FindObjectOfType<T>();
        return t;
    }

    public void SetStage(int stage)
    {
        Stage = stage;
    }

    public void Save(ref Save_MoneyData saveData)
    {
        saveData.Gold = Gold;
        saveData.Key = Key;
        saveData.Diamond = Diamond;
    }

    public void Load(Save_MoneyData saveData)
    {
        Gold = saveData.Gold;
        Key = saveData.Key;
        Diamond = saveData.Diamond;
    }

    public void GameExit()
    {
        SaveSystem.Save();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
