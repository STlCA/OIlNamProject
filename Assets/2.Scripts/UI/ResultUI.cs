using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public void OnResult()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(13);
        GameManager.Instance.UnitManager.ResultSetting(gameObject);        
    }
    public void OnResultSkip()
    {
        GameManager.Instance.SoundManager.EffectAudioClipPlay(13);
        GameManager.Instance.UnitManager.ResultSetting();
    }
}
