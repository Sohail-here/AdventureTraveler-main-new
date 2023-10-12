using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Intergration;
using System.IO;
using Newtonsoft.Json;

public class AppleLogin : MonoSingleton<AppleLogin>
{
    private IAppleAuthManager appleAuthManager;

    private const string APPLE_USER_ID_KEY = "AppleUserId";
    private const string APPLE_USER_TOKEN = "AppleUserToken";
    private const string APPLE_USER_EMAIL_KEY = "AppleUserEMail";
    public static string AppleEmail;
    public static string AppleToken;
    public static string AppleUserID = "";

    public static string AppleGivenName;
    public static string AppleNickName;
    public static string AppleFamilyName;
    public RegisterPanel RegisterP;

    void Start()
    {
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this.appleAuthManager = new AppleAuthManager(deserializer);
        }
    }

    void Update()
    {
        // Updates the AppleAuthManager instance to execute
        // pending callbacks inside Unity's execution loop
        if (this.appleAuthManager != null)
        {
            this.appleAuthManager.Update();
        }
    }

    public void QuickLogin()
    {
        if (PlayerPrefs.HasKey(AppleUserID + APPLE_USER_EMAIL_KEY) && PlayerPrefs.HasKey(AppleUserID + APPLE_USER_TOKEN))
        {
            ULog.Penguin.Log("Log Apple Quick Login");
            urlLogin.Instance.Login("apple", AppleEmail, PlayerPrefs.GetString(AppleUserID + APPLE_USER_TOKEN));
            return;
        }
        else
        {
            ULog.Penguin.LogError($" Login Fail ");
        }
    }

    public void BtnAppleLogin()
    {
        ULog.Penguin.Log($"Apple Login Version 20220505");
#if UNITY_ANDROID
        UIManager.Instance.WarnPanelTitle("Login Error!");
        UIManager.Instance.WarnPanelContent("Android can't use Apple login!");
#endif

#if UNITY_IOS || true

        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);
        appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                ULog.Penguin.Log("Credential Success!");
                // Obtained credential, cast it to IAppleIDCredential
                var appleIdCredential = credential as IAppleIDCredential;
                string email = "", identityToken = "", givenName = "", familyName = "", middleName = "", nickName = "", gFullName = "";
                IPersonName fullName;
                bool hasChangeAppleID = false;
                if (appleIdCredential != null)
                {
                    // Apple User ID
                    // You should save the user ID somewhere in the device
                    AppleUserID = appleIdCredential.User;
                    hasChangeAppleID = PlayerPrefs.HasKey(APPLE_USER_ID_KEY) && PlayerPrefs.GetString(APPLE_USER_ID_KEY) != AppleUserID;

                    ULog.Penguin.Log("AppleUserID -- " + AppleUserID + " - " + hasChangeAppleID);
                    // Identity token
                    identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);

                    // Email (Received ONLY in the first login)
                    if (!string.IsNullOrEmpty(appleIdCredential.Email))
                        email = appleIdCredential.Email;
                    else
                    {
                        string[] strs = identityToken.Split('.');
                        if (strs.Length > 1)
                            email = DecodeAppleEmail(strs[1]);
                        else
                            ULog.Penguin.Log($"Error strs.Length not match : {strs.Length}");
                    }

                    ULog.Penguin.Log("email -- " + email);
                    // Authorization code
                    var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);

                    if (appleIdCredential.FullName != null)
                    {
                        ULog.Penguin.Log("The Account First login");
                        // Full name (Received ONLY in the first login)
                        fullName = appleIdCredential.FullName;
                        givenName = string.IsNullOrEmpty(fullName.GivenName) ? "" : fullName.GivenName;
                        familyName = string.IsNullOrEmpty(fullName.FamilyName) ? "" : fullName.FamilyName;
                        middleName = string.IsNullOrEmpty(fullName.MiddleName) ? "" : fullName.MiddleName;
                        nickName = string.IsNullOrEmpty(fullName.Nickname) ? "" : fullName.Nickname;
                        gFullName = $"GivenName:{givenName} FamilyName:{familyName} MiddleName:{middleName} Nickname:{nickName}";
                        hasChangeAppleID = true;
                    }
                    else
                        ULog.Penguin.Log("FullName Is Null ");



                    ULog.Penguin.Log($"First Get Email : {appleIdCredential.Email}");
                    ULog.Penguin.Log($"First Get Code : {appleIdCredential.AuthorizationCode}");
                    ULog.Penguin.Log($"credential {authorizationCode} : {identityToken} : {gFullName} : {appleIdCredential.Email} : {appleIdCredential.AuthorizationCode} : {appleIdCredential.IdentityToken} : {appleIdCredential.RealUserStatus} : {appleIdCredential.ToString()}");

                }

                if (!hasChangeAppleID && PlayerPrefs.HasKey(APPLE_USER_ID_KEY))
                {
                    ULog.Penguin.Log($"Load Apple PlayerPrefs");
                    givenName = PlayerPrefs.GetString(AppleUserID + "_GNAME");
                    familyName = PlayerPrefs.GetString(AppleUserID + "_FNAME");
                    nickName = PlayerPrefs.GetString(AppleUserID + "_NNAME");

                    identityToken = PlayerPrefs.GetString(AppleUserID + APPLE_USER_TOKEN);
                }
                else
                {
                    ULog.Penguin.Log($"Save Apple PlayerPrefs");
                    PlayerPrefs.SetString(APPLE_USER_ID_KEY, AppleUserID);
                    PlayerPrefs.SetString(AppleUserID + "_GNAME", givenName);
                    PlayerPrefs.SetString(AppleUserID + "_FNAME", familyName);
                    PlayerPrefs.SetString(AppleUserID + "_NNAME", nickName);
                    PlayerPrefs.SetString(AppleUserID + APPLE_USER_EMAIL_KEY, string.IsNullOrEmpty(email) ? appleIdCredential.Email : email);
                    PlayerPrefs.SetString(AppleUserID + APPLE_USER_TOKEN, identityToken);
                }

                if (!string.IsNullOrEmpty(email))
                    AppleEmail = email;
                else if (!string.IsNullOrEmpty(PlayerPrefs.GetString(AppleUserID + APPLE_USER_EMAIL_KEY, "")))
                    AppleEmail = PlayerPrefs.GetString(AppleUserID + APPLE_USER_EMAIL_KEY, "");
                else
                    ULog.Penguin.Log($"Apple Mail All Not Get");

                AppleToken = identityToken;
                if (RegisterP != null)
                {
                    RegisterP.FirstNameInput.Text = givenName;
                    RegisterP.LastNameInput.Text = familyName;
                }
                AppleGivenName = givenName;
                AppleFamilyName = familyName;
                string userN = string.IsNullOrEmpty(nickName) ? $"{givenName}{familyName}" : nickName;
                if (RegisterP != null)
                    RegisterP.UserNameInput.Text = userN;
                AppleNickName = userN;
                ULog.Penguin.Log($"Has Key Check ID:{PlayerPrefs.HasKey(APPLE_USER_ID_KEY)} Email:{PlayerPrefs.HasKey(AppleUserID + APPLE_USER_EMAIL_KEY)} Token:{PlayerPrefs.HasKey(AppleUserID + APPLE_USER_TOKEN)} !");
                ULog.Penguin.Log($"Check Key ID:{PlayerPrefs.GetString(APPLE_USER_ID_KEY, "Not Any ID")} Email:{PlayerPrefs.GetString(AppleUserID + APPLE_USER_EMAIL_KEY, "Not Any Mail!")} Token:{PlayerPrefs.GetString(AppleUserID + APPLE_USER_TOKEN, "Not Any Token!")} !");
                ULog.Penguin.Log($"AppleEmail : {AppleEmail} | AppleToken : {AppleToken}");
                if (!hasChangeAppleID)
                {
                    if (PlayerPrefs.HasKey(AppleUserID + APPLE_USER_EMAIL_KEY) &&
                        PlayerPrefs.HasKey(AppleUserID + APPLE_USER_TOKEN) &&
                        !string.IsNullOrEmpty(PlayerPrefs.GetString(AppleUserID + APPLE_USER_EMAIL_KEY)) &&
                        !string.IsNullOrEmpty(PlayerPrefs.GetString(AppleUserID + APPLE_USER_TOKEN)))
                    {
                        ULog.Penguin.Log($"ID {PlayerPrefs.GetString(APPLE_USER_ID_KEY)}");
                        ULog.Penguin.Log($"Email {PlayerPrefs.GetString(AppleUserID + APPLE_USER_EMAIL_KEY)}");
                        ULog.Penguin.Log($"Token {PlayerPrefs.GetString(AppleUserID + APPLE_USER_TOKEN)}");
                        if (AppleEmail.IndexOf('@') > -1)
                        {
                            CheckCredentialStatusForUserId(identityToken);
                            return;
                        }
                    }
                }

                try
                {
                    StartCoroutine(C_LoginFirstTime(AppleEmail, AppleToken));
                }
                catch (Exception e)
                {
                    ULog.Penguin.Log("Exception:" + e.ToString());
                }
            },
            error =>
            {
                // Something went wrong
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                ULog.Penguin.Log($"Error {error.GetAuthorizationErrorCode()}");
            });

