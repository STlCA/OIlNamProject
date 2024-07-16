using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private UIManager uIManager;

    [Header("GoldUI")]
    public TMP_Text goldTxt;
    public TMP_Text keyTxt1;
    public TMP_Text keyTxt2;
    public TMP_Text crystalTxt;

    private void Start()
    {
        if(goldTxt != null)
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

        uIManager = GameManager.Instance.UIManager;
        uIManager.MainUIUpdate(golds);
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
