using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class urlLogin : MonoSingleton<urlLogin>
{
    public GameObject RegisterPanel;
    public GameObject LinkPanel;

    /// <summary>
    /// Call this function to login, key is password or token.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="email"></param>
    /// <param name="token"></param>
    public void Login(string type, string email, string key)
    {
        ULog.Penguin.Log($"Login Type:{type}\nEmail:{email}\nKey:{key}");
        StartCoroutine(C_UrlLogin(type, email, key));
    }

    [ContextMenu("Open Debug UI")]
    IEnumerator C_UrlLogin(string type, string email, string key)
    {
        WWWForm form = new WWWForm();
        if (type == "email")
        {
            form.AddField("email", email);
            form.AddField("type", "adventuretraveler");
            form.AddField("password", key);
        }
        else
        {
            form.AddField("email", email);
            form.AddField("type", type);
            form.AddField("token", key);
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/login/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/login/", form))
        {
            UIManager.Instance.OpenWaitingPanel();

            ULog.Zen.Log("OpenWaitingPanel");

            Debug.Log("Email-- " + email);
            Debug.Log("Type-- " + type);
            Debug.Log("Token-- " + key);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.CloseWaitingPanel();

                //Test.Instance.DebugPrint("UrlLoginError-- " + www.downloadHandler.text);

                string errorContent = JsonUtility.FromJson<UnDestroyData.ErrorMsg>(www.downloadHandler.text).msg;
                //Test.Instance.DebugPrint("ErrorContent-- " + errorContent);

                if (type == "email")
                {
                    if (errorContent == "Cannot Find User")
                    {
                        UIManager.Instance.WarnPanelTitle("Incorrect login");
                        UIManager.Instance.WarnPanelContent("We were unable to login with" + "\n" + "the email and password you" + "\n" + "have provided. Please try again. ");
                    }
                    else
                    {
                        UIManager.Instance.WarnPanelTitle("Login Error");
                        UIManager.Instance.WarnPanelContent(errorContent);

                        if (errorContent == "account is exiting, try to login in different ways")
                        {
                            StartCoroutine(C_UrlLinkAccount(email));
                        }
                    }
                }
                else
                {
                    ULog.Zen.Log("Type Login Error : " + errorContent);

                    if (errorContent == "cannot find user")
                    {
                        if (type == "apple")
                        {
                            SetAppleDefalutData();
                            ULog.Penguin.Log("Update Apple User Data Done");
                            yield return urlRegister.Instance.urlPOST_AppleRegister();
                            ULog.Penguin.Log("Apple Register");
                        }
                        else
                            RegisterPanel.SetActive(true);
                    }
                    // This email is already in server with other login type.
                    else if (errorContent == "system error")
                    {
                        LinkPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "The email " + "\"" + email + "\"" + "\n" + "is already in our system";
                        LinkPanel.SetActive(true);
                    }
                    // This email isn't in server
                    else if (errorContent == "google token error")
                    {
                        Debug.Log("ErrorContent == Google token error");

                        RegisterPanel.SetActive(true);
                    }
                    else if (errorContent == "facebook token error")
                    {
                        Debug.Log("ErrorContent == Facebook token error");

                        RegisterPanel.SetActive(true);
                    }
                    else if (errorContent == "apple token error")
                    {
                        Debug.Log("ErrorContent == Apple token error");

                        //RegisterPanel.SetActive(true);
                        SetAppleDefalutData();
                        StartCoroutine(urlRegister.Instance.urlPOST_AppleRegister());
                    }
                    else if (errorContent == "account is exiting, try to login in different ways")
                    {
                        StartCoroutine(C_UrlLinkAccount(email));
                    }
                    else
                    {
                        UIManager.Instance.WarnPanelTitle("Incorrect login");
                        UIManager.Instance.WarnPanelContent("We were unable to login with" + "\n" + "the email and password you" + "\n" + "have provided. Please try again. ");
                    }
                }
            }
            else
            {
                Debug.Log("Type Login Success");

                UnDestroyData.token = JsonUtility.FromJson<UnDestroyData.data_token>(www.downloadHandler.text).token;

                ULog.Penguin.Log("token" + UnDestroyData.token);

                if (Intergration.Login.FirstLogin)
                {
                    ULog.Penguin.Log("FirstLogin --- Update Profile");
                    yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();
                }

                urlGetProfile.getProfile.GetProfile();
            }
        }
    }

    public void SetAppleDefalutData()
    {
        string defaultName = "Adventurer#" + UnityEngine.Random.Range(0, 10000).ToString("#0000");
        UnDestroyData._UpLoadUserData.user_name = string.IsNullOrEmpty(AppleLogin.AppleNickName) ? defaultName : AppleLogin.AppleNickName;
        UnDestroyData._UpLoadUserData.first_name = string.IsNullOrEmpty(AppleLogin.AppleGivenName) ? defaultName : AppleLogin.AppleGivenName;
        UnDestroyData._UpLoadUserData.last_name = string.IsNullOrEmpty(AppleLogin.AppleFamilyName) ? defaultName : AppleLogin.AppleFamilyName;

        UnDestroyData._UpLoadUserData.birthday_day = "1980-01-01";
        UnDestroyData._UpLoadUserData.gender = "Male";
        UnDestroyData._UpLoadUserData.height = "4' 00";
        UnDestroyData._UpLoadUserData.weight = "165 Pounds​";
        UnDestroyData._UpLoadUserData.blood_type = "O -";

        UnDestroyData._UpLoadUserData.emergency_contact_number = "00000000000";
        UnDestroyData._UpLoadUserData.medical_allergies = "None";
        UnDestroyData._UpLoadUserData.list_of_medical_conditions = "None";
        UnDestroyData._UpLoadUserData.additional_notes = "None";
    }

    IEnumerator C_UrlLinkAccount(string email)
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/apply_link/" + email + "/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/apply_link/" + email + "/", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("LinkAccountError-- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("LinkAccountSuccess-- " + www.downloadHandler.text);

                UIManager.Instance.WarnPanelTitle("Account Link Success!");
                UIManager.Instance.WarnPanelContent("This account is exist" + "\n" + "and it has been linked!" + "\n" + "Please login again!");
            }
        }
    }
}