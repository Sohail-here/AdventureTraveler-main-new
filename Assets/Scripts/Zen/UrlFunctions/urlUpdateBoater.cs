using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class urlUpdateBoater : MonoSingleton<urlUpdateBoater>
{
    public void UpdateBoater()
    {
        Debug.Log("Save Boater info to PlayerPferf");
        Debug.Log(UnDestroyData._BoaterData.boatStyle);
        Debug.Log(UnDestroyData._BoaterData.boatRegistration);
        Debug.Log(UnDestroyData._BoaterData.vehicleYear);

        PlayerPrefs.SetString("boatType", UnDestroyData._BoaterData.boatType);
        PlayerPrefs.SetString("boatStyle", UnDestroyData._BoaterData.boatStyle);
        PlayerPrefs.SetString("boatRegistration", UnDestroyData._BoaterData.boatRegistration);
        PlayerPrefs.SetString("boatHullColours", UnDestroyData._BoaterData.boatHullColours);
        PlayerPrefs.SetString("boatTopColours", UnDestroyData._BoaterData.boatTopColours);
        PlayerPrefs.SetString("boatTrailer", UnDestroyData._BoaterData.boatTrailer);
        PlayerPrefs.SetString("trailerLicense", UnDestroyData._BoaterData.trailerLicense);
        PlayerPrefs.SetString("towVehicle", UnDestroyData._BoaterData.towVehicle);
        PlayerPrefs.SetString("vehicleYear", UnDestroyData._BoaterData.vehicleYear);
        PlayerPrefs.SetString("vehicleColour", UnDestroyData._BoaterData.vehicleColour);
        PlayerPrefs.SetString("vehicleLicenseNo", UnDestroyData._BoaterData.vehicleLicenseNo);
        PlayerPrefs.SetString("additionalNotes", UnDestroyData._BoaterData.additionalNotes);
        PlayerPrefs.Save();

        //UIManager.Instance.OpenWaitingPanel();
        UIManager.Instance.WarnPanelTitle("Data Saved!");
        UIManager.Instance.WarnPanelContent("Your information has been successfully stored.");
        //StartCoroutine(urlPUT_UpdateBoater());
    }

    public IEnumerator urlPUT_UpdateBoater()
    {
        string data = JsonUtility.ToJson(UnDestroyData._BoaterData);
        Debug.Log("UpdateBoater : " + data + " | " + UnDestroyData.token);

        using (UnityWebRequest www = UnityWebRequest.Put("https://api.i911adventure.com/my_boat/", data))
        //using (UnityWebRequest www = UnityWebRequest.Put("https://staging.i911adventure.com/my_boat/", data))
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