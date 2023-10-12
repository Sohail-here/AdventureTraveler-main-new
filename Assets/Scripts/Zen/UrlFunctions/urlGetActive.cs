using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetActive : MonoBehaviour
{
    // {{host}}/active/{{active_id}}/
    public urlGetActive getActive;

    public urlGetActive()
    {
        getActive = this;
    }

    private int activeID;

    public void GetActive()
    {
        StartCoroutine(urlGET_GetActive());
    }

    IEnumerator urlGET_GetActive()
    {
        WWWForm form = new WWWForm();

        //using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/active/" + activeID.ToString()))
        using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/active/" + activeID.ToString()))
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