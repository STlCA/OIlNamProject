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
    private UnitManager unitManager;
    private UnitUpgradeController unitUpgradeController;
    private PlayerEvent playerEvent;

    [Header("UI : SHOP / HOME / GACHA / UNIT")]
    public List<GameObject> tabUI;
    public List<GameObject> btnUI;

    [Header("UI")]//UnitManager로
    public TMP_Text tabPieceTxt;
    public TMP_Text gachaTabPieceTxt;
    public GameObject falseGacha;
    public GameObject resultUI;
    public TMP_Text resultPieceTxt;
    public List<TMP_Text> resultTierPiece;
    public List<GameObject> tierAnim;
    public GameObject gachaAnim;
    public List<TMP_Text> unitTabTierPiece = new();

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
    public TMP_InputField couponInputField;

    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;
    public AudioSource gameSource;

    [Header("Scene")]
    public Image fadeImage;
    public Slider slider;
    public GameObject nameFalse;

    [Header("FalseUI")]
    public GameObject falseCoupon;//번호틀림
    public GameObject falseUseCoupon;//이미사용

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = GameManager.Instance.UIManager;
        unitManager = GameManager.Instance.UnitManager;
        playerEvent = GameManager.Instance.PlayerEvent;

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

        unitManager.SetUIText(tabPieceTxt, gachaTabPieceTxt, falseGacha, resultUI, resultPieceTxt, resultTierPiece, tierAnim, gachaAnim, unitTabTierPiece);

        playerEvent.CouponUISetting(couponInputField, falseCoupon, falseUseCoupon);
    }

    public void GameStart(GameObject ui)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay((int)EffectList.Lobby);

        SetStage(1);//---------임시 스테이지 셋팅

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
            case (int)TabType.Shop://헷갈려서enum
                GameManager.Instance.SoundManager.BGMCheck(1);
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Home:
                GameManager.Instance.SoundManager.BGMCheck(1);
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Gacha:
                GameManager.Instance.SoundManager.BGMCheck(8);
                btnUI[tabType].SetActive(true);
                tabUI[tabType].SetActive(true);
                break;

            case (int)TabType.Unit:
                GameManager.Instance.SoundManager.BGMCheck(1);
                btnUI[tabType].SetActive(true);
                unitUpgradeController.OnUnitTab();
                tabUI[tabType].SetActive(true);
                break;

            default:
                Debug.Log("탭버튼설정안됨");
                return;
        }
    }

    //BTN Stage //임시로 호출
    public void SetStage(int stage)
    {
        GameManager.Instance.SetStage(stage);
    }
}
