using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public BossEffectType type;

    public List<GameObject> bossEffects = new();
    public List<GameObject> lethalEffects = new();

    private void Start()
    {
        if (type == BossEffectType.Boss)
            StartCoroutine("CoBossEffectsOn");
        else if (type == BossEffectType.Lethal)
            lethalEffects[0].SetActive(true);
        else
            Debug.Log("ÀÌÆåÆ®È¿°ú¾ÈµÊ");
    }

    private IEnumerator CoBossEffectsOn()
    {
        bossEffects[0].SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        bossEffects[1].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        bossEffects[2].SetActive(true);
        yield return new WaitForSecondsRealtime(2);
    }
}
