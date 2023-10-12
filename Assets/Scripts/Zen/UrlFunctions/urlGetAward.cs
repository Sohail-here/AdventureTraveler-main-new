using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlGetAward : MonoBehaviour
{
    public static urlGetAward getAward;

    public urlGetAward()
    {
        getAward = this;
    }

    private void Awake()
    {
        GetAward();
    }

    public void GetAward()
    {
        StartCoroutine(urlGET_GetAward());
    }

    public IEnumerator urlGET_GetAward()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/award/"))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/award/"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log("Error-- " + www.downloadHandler.text);
                UIManager.Instance.WarnPanelTitle("GetGoalsError");
                UIManager.Instance.WarnPanelContent("Please retry later!");
            }
            else
            {
                Debug.Log("GetGoalsSuccess-- " + www.downloadHandler.text);
                UnDestroyData.awardData = JsonUtility.FromJson<UnDestroyData.AwardData>(www.downloadHandler.text);                   
            }
        }
    }
}