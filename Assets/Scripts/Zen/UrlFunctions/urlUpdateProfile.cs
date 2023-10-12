using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class urlUpdateProfile : MonoSingleton<urlUpdateProfile>
{
    public void UpdateProfile()
    {
        StartCoroutine(urlPUT_UpdateProfile());
    }

    public IEnumerator urlPUT_UpdateProfile()
    {
        string data = JsonUtility.ToJson(UnDestroyData._UpLoadUserData);
        Debug.Log("UpdateProfile : " + data + " | " + UnDestroyData.token);

        using (UnityWebRequest www = UnityWebRequest.Put("https://api.i911adventure.com/my_profile/", data))
        //using (UnityWebRequest www = UnityWebRequest.Put("https://staging.i911adventure.com/my_profile/", data))
        {
            UIManager.Instance.OpenWaitingPanel();

            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            UIManager.Instance.CloseWaitingPanel();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("UrlUpdateProfile Error-- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("UrlUpdateProfile Suceess-- " + www.downloadHandler.text);
            }
        }
    }
}