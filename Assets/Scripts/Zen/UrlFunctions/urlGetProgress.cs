using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetProgress : MonoBehaviour
{
    public static urlGetProgress getProgress;

    public urlGetProgress()
    {
        getProgress = this;
    }

    public void GetProgress()
    {
        StartCoroutine(urlGET_GetProgress());
    }

    public IEnumerator urlGET_GetProgress()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/progress/"))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/progress/"))
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
                UnDestroyData.progressData = JsonUtility.FromJson<UnDestroyData.ProgressData>(www.downloadHandler.text);
            }
        }
    }
}