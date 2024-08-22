using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAniamtion : MonoBehaviour
{
    public StartManager manager;    
    public GameObject story;
    public GameObject tutorial;        

    public void StoryEnd()
    {
        tutorial.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TutorialEnd()
    {
        if(manager != null) 
            manager.SceneChange();
        else
        {
            GameManager.Instance.SoundManager.BGMCheck(0);
            tutorial.SetActive(false);        
        }
    }

    public void SelfEnd()
    {
        gameObject.SetActive(false);
    }
    public void MergeTutorialEnd()
    {
        Time.timeScale = GameSceneManager.CurrentTimeScale;
        gameObject.SetActive(false);
    }
    public void AllEnd()
    {
        tutorial.SetActive(false);
        gameObject.SetActive(true);
    }
}
