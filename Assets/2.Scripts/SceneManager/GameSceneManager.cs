using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource bgmSource;
    public AudioSource effectSource;


    private void Start()
    {
        SoundInit();
    }

    private void SoundInit()
    {
        if(GameManager.Instance!=null)
            GameManager.Instance.SoundManager.SourceSet(bgmSource, effectSource);
    }
}
