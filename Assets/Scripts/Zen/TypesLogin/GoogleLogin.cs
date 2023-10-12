using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Intergration;

public class GoogleLogin : MonoBehaviour
{
    public static string GoogleToken;
    public static string GoogleEmail;

    public GameObject RegisterPanel;
    public GameObject LinkPanel;

    private bool LoginOK = false;

    private void Start()
    {
        //Keep logging to false in production
        LCGoogleLoginBridge.ChangeLoggingLevel(true);

        //For Android, You need to replace it with web client ID. Check details given in LCGoogleLoginBridge file or ReadMe file or tutorial video
        //For iOS, web client id will be used if you are using server auth. Normal iOS client id is picked up from Google-Services plist file. Its safe to pass null here for iOS if no server auth is used otherwise its ignored by the sdk

        LCGoogleLoginBridge.InitWithClientID("43333396819-8qhj9vbgta8092hkb66qekaloirq5ns1.apps.googleusercontent.com",
            "43333396819-hfdqbg9asfvecblhvjfdt5dv75evpa1p.apps.googleusercontent.com");
        PrintMessage("Google Login Initialized");
    }

    public void SignOutFromGoogle() { OnSignOut(); }

    public void OnSignIn()
    {
        Action<bool> logInCallBack = (Action<bool>)((loggedIn) =>
        {
            LoginOK = loggedIn;
            if (loggedIn)
            {
                GoogleToken = LCGoogleLoginBridge.GSIIDUserToken();
                GoogleEmail = LCGoogleLoginBridge.GSIEmail();
                PrintMessage("Google Login Success> " + LCGoogleLoginBridge.GSIUserDisplayName());

                Debug.Log("Google Token-- " + GoogleToken);

                try
                {
                    if (Login.FirstLogin)
                    {
                        StartCoroutine(C_LoginFirstTime());
                    }
                    else
                    {
                        urlLogin.Instance.Login("google", GoogleEmail, GoogleToken);
                    }
                }
                catch (Exception e)
                {
                    ULog.Zen.Log("Google Error-- " + e);
                }
            }
            else
            {
                PrintMessage("Google Login Failed");
            }
        });

        LCGoogleLoginBridge.LoginUser(logInCallBack, false);
    }

    IEnumerator C_LoginFirstTime()
    {
        yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();

        urlLogin.Instance.Login("google", GoogleEmail, GoogleToken);
    }

    public void UserToken()
    {
        PrintMessage("UserToken: " + LCGoogleLoginBridge.GSIIDUserToken());
    }

    void PrintMessage(string message)
    {
        //if (logsEnabled == false)
        {
            Debug.Log(message);
        }
        //statusText.text = message;
    }

    private void OnSignOut()
    {
        //AddToInformation("Calling SignOut");
        LCGoogleLoginBridge.LogoutUser();
    }
}