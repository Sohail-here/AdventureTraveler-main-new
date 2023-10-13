using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Facebook.Unity;
using Intergration;

public class FacebookLogin : MonoBehaviour
{  
    public static string FacebookEmail;
    public static string FacebookToken;

    public GameObject RegisterPanel;
    public GameObject LinkPanel;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            //var aToken = AccessToken.CurrentAccessToken;
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

            // Print current access token's User ID
            Debug.Log("UserID-- " + aToken.UserId);

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            Debug.Log("Token-- " + aToken.TokenString);

            FacebookToken = aToken.TokenString;

            FB.API("/me?fields=id,name,email", HttpMethod.GET, GetFacebookInfo);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void FBLogin()
    {
        var perms = new List<string>() { "public_profile", "email" };
        //var perms = new List<string>() { "email", "publish_actions" };
        FB.LogInWithReadPermissions(perms, AuthCallBack);
    }

    public void GetFacebookInfo(IResult result)
    {
        if (result.Error == null)
        {
            FacebookEmail = result.ResultDictionary["email"].ToString();

            // Login function is here!
            try
            {
                if (Login.FirstLogin)
                {
                    StartCoroutine(C_LoginFirstTime());
                }
                else
                {
                    urlLogin.Instance.Login("facebook", FacebookEmail, FacebookToken);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            Debug.Log("Login With Facebook");
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    IEnumerator C_LoginFirstTime()
    {
        yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();

        urlLogin.Instance.Login("facebook", FacebookEmail, FacebookToken);
    }
    
}