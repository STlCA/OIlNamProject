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
        manager.SceneChange();
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
}
