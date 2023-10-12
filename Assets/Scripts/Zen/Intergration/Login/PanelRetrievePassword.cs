using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using AdvancedInputFieldPlugin;

public class PanelRetrievePassword : MonoSingleton<PanelRetrievePassword>
{
    public string Email;

    public AdvancedInputField EmailInput;
    [SerializeField]
    private GameObject pnlNewPassword;
    [SerializeField]
    private GameObject pnlConfirmEmail;

    public void RePasswordEmailSend()
    {
        if (EmailInput.Text == "")
        {
            UIManager.Instance.WarnPanelTitle("Entry field!");
            UIManager.Instance.WarnPanelContent("Please complete all field!");
        }
        else if (!UIManager.Instance.CorrectEmail(EmailInput.Text))
        {
            UIManager.Instance.WarnPanelTitle("This is not a email address!");
            UIManager.Instance.WarnPanelContent("Please input a correct email address!");
        }
        else
        {
            Email = EmailInput.Text;
            StartCoroutine(urlPOST_RePasswordEmail(EmailInput.Text));
        }
    }

    public bool EmailValueChange(TMP_InputField email)
    {
        if (UIManager.Instance.CorrectEmail(email.text))
        {
            return true;           
        }
        else
        {
            return false;        
        }
    }

    IEnumerator urlPOST_RePasswordEmail(string email)
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/check_account_email/" + email + "/adventuretraveler/", form))
        {
            UIManager.Instance.OpenWaitingPanel();

            yield return www.SendWebRequest();

            UIManager.Instance.CloseWaitingPanel();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);

                string[] errorMsg = www.downloadHandler.text.Split('"');

                UIManager.Instance.WarnPanelTitle("RePasswordEmail Error!");
                UIManager.Instance.WarnPanelContent(errorMsg[3]);
            }
            else
            {
                pnlConfirmEmail.SetActive(true);
            }
        }
    }

    public void OpenPnlNewPassword()
    {
        pnlNewPassword.SetActive(true);
    }
}