using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public SoundManager soundManager;

    public Image topFadeImage;
    public Image whiteImage;
    public Slider loadingSlider;
    public TMP_Text tipTxt;
    public DataManager dataManager;

    private DataTable_TIPLoader tipBase;

    private float time = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        tipBase = dataManager.dataTable_TIPLoader;
    }

    private void TipTextSet()
    {
        int random = Random.Range(0, tipBase.ItemsList.Count);

        tipTxt.text = tipBase.ItemsList[random].Tip;
    }

    public void SceneChange()
    {
        StopAllCoroutines();

        topFadeImage.gameObject.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        TipTextSet();
        SaveSystem.Load();

        StartCoroutine("LoadingScene");
    }

    private IEnumerator LoadingScene()
    {
        float timer = 0.0f;
        AsyncOperation loading = SceneManager.LoadSceneAsync("MainScene");

        loading.allowSceneActivation = false;

        while (!loading.isDone) //씬 로딩 완료시 while문이 나가짐
        {
            timer += Time.deltaTime;

            if (loading.progress < 0.8f)
                loadingSlider.value = 0.2f;

            if (loading.progress >= 0.8f)
                loadingSlider.value = 0.8f;
            else
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, loading.progress, timer);

            if (loading.progress >= 0.9f && timer > 2f)
            {
                loadingSlider.value = 1f;
                //whiteImage.gameObject.SetActive(true);
                //yield return StartCoroutine("SceneChangeFadeOut");
                loading.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public IEnumerator SceneChangeFadeOut()
    {
        Color alpha = topFadeImage.color;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / 1;
            alpha.a = Mathf.Lerp(1, 0, time);
            topFadeImage.color = alpha;
            yield return null;
        }

        topFadeImage.gameObject.SetActive(false);
    }

}
