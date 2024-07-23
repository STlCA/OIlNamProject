using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //====================== Player
    public Player Player { get { return player; } }
    private Player player;

    //====================== Money
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            uiMnager.GoldTypeUpdate(GoldType.GOLD, value);
        }
    }
    private int gold;

    public int Key
    {
        get { return key; }
        set
        {
            key = value;
            uiMnager.GoldTypeUpdate(GoldType.KEY, value);
          }
    }
    private int key;

    public int Crystal
    {
        get { return crystal; }
        set
        {
            crystal = value;
            uiMnager.GoldTypeUpdate(GoldType.CRYSTAL, value);
        }
    }
    private int crystal;

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
        dataManager = GetManager<DataManager>();
        popUpController = GetManager<PopUpController>();
        soundManager = GetManager<SoundManager>();
        uiMnager = GetManager<UIManager>();
        player = GetManager<Player>();
        sceneEffect = GetManager<SceneEffect>();


        //Init
        dataManager.Init(this);
        popUpController.Init(this);
        soundManager.Init(this);
        uiMnager.Init(this);
        player.Init(this);
        sceneEffect.Init(this);

        //Gold
        GoldInit();
    }

    private void GoldInit()
    {
        Gold = 10000;
        Key = 3;
        Crystal = 10;
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
