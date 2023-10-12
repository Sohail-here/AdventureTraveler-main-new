using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using AdvancedInputFieldPlugin;

public class PanelNewPassword : MonoBehaviour
{
    [SerializeField]
    private GameObject pnl_RetrievePassword;
    [SerializeField]
    private GameObject pnl_CofirmEmail;
    [SerializeField]
    private GameObject pnl_NewPassword;

    [SerializeField]
    private GameObject newPasswordIsTyping;
    [SerializeField]
    private GameObject newPasswordTypingError;
    [SerializeField]
    private GameObject rePasswordIsTyping;
    [SerializeField]
    private GameObject rePasswordTypingError;

    public AdvancedInputField NewPassword;
    public AdvancedInputField ReNewPassword;

    private bool newPasswordCorrect = false;
    private bool reNewPasswordCorrect = false;

    public void InputOnSelect(GameObject typingObj)
    {
        newPasswordTypingError.SetActive(false);
        rePasswordTypingError.SetActive(false);
        typingObj.SetActive(true);
    }

    public void NewPasswordEndEdit(TMP_InputField password)
    {
        if (UIManager.Instance.registerCheck(password.text))
        {
            newPasswordIsTyping.SetActive(false);
            newPasswordCorrect = true;
        }
        else
        {
            newPasswordIsTyping.SetActive(false);
            newPasswordTypingError.SetActive(true);
            newPasswordCorrect = false;
        }
    }

    public void NewPasswordEndEdit(AdvancedInputField password)
    {
        if (UIManager.Instance.registerCheck(password.Text))
        {
            newPasswordIsTyping.SetActive(false);
            newPasswordCorrect = true;
        }
        else
        {
            newPasswordIsTyping.SetActive(false);
            newPasswordTypingError.SetActive(true);
            newPasswordCorrect = false;
        }
    }

    public void SamePasswordCheck()
    {
        if (NewPassword.Text != ReNewPassword.Text || NewPassword.Text == "")
        {
            rePasswordIsTyping.SetActive(false);
            rePasswordTypingError.SetActive(true);
            reNewPasswordCorrect = false;
        }
        else
        {
            rePasswordIsTyping.SetActive(false);
            rePasswordTypingError.SetActive(false);
            reNewPasswordCorrect = true;
        }
    }

    public void btn_UpdatePassword()
    {
        if (newPasswordCorrect && reNewPasswordCorrect)
        {
            StartCoroutine(urlPOST_RePassword());
        }
        else
        {
            UIManager.Instance.WarnPanelTitle("Input Error!");
            UIManager.Instance.WarnPanelContent("Please check all input field!");
        }
    }

    IEnumerator urlPOST_RePassword()
    {
        WWWForm form = new WWWForm();

        form.AddField("password", NewPassword.Text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.i911adventure.com/update_password/enduser/" + PanelRetrievePassword.Instance.Email + "/", form))
        {
            UIManager.Instance.OpenWaitingPanel();

            yield return www.SendWebRequest();

            UIManager.Instance.CloseWaitingPanel();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text == "{\"msg\": \"get verify email first\"}")
                {
                    UIManager.Instance.WarnPanelTitle("Validate Email");
                    UIManager.Instance.WarnPanelContent("Before we are authorized to" + "\n" + "change your pasword you must" + "\n" + "click on the link we sent to your" + "\n" + "email");
                }
                ULog.Penguin.Log($"NewPassword Error!\n{www.error}\n{www.downloadHandler.text}");              
            }
            else
            {
                ULog.Penguin.Log($"Retrieve Password Success!");
                UIManager.Instance.WarnPanelTitle("Retrieve Password Success!");
                UIManager.Instance.WarnPanelContent("Please use new password to login!");

                pnl_NewPassword.SetActive(false);
                pnl_CofirmEmail.SetActive(false);
                pnl_RetrievePassword.SetActive(false);
            }
        }
    }
}