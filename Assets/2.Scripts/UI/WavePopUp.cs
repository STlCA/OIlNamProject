using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePopUp : MonoBehaviour
{
    [SerializeField] private GameObject wavePopUp;
    [SerializeField] private GameObject background;
    [SerializeField] private TMP_Text message;

    private Image image;

    public IEnumerator PopUp(string message, Color color)
    {
        image = background.GetComponent<Image>();
        image.color = color;
        this.message.text = message;
        GameManager.Instance.PopUpController.UIOn(wavePopUp);

        yield return new WaitForSeconds(1.0f);

        GameManager.Instance.PopUpController.UIOff(wavePopUp);
    }
}
