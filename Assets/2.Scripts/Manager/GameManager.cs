using Constants;
using UnityEngine;

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

    //====================== Enemy, UI
    public EnemySpawn EnemySpawn;
    public WaveUI WaveUI;
    public TimerUI TimerUI;

    public BestRecord BestRecord;

    //====================== Player,Unit
    public Player Player { get { return player; } }
    private Player player;

    public UnitManager UnitManager { get { return unitManager; } }
    private UnitManager unitManager;

    public UnitController UnitController;

    //====================== Money
    public int Point
    {
        get { return point; }
        set
        {
            point += value;
            uiMnager.MoneyTypeUpdate(MoneyType.Point, point);
        }
    }
    private int point;

    public int Key
    {
        get { return key; }
        set
        {
            key += value;
            uiMnager.MoneyTypeUpdate(MoneyType.KEY, key);
        }
    }
    private int key;

    public int Gold
    {
        get { return ruby; }
        set
        {
            ruby += value;
            uiMnager.MoneyTypeUpdate(MoneyType.Ruby, ruby);
        }
    }
    private int ruby;

    //================================================

    private void Awake()
    {
        #region ΩÃ±€≈Ê
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


        //Init
        playerEvent.Init(this);
        dataManager.Init(this);
        soundManager.Init(this);
        uiMnager.Init(this);
        player.Init(this);
        sceneEffect.Init(this);
        unitManager.Init(this);//datamanager∫∏¥Ÿ æ∆∑°

        //Gold
        MoneyInit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameExit();
    }

    private void MoneyInit()
    {
        Point = 10;
        Key = 999;
        Gold = 100;
    }

    public void MoneyChange(MoneyType type, int val)
    {
        switch (type)
        {
            case MoneyType.Point:
                Point = val; break;
            case MoneyType.KEY:
                Key = val; break;
            case MoneyType.Ruby:
                Gold = val; break;
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

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
