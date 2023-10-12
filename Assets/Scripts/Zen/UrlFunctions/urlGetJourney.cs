using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetJourney : MonoBehaviour
{
    public static urlGetJourney getJourney;

    public int journeyID;

    public urlGetJourney()
    {
        getJourney = this;
    }

    public void GetJourney()
    {
        StartCoroutine(urlGET_GetJourney());
    }

    IEnumerator urlGET_GetJourney()
    {
        WWWForm form = new WWWForm();

        //using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/journey/" + journeyID.ToString()))
        using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/journey/" + journeyID.ToString()))
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
