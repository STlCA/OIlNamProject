using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneEffect : Manager
{
    private StoryUI storyUI = null;
    private StartManager startManager = null;


    //GameManager children�� �ƴϸ� fadeImage��������������?

    [Header("StartScene")]
    public Image startfadeImage = null;
    public Image fadeImage = null;
    public Image crackingImage = null;
    public RawImage startVideoImage = null;
    public Image textBackImage = null;
    public Image skipImage = null;

    [Header("Scene")]
    public GameObject myCanvas;
    private Image myfadeImage = null;
    private Slider mySlider = null;

    private float fadeTime = 1f;
    private float waitTime = 1f;
    private float time = 0f;
    private float circleScale = 100f;

    [HideInInspector]public TMP_Text storyText = null;//�ӽ�public

    private void Start()
    {
        if (GetComponent<StoryUI>() != null)
        {
            startManager = GetComponent<StartManager>();
            storyUI = GetComponent<StoryUI>();
            storyUI.Init();

            CanvasInit();
        }
    }

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        myfadeImage = myCanvas.GetComponentInChildren<Image>();
        mySlider = myCanvas.GetComponentInChildren<Slider>();

        myfadeImage.gameObject.SetActive(false);
        mySlider.gameObject.SetActive(false);
    }

    private void CanvasInit()
    {
        GameObject go = Instantiate(myCanvas);

        myfadeImage = go.GetComponentInChildren<Image>();
        mySlider = go.GetComponentInChildren<Slider>();

        myfadeImage.gameObject.SetActive(false);
        mySlider.gameObject.SetActive(false);
    }

    public void StorySceneOn()
    {
        StartCoroutine("StartFadeFlow");
    }

    public IEnumerator StartFadeFlow()
    {
        startfadeImage.gameObject.SetActive(true);

        while (startfadeImage.rectTransform.localScale.x < circleScale)
        {
            time += Time.deltaTime / fadeTime;
            float x = Mathf.Lerp(0, circleScale, time);
            float y = Mathf.Lerp(0, circleScale, time);

            Vector3 temp = new Vector3(x, y, 1);

            startfadeImage.rectTransform.localScale = temp;

            yield return null;
        }

        fadeImage.gameObject.SetActive(true);
        startfadeImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        //crackingImage.gameObject.SetActive(true);
        startVideoImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        StartCoroutine("StoryTypeTextEffect");
    }


    public IEnumerator StoryTypeTextEffect(/*string text*/)
    {
        StopCoroutine("StartFadeFlow");

        skipImage.gameObject.SetActive(true);
        //textBackImage.gameObject.SetActive(true);

        storyText.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        string text;

        for (int i = 0; i < storyUI.story.Length; ++i)
        {
            text = storyUI.story[i].text;

            for (int j = 0; j < text.Length; j++)
            {
                stringBuilder.Append(text[j]);
                storyText.text = stringBuilder.ToString();
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1f);

            if (storyUI.story[i].delete == true)
            {
                stringBuilder.Clear();
                storyText.text = stringBuilder.ToString();
            }
        }

        startManager.SceneChange();
    }

    public IEnumerator FadeFlow(Image image)
    {
        image.gameObject.SetActive(true);
        Color alpha = image.color;
        time = 0f;

        Time.timeScale = 0.0f;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            image.color = alpha;
            yield return null;
        }

        time = 0f;

        Time.timeScale = 1f;

        yield return new WaitForSeconds(waitTime);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            image.color = alpha;
            yield return null;
        }

        image.gameObject.SetActive(false);
    }


    public void GameToMain()
    {
        StopAllCoroutines();
        StartCoroutine(CoLoadingScene("MainScene", false));
    }
    public void MainToGame()
    {
        StopAllCoroutines();
        StartCoroutine(CoLoadingScene("GameScene", true));
    }

    private IEnumerator SceneChangeFadeIn()
    {
        myfadeImage.gameObject.SetActive(true);

        Color alpha = myfadeImage.color;
        time = 0f;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / 1;
            alpha.a = Mathf.Lerp(0, 1, time);
            myfadeImage.color = alpha;
            yield return null;
        }

        mySlider.gameObject.SetActive(true);
    }

    public IEnumerator CoLoadingScene(string scnenName, bool gameScene)
    {
        yield return StartCoroutine("SceneChangeFadeIn");

        AsyncOperation loading = SceneManager.LoadSceneAsync(scnenName);

        while (!loading.isDone) //�� �ε� �Ϸ�� while���� ������
        {
            if (loading.progress >= 0.9f)
                mySlider.value = 1f;
            else
                mySlider.value = loading.progress;

            yield return new WaitForSecondsRealtime(0.1f);
        }

        mySlider.gameObject.SetActive(false);

        yield return StartCoroutine("SceneChangeFadeOut");
    }

    public IEnumerator SceneChangeFadeOut()
    {
        Color alpha = myfadeImage.color;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / 1;
            alpha.a = Mathf.Lerp(1, 0, time);
            myfadeImage.color = alpha;
            yield return null;
        }

        Time.timeScale = 1f;

        myfadeImage.gameObject.SetActive(false);
    }
}
