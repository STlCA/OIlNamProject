using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public UnitController unitController;
    public HappyEnergy happyEnergy;

    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;
    public AudioSource gameSource;

    [Header("UI")]
    public TMP_Text rubyTxt;
    public TMP_Text useRubyTxt;

    private float time = 1;
    public int Ruby
    {
        get { return ruby; }
        private set
        {
            ruby += value;
            rubyTxt.text = ruby.ToString("N0");
        }
    }
    private int ruby;

    public int UseRuby
    {
        get { return useRuby; }
        private set
        {
            useRuby += value;
            useRubyTxt.text = useRuby.ToString("N0");
        }
    }
    private int useRuby;

    private void Start()
    {
        SoundInit();
        RubyInit();

        happyEnergy = GetComponent<HappyEnergy>();
    }

    private void SoundInit()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SoundManager.SourceSet(bgmSource, effectSource, gameSource);
            GameManager.Instance.SoundManager.BGMChange(2);
        }

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

    public void RubyInit()
    {
        Ruby = 25;
        UseRuby = 5;
    }

    public void ChangeRuby(int val)
    {
        Ruby = val;
    }

    public bool CanUseRuby()
    {
        bool canUse = Ruby >= UseRuby;                

        return canUse;
    }
    public int ChangeUseRuby()
    {
        int val = UseRuby;
        UseRuby = 2;

        return val;
    }
}
