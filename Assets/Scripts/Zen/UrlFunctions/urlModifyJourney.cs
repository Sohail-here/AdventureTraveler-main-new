using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlModifyJourney : MonoBehaviour
{
    public static urlModifyJourney modifyJourney;

    public urlModifyJourney()
    {
        modifyJourney = this;
    }

    public void ModifyJourneyData()
    {
        StartCoroutine(urlPUT_ModifyJourney());
    }

    IEnumerator urlPUT_ModifyJourney()
    {
        WWWForm form = new WWWForm();

        byte[] data = System.Text.Encoding.UTF8.GetBytes(UnDestroyData.journeyData.ToString());

        //using (UnityWebRequest www = UnityWebRequest.Put("https://api.i911adventure.com/journey/", data))
        using (UnityWebRequest www = UnityWebRequest.Put("https://staging.i911adventure.com/journey/", data))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("ModifyJourney Error");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                UIManager.Instance.WarnPanelTitle("Modify Journey Success!");
                UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
            }
        }
    }
}