using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoogleManger : MonoBehaviour
{
    public TextMeshProUGUI logText;

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play GamesServices
            //Perfectly login success

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            logText.text = "Success \n" + name;
        }
        else
        {
            logText.text = "Sign in Failed!";

            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. clicking it should call
            //playgamesplatform.instance.mauallyauthenticate(processAuthentication).
            //login failed
        }
    }
}
