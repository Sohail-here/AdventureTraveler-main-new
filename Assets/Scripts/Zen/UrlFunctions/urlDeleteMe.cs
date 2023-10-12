using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class urlDeleteMe : MonoBehaviour
{
    public static urlDeleteMe delete;

    public urlDeleteMe()
    {
        delete = this;
    }

    public void DeleteMe()
    {
        StartCoroutine(urlDEL_DeleteMe());
    }

    IEnumerator urlDEL_DeleteMe()
    {
        WWWForm form = new WWWForm();

        //using (UnityWebRequest www = UnityWebRequest.Delete("https://api.i911adventure.com/delete_me/"))
        using (UnityWebRequest www = UnityWebRequest.Delete("https://staging.i911adventure.com/delete_me/"))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("Delete Error");
                UIManager.Instance.WarnPanelContent(www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                //Close app after delete account
                Application.Quit();
            }
        }
    }
}