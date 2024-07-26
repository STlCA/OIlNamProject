using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private int wave = 50;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private TMP_Text waveText;

    // Wave 시작
    private void StartWave()
    {

    }

    // 다음 Wave로 진입
    public void NextWave()
    {
        // 넘어갈 다음 Wave가 있을 때

        // 최종 보스 Wave를 종료할 때

    }

    // wave UI 업데이트
    private void UpdateWaveUI()
    {
        waveText.text = "WAVE " + currentWave.ToString();
    }
}
