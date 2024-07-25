using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    private TMP_Text timeTxt;

    private float time = 0f;

    private void Start()
    {
        timeTxt = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("00:00");
    }



}
