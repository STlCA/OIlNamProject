using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;

    [Header("UI")]
    public TMP_Text goldTxt;
    public TMP_Text useGoldTxt;

    private float time = 1;
    public int Gold
    {
        get { return gold; }
        private set
        {
            gold += value;
            goldTxt.text = gold.ToString("N0");
        }
    }
    private int gold;

    public int UseGold
    {
        get { return useGold; }
        private set
        {
            useGold += value;
            useGoldTxt.text = useGold.ToString("N0");
        }
    }
    private int useGold;

    private void Start()
    {
        SoundInit();
        GoldInit();
    }

    private void SoundInit()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SoundManager.SourceSet(bgmSource, effectSource);
    }

    public void TimeChange()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SoundManager.EffectAudioClipPlay(0);

        if (time == 1)
            time = 2;
        else if (time == 2)
            time = 3;
        else if (time == 3)
            time = 1;

        Time.timeScale = time;
    }

    public void GoldInit()
    {
        Gold = 25;
        UseGold = 5;
    }

    public void ChangeGold(int val)
    {
        Gold = val;
    }

    public bool CanUseGold()
    {
        bool canUse = Gold >= UseGold;                

        return canUse;
    }
    public int ChangeUseGold()
    {
        int val = UseGold;
        UseGold = 2;

        return val;
    }
}
