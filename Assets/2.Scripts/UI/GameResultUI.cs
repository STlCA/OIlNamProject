using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private GameObject messageUI;  // �ű��/Ŭ���� UI
    [SerializeField] private TMP_Text messageText;  // �ű��/Ŭ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text currentRecordText;    // �̹� ��� �ؽ�Ʈ
    [SerializeField] private TMP_Text lastBestRecordText;   // ���� �ְ� ��� �ؽ�Ʈ
    [SerializeField] private TMP_Text expText;  // ���� ����ġ �ؽ�Ʈ
    [SerializeField] private TMP_Text goldText; // ���� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text eBook;    // ���� ������ �ؽ�Ʈ
    [SerializeField] private GameObject restartBtn; // ����� ��ư

    private DataTable_WaveRewardLoader rewardData;

    private void Awake()
    {
        rewardData = GameManager.Instance.DataManager.dataTable_WaveRewardLoader;
    }

    // �ű�� �޼� ���� ��
    public void DisableMessage()
    {
        messageUI.SetActive(false);
    }

    // �ű�� �޼� �� NewRecord! ���
    public void NewRecordText()
    {
        messageUI.SetActive(true);
        messageText.text = "New Record!";
    }

    // �ش� é�� ��� Ŭ���� �� Clear! ���
    public void ClearText()
    {
        messageUI.SetActive(true);
        messageText.text = "Clear!";
    }

    // ���� ��� ���
    public void PrintRecord(int currentWave, int bestRecord, bool isClear = false)
    {
        int difference;

        // �ű�� �� ���
        if (currentWave > bestRecord)
        {
            difference = currentWave - bestRecord;

            currentRecordText.text = "WAVE " + currentWave.ToString() + " (�� " + difference.ToString() + ")";

            // ��� Ŭ���� UI ���
            if(isClear)
            {
                ClearText();
            }
            // ��� �ű�� UI ���
            else
            {
                NewRecordText();
            }

            // ��� ����
            GameManager.Instance.UpdateRecord(currentWave, GameManager.Instance.Stage);
        }
        // �ű���� �ƴ� ���
        else if (bestRecord >= currentWave)
        {
            difference = bestRecord - currentWave;

            currentRecordText.text = "WAVE " + currentWave.ToString() + " (�� " + difference.ToString() + ")";

            // ��� �ű��/Ŭ���� UI ��Ȱ��ȭ
            DisableMessage();
        }

        lastBestRecordText.text = "WAVE " + bestRecord.ToString();
    }

    // ���� ����
    public void GetReward(int currentWave)
    {
        int rewardID = GameManager.Instance.Stage == 1 ? 7000 + currentWave : 7050 + currentWave;

        expText.text = "EXP " + rewardData.GetByKey(rewardID).Exp.ToString();
        GameManager.Instance.Player.ExpUp(rewardData.GetByKey(rewardID).Exp);

        goldText.text = string.Format("{0:#,###}", rewardData.GetByKey(rewardID).Gold).ToString();
        GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, rewardData.GetByKey(rewardID).Gold);

        eBook.text = rewardData.GetByKey(rewardID).Enforcebook.ToString() + "��";
        GameManager.Instance.UnitManager.ChangeUnitPiece(rewardData.GetByKey(rewardID).Enforcebook);

        SaveSystem.Save();
    }

    // ���� ȭ������ �̵�
    public void GoHome()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SoundManager.EffectAudioClipPlay(9);
        SceneManager.LoadScene("MainScene");
    }

    // ���� ����� ��ư Ȱ��ȭ/��Ȱ��ȭ
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

    // ���� �����
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

    // ��ȭ Ȯ��
    private bool StartCheck()
    {
        return GameManager.Instance.Key > 0;
    }
}
