using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//TODO : CharacterController

public class UserNameInput : MonoBehaviour
{
    public TMP_Text userName;
    public TMP_InputField nameInputField;

    public void SetUserName()
    {
        if (nameInputField.text.Length > 0)
        {
            userName.text = nameInputField.text;
            nameInputField.text = "";
        }
        else
            return;

        //TODO : SaveSystemø° ¿˙¿Â
    }
}
