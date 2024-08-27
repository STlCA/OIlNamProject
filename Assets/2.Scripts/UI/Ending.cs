using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    public VideoPlayer video;

    private void Start()
    {
        video.loopPointReached += SelfActive;
    }

    public void SelfActive(VideoPlayer vp)
    {
        GameManager.Instance.SoundManager.BGMSource.Play();
        gameObject.SetActive(false);
    }
    public void SelfActive()
    {
        GameManager.Instance.SoundManager.BGMSource.Play();
        gameObject.SetActive(false);
    }
}
