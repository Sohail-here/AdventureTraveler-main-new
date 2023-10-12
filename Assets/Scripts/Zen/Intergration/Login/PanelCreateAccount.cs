//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using AdvancedInputFieldPlugin;
//using Intergration;
//using TMPro;
////using AdvancedInputFieldPlugin;

//public class PanelCreateAccount : MonoBehaviour
//{
//    public AdvancedInputField Email;
//    public AdvancedInputField ReEmail;
//    public AdvancedInputField Password;
//    public AdvancedInputField RePassword;


//    public GameObject MedicalPanel;

//    private bool emailCorrect = false;
//    private bool passwordCorrect = false;
//    private bool emailSame = false;
//    private bool passwSame = false;
//    private void OnEnable()
//    {
//        Email.Text = "";
//        ReEmail.Text = "";
//        Password.Text = "";
//        RePassword.Text = "";
//    }

//    public void EmailValueChange(AdvancedInputField email)
//    {
//        Debug.Log(email.Text);
//        if (UIManager.Instance.CorrectEmail(email.Text))
//        {
//            emailCorrect = true;
//            email.transform.GetChild(4).gameObject.SetActive(false);
//        }
//        else
//        {
//            emailCorrect = false;
//            email.transform.GetChild(4).gameObject.SetActive(true);
//        }
//    }

//    public void SameEmailCheck()
//    {
//        if (Email.Text != ReEmail.Text || Email.Text == "")
//        {
//            emailSame = false;
//            ReEmail.transform.GetChild(4).gameObject.SetActive(true);
//        }
//        else
//        {
//            emailSame = true;
//            ReEmail.transform.GetChild(4).gameObject.SetActive(false);
//        }
//    }

//    public void PasswordValueChanged(AdvancedInputField password)
//    {
//        if (UIManager.Instance.registerCheck(password.Text))
//        {
//            passwordCorrect = true;
//            //password.transform.GetChild(4).gameObject.SetActive(false);
//            password.transform.GetChild(4).gameObject.SetActive(false);
//        }
//        else
//        {
//            passwordCorrect = false;
//            //password.transform.GetChild(4).gameObject.SetActive(true);
//            password.transform.GetChild(4).gameObject.SetActive(true);
//        }
//    }

//    public void SamePasswordCheck(GameObject errorIcon)
//    {
//        if (Password.Text != RePassword.Text || Password.Text == "")
//        {
//            passwSame = false;
//            errorIcon.SetActive(true);
//        }
//        else
//        {
//            passwSame = true;
//            errorIcon.SetActive(false);
//        }
//    }

//    public void BtnAccountContinue()
//    {
//        // Legacy version
//        /*
//        if (EmailAddress.text == "" || Re_EmailAddress.text == "" || EnterPassword.text == "" || Re_EnterPassword.text == "")
//        {
//            FieldEntry.SetActive(true);
//            return;
//        }
//        */

//        // For advanced input field
//        if (Email.Text == "" || ReEmail.Text == "" || Password.Text == "" || RePassword.Text == "")
//        {
//            UIManager.Instance.WarnPanelTitle("Field Entry");
//            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
//            return;
//        }

//        if (!emailCorrect)
//        {
//            UIManager.Instance.WarnPanelTitle("Please Verify Email");
//            UIManager.Instance.WarnPanelContent("Email is not Correct" + "\n" + "Please check it again");
//            return;
//        }

//        if (!emailSame)
//        {
//            UIManager.Instance.WarnPanelTitle("Please Verify Email");
//            UIManager.Instance.WarnPanelContent("Emails are not same" + "\n" + "Please check it again");
//            return;
//        }

//        if (!passwordCorrect)
//        {
//            UIManager.Instance.WarnPanelTitle("Please Verify Password");
//            UIManager.Instance.WarnPanelContent("Password format is not correct" + "\n" + "Please check it again and notice the rule");
//            return;
//        }

//        if (!passwSame)
//        {
//            UIManager.Instance.WarnPanelTitle("Please Verify Password");
//            UIManager.Instance.WarnPanelContent("Password is not same" + "\n" + "Please check it again");
//            return;
//        }

//        switch (Login.loginMode)
//        {
//            case "email":
//                UnDestroyData.accountData.email = Email.Text;
//                UnDestroyData.accountData.password = Password.Text;
//                break;
//            case "facebook":
//                UnDestroyData.accountData.email = FacebookLogin.FacebookEmail;
//                break;
//            case "google":
//                UnDestroyData.accountData.email = GoogleLogin.GoogleEmail;
//                break;
//            case "apple":
//                UnDestroyData.accountData.email = AppleLogin.AppleEmail;
//                break;
//        }

//        MedicalPanel.SetActive(true);
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedInputFieldPlugin;
using Intergration;
using TMPro;
using UnityEngine.Networking;
//using AdvancedInputFieldPlugin;

