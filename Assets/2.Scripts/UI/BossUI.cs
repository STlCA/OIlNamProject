using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public List<GameObject> effects = new();

    private void Start()
    {
        StartCoroutine("CoEffectsOn");
    }

    private IEnumerator CoEffectsOn()
    {
        effects[0].SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        effects[1].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        effects[2].SetActive(true);
        yield return new WaitForSecondsRealtime(2);
    }
}
