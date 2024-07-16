using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [Header("GoldUI")]
    public TMP_Text goldTxt;
    public TMP_Text keyTxt1;
    public TMP_Text keyTxt2;
    public TMP_Text crystalTxt;

    [Header("PlayerUI")]
    public TMP_Text nameTxt;
    public TMP_Text setNametxt;
    //public TMP_Text levelTxt;
    public TMP_Text expTxt;

    private void Start()
    {
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
            setNametxt,
            //levelTxt,
            expTxt
        };

        GameManager.Instance.UIManager.MainUIUpdate(golds);

        GameManager.Instance.Player.PlayerUIUpdate(player);
    }

    public void MainGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GameMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
