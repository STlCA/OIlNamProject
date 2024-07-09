using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    /*    private void Update()
        {
    #if UNITY_EDITOR
            if (Input.anyKeyDown)
                SceneManager.LoadScene("MainScene");
    #elif UNITY_ANDROID
            if (Input.touchCount > 0)
                SceneManager.LoadScene("MainScene");
    #endif
        }*/

    public SoundManager soundManager;

    public void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }

/*    private void Update()
    {
        if (Input.anyKeyDown)
            soundManager.EffectAudioClipPlay(0);
    }*/
}