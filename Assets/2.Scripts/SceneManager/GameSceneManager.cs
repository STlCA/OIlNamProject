using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;
    private float time = 1;

    private void Start()
    {
        SoundInit();
    }

    private void SoundInit()
    {
        if(GameManager.Instance!=null)
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
}
