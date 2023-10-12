using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetActives : MonoBehaviour
{
    public urlGetActives getActives;

    public urlGetActives()
    {
        getActives = this;
    }

    private int activeNumber;
    private bool isOne;

    public void GetActives()
    {
        StartCoroutine(urlGET_GetActives());
    }

    IEnumerator urlGET_GetActives()
    {
        WWWForm form = new WWWForm();

        //using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/actives/" + activeNumber.ToString() + "?me=" + isOne.ToString()))
        using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/actives/" + activeNumber.ToString() + "?me=" + isOne.ToString()))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log("Error-- " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Success-- " + www.downloadHandler.text);
            }
        }
    }
}