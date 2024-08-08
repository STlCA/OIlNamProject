using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    private SoundManager soundManager;//나중에private
    private float currentTimeScale;

    public GameObject Result;

    private void Start()
    {
        if (GameManager.Instance != null)
            soundManager = GameManager.Instance.SoundManager;
    }

    public void ResultOn()
    {
        Result.SetActive(true);
    }

    //ON OFF Change
    public void UIOnOff(GameObject ui)
    {
        if (ui == null)
            return;

        if (ui.activeSelf == true)
            ui.SetActive(false);
        else
            ui.SetActive(true);
    }

    //UION / OFF
    public void UIOn(GameObject ui)
    {
        if (ui == null)
            return;
        ui.SetActive(true);
    }
    public void UIOff(GameObject ui)
    {
        if (ui == null)
            return;

        ui.SetActive(false);
        //Time.timeScale = 1.0f; // 현재 상황에서는 팝업이 제거되었을 때 타임스케일을 1로 할 필요가 없어서 주석처리함. 만약 타임 스케일이 1로 바뀌는 기능이 필요하다면 메서드를 추가로 작성하길 바람
    }
    public void UIOffStopTime(GameObject ui)
    {
        if (ui == null)
            return;

        ui.SetActive(false);
    }
    //
    public void PauseUIOn(GameObject ui)
    {
        soundManager.EffectAudioClipPlay(0);

        if (ui == null)
            return;

        ui.SetActive(true);
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
    public void PauseUIOff(GameObject ui)
    {
        soundManager.EffectAudioClipPlay(0);

        if (ui == null)
            return;

        Time.timeScale = currentTimeScale;

        ui.SetActive(false);
    }
    public void PauseUIHomeOut(GameObject ui)
    {
        soundManager.EffectAudioClipPlay(0);

        if (ui == null)
            return;

        ui.SetActive(false);

        Time.timeScale = 1f;

        GameManager.Instance.SceneEffect.GameToMain();
    }
}

