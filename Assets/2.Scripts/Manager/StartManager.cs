using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Image topFadeImage;
    public Slider loadingSlider;

    private float time = 0f;

    public void SceneChange()
    {
        topFadeImage.gameObject.SetActive(true);
        loadingSlider.gameObject.SetActive(true);

        StartCoroutine("LoadingScene");
    }

    private IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("MainScene");

        while (!loading.isDone) //씬 로딩 완료시 while문이 나가짐
        {
            if (loading.progress >= 0.9f)
                loadingSlider.value = 1f;
            else
                loadingSlider.value = loading.progress;

            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return StartCoroutine("SceneChangeFadeOut");
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
