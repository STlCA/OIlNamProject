using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private GameObject gameResultUI;
    [SerializeField] private GameObject newRecord;
    [SerializeField] private TMP_Text currentRecordText;
    [SerializeField] private TMP_Text lastBestRecordText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 게임 재시작
    public void GameStart(GameObject ui)
    {
        if (StartCheck())
        {
            GameManager.Instance.MoneyChange(Constants.MoneyType.KEY, -1);
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1f;
        }
        else
            GameManager.Instance.PopUpController.UIOn(ui);
    }

    // 재화 확인
    private bool StartCheck()
    {
        return GameManager.Instance.Key > 0;
    }

    // 메인 화면으로 이동
    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