#endif
    }

    private string DecodeAppleEmail(string toDecode)
    {
        string email = "";
        try
        {
            var s = Base64DecodeToString(toDecode);
            AppleData data = JsonConvert.DeserializeObject<AppleData>(s);
            email = data.email;
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log($"Error {ex.ToString()}");
        }

        return email;
    }

    private string Base64DecodeToString(string toDecode)
    {
        string decodePrepped = toDecode.Replace("-", "+").Replace("_", "/");

        switch (decodePrepped.Length % 4)
        {
            case 0:
                break;
            case 2:
                decodePrepped += "==";
                break;
            case 3:
                decodePrepped += "=";
                break;
            default:
                throw new Exception("Not a legal base64 string!");
        }

        byte[] data = Convert.FromBase64String(decodePrepped);
        return System.Text.Encoding.UTF8.GetString(data);
    }

    private void DeletePrefs()
    {
        PlayerPrefs.DeleteKey(APPLE_USER_ID_KEY);
        PlayerPrefs.DeleteKey(AppleUserID + APPLE_USER_EMAIL_KEY);
        PlayerPrefs.DeleteKey(AppleUserID + APPLE_USER_TOKEN);
    }

    public void RegisterToLogin()
    {
        Intergration.Login.FirstLogin = true;
        urlLogin.Instance.SetAppleDefalutData();
        CheckCredentialStatusForUserId(AppleToken);
    }

    public void CheckCredentialStatusForUserId(string appleUserId)
    {
        // If there is an apple ID available, we should check the credential state
        this.appleAuthManager.GetCredentialState(
            appleUserId,
            state =>
            {
                switch (state)
                {
                    // If it's authorized, login with that user id
                    case CredentialState.Authorized:
                        ULog.Penguin.Log($"CredentialState is Authorized");
                        QuickLogin();
                        break;
                    // If it was revoked, or not found, we need a new sign in with apple attempt
                    // Discard previous apple user id
                    case CredentialState.Revoked:
                    case CredentialState.NotFound:
                        ULog.Penguin.Log($"CredentialState is Revoked | NotFound");
                        StartCoroutine(C_LoginFirstTime(AppleEmail, AppleToken));
                        break;
                }
            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                ULog.Penguin.Log("Error while trying to get credential state " + authorizationErrorCode.ToString() + " " + error.ToString());
                StartCoroutine(C_LoginFirstTime(AppleEmail, AppleToken));
            });
    }


    IEnumerator C_LoginFirstTime(string email, string token)
    {

        yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();

        urlLogin.Instance.Login("apple", email, token);
    }

    public class AppleData
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string sub { get; set; }
        public string c_hash { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public int auth_time { get; set; }
        public bool nonce_supported { get; set; }
    }
}