using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlDel_DeletAllJourneys : MonoSingleton<urlDel_DeletAllJourneys>
{ 
    public void DeleteAllJourneys()
    {
        StartCoroutine(urlDeleteAllJourneys());
    }

    IEnumerator urlDeleteAllJourneys()
    {
        //using (UnityWebRequest www = UnityWebRequest.Delete("https://api.i911adventure.com/delete_all_journey/"))
        using (UnityWebRequest www = UnityWebRequest.Delete("https://staging.i911adventure.com/delete_all_journey/"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("DeleteAllJourneys Error");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("DeleteAllJourneysSuccess");
            }
        }
    }
}
