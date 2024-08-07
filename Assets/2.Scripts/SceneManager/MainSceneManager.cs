using Cainos.PixelArtTopDown_Basic;
using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;
    private UnitUpgradeController unitUpgradeController;

    [Header("UI")]    
    public List<GameObject> tabUI;
    public List<GameObject> btnUI;

    [Header("GoldUI")]
    public TMP_Text goldTxt;
    public TMP_Text keyTxt1;
    public TMP_Text crystalTxt;

    [Header("PlayerUI")]
    public TMP_Text nameTxt;
    //public TMP_Text levelTxt;
    public TMP_Text expTxt;
    public TMP_Text setUIName;
    public TMP_InputField nameInputField;

    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;
    public AudioSource gameSource;

    [Header("Scene")]
    public Image fadeImage;
    public Slider slider;
    public GameObject nameFalse;

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = GameManager.Instance.UIManager;

        unitUpgradeController = GetComponent<UnitUpgradeController>();

        if (goldTxt != null)
            Init();
    }

    public void Init()
    {
        List<TMP_Text> golds = new()
        {
            goldTxt,
            keyTxt1,
            crystalTxt
        };

        List<TMP_Text> player = new()
        {
            nameTxt,
            expTxt,
            setUIName,
        };

        uiManager.MainUIUpdate(golds);

        gameManager.Player.PlayerUIInit(player, nameInputField);

        gameManager.SoundManager.SourceSet(bgmSource, effectSource, gameSource);
        gameManager.SoundManager.BGMChange(1);
    }

    public void GameStart(GameObject ui)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Lobby);

        if (StartCheck())
        {
            gameManager.MoneyChange(MoneyType.KEY, -5);
            gameManager.SceneEffect.MainToGame();
        }
        else
            gameManager.PopUpController.UIOn(ui);
    }
    private bool StartCheck()
    {
        return gameManager.Key > 0;
    }
    public void Click()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Intro);
    }

    public void ChangeName(GameObject closeUI)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Intro);

        if (nameInputField.text.Length <= 0 || nameInputField.text.Length > 10)
        {
            nameFalse.SetActive(true);
            return;
        }

        setUIName.text = nameInputField.text;
        gameManager.Player.SetUserName(nameInputField.text);
        nameInputField.text = "";
        closeUI.SetActive(false);
    }

    public void TabChange(int tabType)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(EffectList.Intro);

        for (int i = 0; i < tabUI.Count; i++)
        {
            tabUI[i].SetActive(false);
            btnUI[i].SetActive(false);
        }

        switch (tabType)
        {
            case (int)TabType.Shop://Çò°¥·Á¼­enum
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Home:
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Gacha:
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Unit:
                btnUI[tabType].SetActive(true);
                unitUpgradeController.OnUnitTab();
                tabUI[tabType].SetActive(true);
                break;

            default:
                Debug.Log("ÅÇ¹öÆ°¼³Á¤¾ÈµÊ");
                return;
        }
    }
}
