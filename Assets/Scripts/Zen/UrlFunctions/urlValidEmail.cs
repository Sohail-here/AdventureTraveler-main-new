using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class urlValidEmail : MonoSingleton<urlValidEmail>
{
    public void ValidEmail(string email)
    {
        StartCoroutine(urlPOST_ValidEmail(email));
    }

    IEnumerator urlPOST_ValidEmail(string email)
    {
        Debug.Log("Get in UrlValidEmail");

        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/valid_email/" + email + "/enduser/", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/valid_email/" + email + "/enduser/", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("UrlValidEmail Error-- " + www.error + " -- " + www.downloadHandler.text);
            }
            else
            {
                //Auto Login
                AppleLogin.Instance.RegisterToLogin();
                //AppleLogin.Instance.BtnAppleLogin();
                Debug.Log("UrlValidEmail Success");
            }
        }
    }
}