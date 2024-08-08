using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public void OnResult()
    {
        GameManager.Instance.UnitManager.ResultSetting(gameObject);        
    }
    public void OnResultSkip()
    {
        GameManager.Instance.UnitManager.ResultSetting();
    }
}
