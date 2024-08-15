using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private GameObject messageUI;  // 신기록/클리어 UI
    [SerializeField] private TMP_Text messageText;  // 신기록/클리어 텍스트
    [SerializeField] private TMP_Text currentRecordText;    // 이번 기록 텍스트
    [SerializeField] private TMP_Text lastBestRecordText;   // 지난 최고 기록 텍스트
    [SerializeField] private TMP_Text expText;  // 보상 경험치 텍스트
    [SerializeField] private TMP_Text goldText; // 보상 공드 텍스트
    [SerializeField] private TMP_Text eBook;    // 보상 모집권 텍스트
    [SerializeField] private GameObject restartBtn; // 재시작 버튼

    private DataTable_WaveRewardLoader rewardData;

    private void Awake()
    {
        rewardData = GameManager.Instance.DataManager.dataTable_WaveRewardLoader;
    }

    // 신기록 달성 실패 시
    public void DisableMessage()
    {
        messageUI.SetActive(false);
    }

    // 신기록 달성 시 NewRecord! 출력
    public void NewRecordText()
    {
        messageUI.SetActive(true);
        messageText.text = "New Record!";
    }

    // 해당 챕터 모두 클리어 시 Clear! 출력
    public void ClearText()
    {
        messageUI.SetActive(true);
        messageText.text = "Clear!";
    }

    // 게임 기록 출력
    public void PrintRecord(int currentWave, int bestRecord, bool isClear = false)
    {
        int difference;

        // 신기록 일 경우
        if (currentWave > bestRecord)
        {
            difference = currentWave - bestRecord;

            currentRecordText.text = "WAVE " + currentWave.ToString() + " (▲ " + difference.ToString() + ")";

            // 상단 클리어 UI 출력
            if(isClear)
            {
                ClearText();
            }
            // 상단 신기록 UI 출력
            else
            {
                NewRecordText();
            }

            // 기록 갱신
            GameManager.Instance.UpdateRecord(currentWave, GameManager.Instance.Stage);
        }
        // 신기록이 아닐 경우
        else if (bestRecord >= currentWave)
        {
            difference = bestRecord - currentWave;

            currentRecordText.text = "WAVE " + currentWave.ToString() + " (▼ " + difference.ToString() + ")";

            // 상단 신기록/클리어 UI 비활성화
            DisableMessage();
        }

        lastBestRecordText.text = "WAVE " + bestRecord.ToString();
    }

    // 전투 보상
    public void GetReward(int currentWave)
    {
        int rewardID = GameManager.Instance.Stage == 1 ? 7000 + currentWave : 7050 + currentWave;

        expText.text = "EXP " + rewardData.GetByKey(rewardID).Exp.ToString();
        GameManager.Instance.Player.ExpUp(rewardData.GetByKey(rewardID).Exp);

        goldText.text = string.Format("{0:#,###}", rewardData.GetByKey(rewardID).Gold).ToString();
        GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, rewardData.GetByKey(rewardID).Gold);

        eBook.text = rewardData.GetByKey(rewardID).Enforcebook.ToString() + "개";
        GameManager.Instance.UnitManager.ChangeUnitPiece(rewardData.GetByKey(rewardID).Enforcebook);

        SaveSystem.Save();
    }

    // 메인 화면으로 이동
    public void GoHome()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SoundManager.EffectAudioClipPlay(9);
        SceneManager.LoadScene("MainScene");
    }

    // 게임 재시작 버튼 활성화/비활성화
    public void RestartButtonActivation(bool isActivation)
    {
        if (isActivation)
        {
            restartBtn.SetActive(true);
        }
        else
        {
            restartBtn.SetActive(false);
        }
    }

    // 게임 재시작
    public void GameStart(GameObject ui)
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(9);

        if (StartCheck())
        {
            GameManager.Instance.MoneyChange(Constants.MoneyType.KEY, -5);
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
}
