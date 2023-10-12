using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnDestroyData;

namespace AdventureTraveler
{
    public class GlobalPanelDemoData : MonoBehaviour
    {
        readonly string emailAddress = "qwe4906qwe@gmail.com";
        readonly string password = "Qq74117411";
        [SerializeField] private Journeys journeys;
        [SerializeField] EventForJourneys OnJourneysData;
        private int page = 1;
        public bool me = false;

        void Start()
        {
            StartCoroutine(urlPostLogin());
        }

        IEnumerator urlPostLogin()
        {
            WWWForm form = new WWWForm();

            form.AddField("email", emailAddress);
            form.AddField("type", "adventuretraveler");
            form.AddField("password", password);

            using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/login/", form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    // StartCoroutine(urlGET_GetProfile());
                }
                else
                {
                    var dataToken = JsonUtility.FromJson<UnDestroyData.data_token>(www.downloadHandler.text);
                    //UnDestroyData.token = dataToken.token;
                    //urlGetProfile.getProfile.GetProfile();
                    Debug.Log(dataToken.name);
                    Debug.Log(dataToken.token);

                    StartCoroutine(urlGET_GetJourneys(dataToken.token));
                }
            }
        }

        IEnumerator urlGET_GetProfile()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/my_profile/"))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    UIManager.Instance.waitingPanel.SetActive(false);
                    UIManager.Instance.WarnPanelTitle("Get Profile Error");
                    UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
                }
                else
                {

                }
            }
        }

        public IEnumerator urlGET_GetJourneys(string token)
        {
            //Debug.Log("GetJourneys boolMe-- " + me);
            //Debug.Log("GetJourneys intPage-- " + page);

            WWWForm form = new WWWForm();

            using (UnityWebRequest www = UnityWebRequest.Get("https://api.i911adventure.com/journeys/" + page.ToString() + "/"))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Authorization", "JWT " + token);

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

                    var data = JsonUtility.FromJson<UnDestroyData.Journeys>(www.downloadHandler.text);
                    journeys = data;
                    OnJourneysData.Invoke(journeys);

                }
            }
        }

    }
}