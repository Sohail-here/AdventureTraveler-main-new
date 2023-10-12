using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class is for upload files to server
/// </summary>
public class urlUserUpload : MonoSingleton<urlUserUpload>
{
    public string Filename;
    public string JourneyID;

    public void UserUpload()
    {
        StartCoroutine(urlPost_UserUpload());
    }

    public IEnumerator urlPost_UserUpload()
    {
        Filename = Application.persistentDataPath + "/" + Filename;
        Debug.Log(Filename);
        byte[] data = File.ReadAllBytes(Filename);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", data, Filename);

        //using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/user_upload/" + JourneyID + "/", form))
        using (UnityWebRequest www = UnityWebRequest.Post("https://staging.i911adventure.com/user_upload/" + JourneyID + "/", form))
        {
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            /*
            if (isImage)
            {
                LoadingSchedule.instance.imgUploadProgress = www.downloadProgress;
            }
            else
            {
                LoadingSchedule.instance.videoUploadProgress = www.downloadProgress;
            }
            */

            yield return www.SendWebRequest();

            Debug.Log("Upload-- " + Filename);

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("User Upload Error");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("UserUploadSuccess-- " + Filename);
                ULog.Zen.Log("UserUploadSuccess-- " + Filename);
                //LoadingSchedule.instance.imgUploadProgress = 1;
                //LoadingSchedule.instance.videoUploadProgress = 1;
            }
        }
    }
}