public class PanelCreateAccount : MonoBehaviour
{
    public AdvancedInputField Email;
    public AdvancedInputField ReEmail;
    public AdvancedInputField Password;
    public AdvancedInputField RePassword;


    public GameObject MedicalPanel;

    private bool emailCorrect = false;
    private bool passwordCorrect = false;
    private bool emailSame = false;
    private bool passwSame = false;

    private const string checkEmailExistOrNotApi = "https://api.i911adventure.com/validate/user/email/";
    private void OnEnable()
    {
        Email.Text = "";
        ReEmail.Text = "";
        Password.Text = "";
        RePassword.Text = "";
    }

    public void EmailValueChange(AdvancedInputField email)
    {
        Debug.Log(email.Text);
        if (UIManager.Instance.CorrectEmail(email.Text))
        {
            emailCorrect = true;
            email.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            emailCorrect = false;
            email.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void SameEmailCheck()
    {
        if (Email.Text != ReEmail.Text || Email.Text == "")
        {
            emailSame = false;
            ReEmail.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            emailSame = true;
            ReEmail.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void PasswordValueChanged(AdvancedInputField password)
    {
        if (UIManager.Instance.registerCheck(password.Text))
        {
            passwordCorrect = true;
            //password.transform.GetChild(4).gameObject.SetActive(false);
            password.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            passwordCorrect = false;
            //password.transform.GetChild(4).gameObject.SetActive(true);
            password.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void SamePasswordCheck(GameObject errorIcon)
    {
        if (Password.Text != RePassword.Text || Password.Text == "")
        {
            passwSame = false;
            errorIcon.SetActive(true);
        }
        else
        {
            passwSame = true;
            errorIcon.SetActive(false);
        }
    }

    public void BtnAccountContinue()
    {
        // Legacy version
        /*
        if (EmailAddress.text == "" || Re_EmailAddress.text == "" || EnterPassword.text == "" || Re_EnterPassword.text == "")
        {
            FieldEntry.SetActive(true);
            return;
        }
        */

        // For advanced input field


        if (Email.Text == "" || ReEmail.Text == "" || Password.Text == "" || RePassword.Text == "")
        {
            UIManager.Instance.WarnPanelTitle("Field Entry");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
            return;
        }

        if (!emailCorrect)
        {
            UIManager.Instance.WarnPanelTitle("Please Verify Email");
            UIManager.Instance.WarnPanelContent("Email is not Correct" + "\n" + "Please check it again");
            return;
        }

        if (!emailSame)
        {
            UIManager.Instance.WarnPanelTitle("Please Verify Email");
            UIManager.Instance.WarnPanelContent("Emails are not same" + "\n" + "Please check it again");
            return;
        }

        if (!passwordCorrect)
        {
            UIManager.Instance.WarnPanelTitle("Please Verify Password");
            UIManager.Instance.WarnPanelContent("Password format is not correct" + "\n" + "Please check it again and notice the rule");
            return;
        }

        if (!passwSame)
        {
            UIManager.Instance.WarnPanelTitle("Please Verify Password");
            UIManager.Instance.WarnPanelContent("Password is not same" + "\n" + "Please check it again");
            return;
        }

        switch (Login.loginMode)
        {
            case "email":
                UnDestroyData.accountData.email = Email.Text;
                UnDestroyData.accountData.password = Password.Text;
                //yield return urlRegister.Instance.urlPOST_Register();
                break;
            case "facebook":
                UnDestroyData.accountData.email = FacebookLogin.FacebookEmail;
                break;
            case "google":
                UnDestroyData.accountData.email = GoogleLogin.GoogleEmail;
                break;
            case "apple":
                UnDestroyData.accountData.email = AppleLogin.AppleEmail;
                break;
        }

        StartCoroutine(ValidateEmail(Email.Text));

    }

    private IEnumerator ValidateEmail(string email)
    {
        // Create a JSON object with the email to send to the API.
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("email", email);

        // Create a UnityWebRequest for the POST request.
        UnityWebRequest request = new UnityWebRequest(checkEmailExistOrNotApi, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json.ToString());
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        // Send the request and wait for a response.
        yield return request.SendWebRequest();

        // Check for errors.
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Parse the JSON response.
            string jsonResponse = request.downloadHandler.text;
            JSONObject responseJson = new JSONObject(jsonResponse);

            string responseText = request.downloadHandler.text;
            int statusCode = (int)request.responseCode;

            if (statusCode == 200)
            {
                MedicalPanel.SetActive(true);

            }
            else
            {
                UIManager.Instance.WarnPanelTitle("Error Register");
                UIManager.Instance.WarnPanelContent("Error: " + statusCode + "\n" + responseText);
                // Handle other status codes
                Debug.LogError("Error: " + statusCode + ", " + responseText);
            }


        }
    }
}