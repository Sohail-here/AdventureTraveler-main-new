using RestSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AutoLogin : MonoBehaviour
{
    public const string AUTO_LOGIN_KEY = "AutoLogin";
    async void Start()
    {
        if (ES3.KeyExists(AUTO_LOGIN_KEY))
        {
            ULog.Penguin.Log($"AutoLogin Start");
            UIManager.Instance.OpenWaitingPanel();
            UnDestroyData.token = ES3.Load<string>(AUTO_LOGIN_KEY);
            ULog.Penguin.Log($"UnDestroyData.token {UnDestroyData.token}");
            var client = new RestClient("https://api.i911adventure.com/");
            var request = new RestRequest($"/my_profile/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

            var response = await client.ExecuteAsync(request);
            ULog.Penguin.Log($"AutoLogin {response.StatusCode}\n{response.Content}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UnDestroyData.UserData data = JsonUtility.FromJson<UnDestroyData.UserData>(response.Content);

                UnDestroyData.userData.email = data.email;
                UnDestroyData.userData.lang = data.lang;
                UnDestroyData.userData.created_at = data.created_at;
                UnDestroyData.userData.account_type = data.account_type;
                UnDestroyData.userData.last_name = data.last_name;
                UnDestroyData.userData.first_name = data.first_name;
                UnDestroyData.userData.birthday_day = data.birthday_day;
                UnDestroyData.userData.gender = data.gender;
                UnDestroyData.userData.height = data.height;
                UnDestroyData.userData.weight = data.weight;
                UnDestroyData.userData.blood_type = data.blood_type;
                UnDestroyData.userData.medical_allergies = data.medical_allergies;
                UnDestroyData.userData.emergency_contact_number = data.emergency_contact_number;
                UnDestroyData.userData.list_of_medical_conditions = data.list_of_medical_conditions;
                UnDestroyData.userData.additional_notes = data.additional_notes;
                UnDestroyData.userData.status = data.status;
                UnDestroyData.userData.full_name = data.full_name;
                UnDestroyData.userData.user_name = data.user_name;



                UnDestroyData._BoaterData.boatType = PlayerPrefs.GetString("boatType");
                UnDestroyData._BoaterData.boatStyle = PlayerPrefs.GetString("boatStyle");
                UnDestroyData._BoaterData.boatRegistration = PlayerPrefs.GetString("boatRegistration");
                UnDestroyData._BoaterData.boatHullColours = PlayerPrefs.GetString("boatHullColours");
                UnDestroyData._BoaterData.boatTopColours = PlayerPrefs.GetString("boatTopColours");
                UnDestroyData._BoaterData.boatTrailer = PlayerPrefs.GetString("boatTrailer");
                UnDestroyData._BoaterData.trailerLicense = PlayerPrefs.GetString("trailerLicense");
                UnDestroyData._BoaterData.towVehicle = PlayerPrefs.GetString("towVehicle");
                UnDestroyData._BoaterData.vehicleYear = PlayerPrefs.GetString("vehicleYear");
                UnDestroyData._BoaterData.vehicleColour = PlayerPrefs.GetString("vehicleColour");
                UnDestroyData._BoaterData.vehicleLicenseNo = PlayerPrefs.GetString("vehicleLicenseNo");
                UnDestroyData._BoaterData.additionalNotes = PlayerPrefs.GetString("additionalNotes");
                //Debug.Log("Vehical Year---------------" + UnDestroyData._BoaterData.vehicleYear);
               // Debug.Log("additionalNotes---------------" + UnDestroyData._BoaterData.additionalNotes);

                Debug.Log("GetProfile");

                UnDestroyData.IsGusetLogin = false;
                SceneManager.LoadScene("Main");
            }
            else
            {
                ES3.DeleteKey(AutoLogin.AUTO_LOGIN_KEY);
            }

            UIManager.Instance.CloseWaitingPanel();
        }

    }
}
