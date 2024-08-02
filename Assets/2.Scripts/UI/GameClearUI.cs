using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 메인 화면으로 이동
    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
