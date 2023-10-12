using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using FfmpegUnity;

/// <summary>
/// This class is for create journey id
/// </summary>
public class urlNewJourney : MonoBehaviour
{
    public static urlNewJourney newJourney;

    public urlNewJourney()
    {
        newJourney = this;
    } 

    // Call this function th upload main video and cover img
    public void NewJourney()
    {
        StartCoroutine(urlPOST_NewJourney());
    }

    public IEnumerator urlPOST_NewJourney()
    {
        UnDestroyData.JourneyData data;
        
        data.duration = UnDestroyData.journeyData.duration;
        data.distance = UnDestroyData.journeyData.distance;
        data.category = UnDestroyData.journeyData.category;
        data.name = UnDestroyData.userData.user_name;

        /*Debug.Log(data.duration);
        Debug.Log(data.distance);
        Debug.Log(data.name);
        Debug.Log(data.category);*/

        string jsonString = JsonUtility.ToJson(data);

        //using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://api.i911adventure.com/new_journey/", jsonString))
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://staging.i911adventure.com/new_journey/", jsonString))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonString));

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("NewJourney Error");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                // Get journey data to upload image and video
                UnDestroyData.newJourneyData.journey_id = JsonUtility.FromJson<UnDestroyData.NewJourneyData>(www.downloadHandler.text).journey_id;
                UnDestroyData.newJourneyData.created = JsonUtility.FromJson<UnDestroyData.NewJourneyData>(www.downloadHandler.text).created;

                //UploadWithPoints();
            }
        }
    }

    private void RunJob(System.Action onComplete)
    {
        //ScreenShot.TakeScreenShot_Static(1125, 690);
        //GameObject.Find("Ffmpeg").transform.GetComponent<FfmpegCaptureCommand>().StopFfmpeg();

    }

    private void UploadWithPoints()
    {
        //StartCoroutine(LoadingSchedule.instance.userUploadVideo());

        RunJob(() =>
        {
            urlUserUpload.Instance.Filename = Record.instance.DateTime + ".mp4";
            //Debug.Log(Record.record.DateTime);
            urlUserUpload.Instance.UserUpload();
        });
    }
}