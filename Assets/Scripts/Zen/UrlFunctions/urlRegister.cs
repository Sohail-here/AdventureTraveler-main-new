using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

/// <summary>
/// register
/// valid email
/// </summary>
public class urlRegister : MonoSingleton<urlRegister>
{
    public GameObject PanelRegister;

    // This is first register panel, so we don't need to close it.
    //public GameObject PanelCreateName;
    public GameObject PanelCreateAccount;
    public GameObject PanelMedical;    

    public IEnumerator urlPOST_Register()
    {       
        WWWForm form = new WWWForm();

        Debug.Log("Email-- " + UnDestroyData.accountData.email);
        Debug.Log("Password-- " + UnDestroyData.accountData.password);

        form.AddField("email", UnDestroyData.accountData.email);
        form.AddField("type", "adventuretraveler");
        form.AddField("password", UnDestroyData.accountData.password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/register/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/register/", form))
        {
            UIManager.Instance.OpenWaitingPanel();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.CloseWaitingPanel();

                UIManager.Instance.WarnPanelTitle("Error Register");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);

                Debug.Log("Error Email Register-- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("register-- " + www.downloadHandler.text);

                UIManager.Instance.CloseWaitingPanel();

                PanelRegister.SetActive(false);
                PanelCreateAccount.SetActive(false);
                PanelMedical.SetActive(false);

                UIManager.Instance.WarnPanelTitle("Please Verify Email");
                UIManager.Instance.WarnPanelContent("A verfication link has been sent" + "\n" + "to your email" + "\n" + "\n" + "Please active the validation link" + "\n" + "to continue account creation");
            }
        }
    }

    public IEnumerator urlPOST_GoogleRegister()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", GoogleLogin.GoogleEmail);
        form.AddField("type", "google");
        form.AddField("token", GoogleLogin.GoogleToken);

        Debug.Log(GoogleLogin.GoogleEmail);
        Debug.Log(GoogleLogin.GoogleToken);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/register/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/register/", form))
        {
            UIManager.Instance.OpenWaitingPanel();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.CloseWaitingPanel();

                UIManager.Instance.WarnPanelTitle("Error Google Register");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);

                Debug.Log("Error Google Register-- " + www.error + " -- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Google Register-- " + www.downloadHandler.text);

                UIManager.Instance.CloseWaitingPanel();

                PanelRegister.SetActive(false);
                PanelCreateAccount.SetActive(false);
                PanelMedical.SetActive(false);

                urlValidEmail.Instance.ValidEmail(GoogleLogin.GoogleEmail);

                UIManager.Instance.WarnPanelTitle("Please Verify Email");
                UIManager.Instance.WarnPanelContent("A verfication link has been sent" + "\n" + "to your email" + "\n" + "\n" + "Please active the validation link" + "\n" + "to continue account creation");

            }
        }
    }

    public IEnumerator urlPOST_FacebookRegister()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", FacebookLogin.FacebookEmail);
        form.AddField("type", "facebook");
        form.AddField("token", FacebookLogin.FacebookToken);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/register/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/register/", form))
        {
            UIManager.Instance.OpenWaitingPanel();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.CloseWaitingPanel();

                UIManager.Instance.WarnPanelTitle("Error Facebook Register");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);

                Debug.Log("Error Facebook Register-- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Facebook Register-- " + www.downloadHandler.text);

                UIManager.Instance.CloseWaitingPanel();

                PanelRegister.SetActive(false);
                PanelCreateAccount.SetActive(false);
                PanelMedical.SetActive(false);

                urlValidEmail.Instance.ValidEmail(FacebookLogin.FacebookEmail);

                UIManager.Instance.WarnPanelTitle("Please Verify Email");
                UIManager.Instance.WarnPanelContent("A verfication link has been sent" + "\n" + "to your email" + "\n" + "\n" + "Please active the validation link" + "\n" + "to continue account creation");
            }
        }
    }

    public IEnumerator urlPOST_AppleRegister()
    {
        WWWForm form = new WWWForm();
        ULog.Penguin.Log($"AppleMail:{AppleLogin.AppleEmail} Token:{AppleLogin.AppleToken}");
        form.AddField("email", AppleLogin.AppleEmail);
        form.AddField("type", "apple");
        form.AddField("token", AppleLogin.AppleToken);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/register/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/register/", form))
        {
            UIManager.Instance.OpenWaitingPanel();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.CloseWaitingPanel();

                UIManager.Instance.WarnPanelTitle("Error Apple Register");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);

                Debug.Log("Error Apple Register-- " + www.downloadHandler.text);
            }
            else
            {
                //UIManager.Instance.CloseWaitingPanel();

                PanelRegister.SetActive(false);
                PanelCreateAccount.SetActive(false);
                PanelMedical.SetActive(false);

                urlValidEmail.Instance.ValidEmail(AppleLogin.AppleEmail);

                //UIManager.Instance.WarnPanelTitle("Please Verify Email");
                //UIManager.Instance.WarnPanelContent("A verfication link has been sent" + "\n" + "to your email" + "\n" + "\n" + "Please active the validation link" + "\n" + "to continue account creation");
            }
        }
    }
}