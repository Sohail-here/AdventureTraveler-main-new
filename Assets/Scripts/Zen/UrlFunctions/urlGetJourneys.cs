using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetJourneys : MonoBehaviour
{
    public static urlGetJourneys getJourneys;

    public urlGetJourneys()
    {
        getJourneys = this;
    }

    public bool me;
    public int page;

    public IEnumerator urlGET_GetJourneys()
    {
        WWWForm form = new WWWForm();

        //using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/journeys/" + page.ToString() + "/?me=" + me.ToString()))
        using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/journeys/" + page.ToString() + "/?me=" + me.ToString()))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(www.error);
                //Debug.Log("Error-- " + www.downloadHandler.text);
                UIManager.Instance.WarnPanelTitle("GetJourneysError");
                UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                UnDestroyData.journeys = JsonUtility.FromJson<UnDestroyData.Journeys>(www.downloadHandler.text);
            }
        }
    }
}