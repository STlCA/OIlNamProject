using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneEffect : MonoBehaviour
{
    private StoryUI storyUI = null;
    private StartManager startManager = null;

    [Header("StartScene")]
    public Image startfadeImage = null;
    public Image fadeImage = null;
    public Image crackingImage = null;
    public Image textBackImage = null;
    public Image skipImage = null;

    private float fadeTime = 1f;
    private float waitTime = 1f;
    private float time = 0f;
    private float circleScale = 100f;

    public TMP_Text storyText = null;//¿”Ω√public


    private void Start()
    {
        if (GetComponent<StoryUI>() != null)
        {
            startManager = GetComponent<StartManager>();
            storyUI = GetComponent<StoryUI>();
            storyUI.Init();
        }
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

        crackingImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        StartCoroutine("StoryTypeTextEffect");
    }

    public IEnumerator FadeFlow()
    {
        fadeImage.gameObject.SetActive(true);
        Color alpha = fadeImage.color;
        time = 0f;

        Time.timeScale = 0.0f;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = alpha;
            yield return null;
        }

        time = 0f;

        Time.timeScale = 1f;

        yield return new WaitForSeconds(waitTime);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = alpha;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    /*    public void TypeEffectON(TMP_Text text)
        {
            storyText = text;
        }*/

    public IEnumerator StoryTypeTextEffect(/*string text*/)
    {
        StopCoroutine("StartFadeFlow");

        skipImage.gameObject.SetActive(true);
        textBackImage.gameObject.SetActive(true);

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
}
