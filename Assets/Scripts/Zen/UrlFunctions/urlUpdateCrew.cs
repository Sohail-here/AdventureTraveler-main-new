using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class urlUpdateCrew : MonoSingleton<urlUpdateCrew>
{
    public void UpdateCrew()
    {
        Debug.Log("Save Crew info to PlayerPferf");
 

        PlayerPrefs.SetString("emergencyContactName1", UnDestroyData._CrewData.emergencyContactName1);
        PlayerPrefs.SetString("contactName1", UnDestroyData._CrewData.contactName1);
        PlayerPrefs.SetString("drpRelationType1", UnDestroyData._CrewData.drpRelationType1);

        PlayerPrefs.SetString("emergencyContactName2", UnDestroyData._CrewData.emergencyContactName2);
        PlayerPrefs.SetString("contactName2", UnDestroyData._CrewData.contactName2);
        PlayerPrefs.SetString("drpRelationType2", UnDestroyData._CrewData.drpRelationType2);

        PlayerPrefs.SetString("emergencyContactName3", UnDestroyData._CrewData.emergencyContactName3);
        PlayerPrefs.SetString("contactName3", UnDestroyData._CrewData.contactName3);
        PlayerPrefs.SetString("drpRelationType3", UnDestroyData._CrewData.drpRelationType3);

      
        PlayerPrefs.SetString("myCrewAdditionalNotes", UnDestroyData._CrewData.myCrewAdditionalNotes);
        PlayerPrefs.Save();

        UIManager.Instance.WarnPanelTitle("Data Saved!");
        UIManager.Instance.WarnPanelContent("Your information has been successfully stored.");
        //StartCoroutine(urlPUT_UpdateBoater());
    }

    public IEnumerator urlPUT_UpdateCrew()
    {
        string data = JsonUtility.ToJson(UnDestroyData._CrewData);
        Debug.Log("UpdateCrew : " + data + " | " + UnDestroyData.token);

        using (UnityWebRequest www = UnityWebRequest.Put("https://api.i911adventure.com/my_crew/", data))
        //using (UnityWebRequest www = UnityWebRequest.Put("https://staging.i911adventure.com/my_crew/", data))
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
