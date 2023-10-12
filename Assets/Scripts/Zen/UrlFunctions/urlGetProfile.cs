using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class urlGetProfile : MonoBehaviour
{
    public static urlGetProfile getProfile;

    public urlGetProfile()
    {
        getProfile = this;
    }

    public void GetProfile()
    {
        StartCoroutine(urlGET_GetProfile());
    }

    public void GetProfileDontLoadScene()
    {
        StartCoroutine(urlGET_GetProfileDontLoadScene());
    }

    public IEnumerator urlGET_GetProfile()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/my_profile/"))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/my_profile/"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.waitingPanel.SetActive(false);
                UIManager.Instance.WarnPanelTitle("Get Profile Error");
                UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
                Debug.Log("Get Profile Error-- " + UnDestroyData.token + " |" + www.downloadHandler.text);
            }
            else
            {
                ES3.Save(AutoLogin.AUTO_LOGIN_KEY, UnDestroyData.token);
                ULog.Penguin.Log($"ES SAVE AUTO_LOGIN_KEY :{UnDestroyData.token}");
                UnDestroyData.UserData data = JsonUtility.FromJson<UnDestroyData.UserData>(www.downloadHandler.text);

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

                Debug.Log("GetProfile : " + www.downloadHandler.text);

                UnDestroyData.IsGusetLogin = false;
                SceneManager.LoadScene("Main");
            }
        }
    }

    public IEnumerator urlGET_GetProfileDontLoadScene()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/my_profile/"))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://staging.i911adventure.com/my_profile/"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("Get Profile Error");
                UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
            }
            else
            {
                UnDestroyData.UserData data = JsonUtility.FromJson<UnDestroyData.UserData>(www.downloadHandler.text);

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
            }
        }
    }
}
