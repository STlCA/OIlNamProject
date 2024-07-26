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

    [Header("GoldUI")]
    public TMP_Text goldTxt;
    public TMP_Text keyTxt1;
    public TMP_Text keyTxt2;
    public TMP_Text crystalTxt;

    [Header("PlayerUI")]
    public TMP_Text nameTxt;
    //public TMP_Text levelTxt;
    public TMP_Text expTxt;
    public TMP_InputField nameInputField;

    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;

    [Header("Scene")]
    public Image fadeImage;
    public Slider slider;

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = GameManager.Instance.UIManager;

        if (goldTxt != null)
            Init();
    }

    public void Init()
    {
        List<TMP_Text> golds = new()
        {
            goldTxt,
            keyTxt1,
            keyTxt2,
            crystalTxt
        };

        List<TMP_Text> player = new()
        {
            nameTxt,
            //levelTxt,
            expTxt,
        };

        uiManager.MainUIUpdate(golds);

        gameManager.Player.PlayerUIUpdate(player, nameInputField);

        gameManager.SoundManager.SourceSet(bgmSource, effectSource);
    }

    public void GameStart(GameObject ui)
    {
        if (StartCheck())
        {
            gameManager.MoneyChange(Constants.MoneyType.KEY, -1);
            gameManager.SceneEffect.MainToGame();
        }
        else
            gameManager.PopUpController.UIOn(ui);
    }
    private bool StartCheck()
    {
        return gameManager.Key > 0;
    }
}
