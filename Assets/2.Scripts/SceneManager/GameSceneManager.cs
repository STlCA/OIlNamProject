using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public UnitSpawnController unitSpawnController;
    public HappyEnergy happyEnergy;

    [Header("Stage : Tile / Spirte / Way / Spawn")]
    public List<GameObject> stage1;
    public List<GameObject> stage2;

    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;
    public AudioSource gameSource;

    [Header("UI")]
    public TMP_Text rubyTxt;
    public TMP_Text useRubyTxt;
    public GameObject speed1x;
    public GameObject speed2x;

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

            if (useRuby >= 100)
                useRuby = 100;

            useRubyTxt.text = useRuby.ToString("N0");
        }
    }
    private int useRuby;

    private void Start()
    {
        SoundInit();
        RubyInit();

        happyEnergy = GetComponent<HappyEnergy>();

        MapInit(GameManager.Instance.Stage);
    }

    private void MapInit(int stage)
    {
        if (stage == 1)
        {
            for (int i = 0; i < stage1.Count; ++i)
            {
                stage1[i].SetActive(true);
                stage2[i].SetActive(false);
            }

            unitSpawnController.Init(stage1[3]);
        }
        else if (stage == 2)
        {
            for (int i = 0; i < stage1.Count; ++i)
            {
                stage2[i].SetActive(true);
                stage1[i].SetActive(false);
            }

            unitSpawnController.Init(stage2[3]);
        }
        else
            Debug.Log("Stage설정안됨");
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
            GameManager.Instance.SoundManager.EffectAudioClipPlay(9);

        if (time == 1)
        {
            // 2배속
            time = 2;
            speed1x.SetActive(false);
            speed2x.SetActive(true);
        }
        //else if (time == 2)
        //    // 3배속
        //    time = 3;
        else if (time == 2)
        {
            // 1배속
            time = 1;
            speed1x.SetActive(true);
            speed2x.SetActive(false);
        }

        Time.timeScale = time;
    }

    public void RubyInit()
    {
        //Ruby = 60;
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

    public void PlayerExpUp()//치트키
    {
        GameManager.Instance.Player.ExpUp(2000);
    }
}
