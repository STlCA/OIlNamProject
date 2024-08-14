using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneEffect : Manager
{
    private StoryUI storyUI = null;
    private StartManager startManager = null;
    private DataTable_TIPLoader tipBase;

    //GameManager children이 아니면 fadeImage공용프리팹으로?

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
    private TMP_Text tipText = null;

    private float fadeTime = 1f;
    private float waitTime = 1f;
    private float time = 0f;
    private float circleScale = 100f;

    [HideInInspector]public TMP_Text storyText = null;//임시public

    private void Start()
    {
        if (GetComponent<StoryUI>() != null)
        {
            startManager = GetComponent<StartManager>();
            storyUI = GetComponent<StoryUI>();
            storyUI.Init();

            CanvasInit();
        }

        if (GameManager.Instance != null)
            tipBase = GameManager.Instance.DataManager.dataTable_TIPLoader;
    }

    public override void Init(GameManager gm)
    {
        base.Init(gm);

        myfadeImage = myCanvas.GetComponentInChildren<Image>();
        mySlider = myCanvas.GetComponentInChildren<Slider>();
        tipText = mySlider.GetComponentInChildren<TMP_Text>();

        myfadeImage.gameObject.SetActive(false);
        mySlider.gameObject.SetActive(false);
    }

    private void CanvasInit()
    {
        GameObject go = Instantiate(myCanvas);

        myfadeImage = go.GetComponentInChildren<Image>();
        mySlider = go.GetComponentInChildren<Slider>();
        tipText = mySlider.GetComponentInChildren<TMP_Text>();

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

        yield return new WaitForSeconds(3);

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

        int random = Random.Range(0,tipBase.ItemsList.Count);

        tipText.text = tipBase.ItemsList[random].Tip;
    }

    public IEnumerator CoLoadingScene(string scnenName, bool gameScene)
    {
        yield return StartCoroutine("SceneChangeFadeIn");

        float timer = 0.0f;

        AsyncOperation loading = SceneManager.LoadSceneAsync(scnenName);
        loading.allowSceneActivation = false;

        while (!loading.isDone) //씬 로딩 완료시 while문이 나가짐
        {
            timer += Time.deltaTime;

            if (loading.progress >= 0.9f)
                mySlider.value = 0.8f;
            else
                mySlider.value = Mathf.Lerp(mySlider.value, loading.progress, timer);

            if (loading.progress >= 0.9f && timer > 2f)
            {
                mySlider.value = 1f;
                //whiteImage.gameObject.SetActive(true);
                //yield return StartCoroutine("SceneChangeFadeOut");
                loading.allowSceneActivation = true;
            }

            yield return null;
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
