using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Manager
{
    public TMP_Text userNameTxt;
    public TMP_InputField nameInputField;

    private int level;
    private string userName;
    private float exp;



    public void SetUserName()
    {
        if (nameInputField.text.Length > 0)
        {
            userNameTxt.text = nameInputField.text;
            nameInputField.text = "";
        }
        else
            return;

        //TODO : SaveSystemø° ¿˙¿Â
    }

}