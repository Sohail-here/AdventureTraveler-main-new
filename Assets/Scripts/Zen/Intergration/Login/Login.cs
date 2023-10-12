using System;
using System.Collections;
using UnityEngine;
using TMPro;
using AdvancedInputFieldPlugin;

namespace Intergration
{
    public class Login : MonoBehaviour
    {
        //public TMP_InputField EmailInput;
        //public TMP_InputField PasswordInput;

        public AdvancedInputField AinpEmail;
        public AdvancedInputField AinpPassword;

        public static string loginMode;

        public static bool FirstLogin;

        void Start()
        {
            FirstLogin = false;
        }

        #region Set login mode
        public void SetEmail()
        {
            loginMode = "email";
        }

        public void SetGoogle()
        {
            loginMode = "google";
        }

        public void SetFacebook()
        {
            loginMode = "facebook";
        }

        public void SetApple()
        {
            loginMode = "apple";
        }
        #endregion

        public void EmailLogin()
        {
            /*
            if (EmailInput.text == "" || PasswordInput.text == "")
            {
                UIManager.Instance.WarnPanelTitle("Field Entry");
                UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
                return;
            }
            else
            {
                //Debug.Log("FirstLogin-- " + FirstLogin);

                urlLogin.Instance.Login("email", EmailInput.text, PasswordInput.text);
            }
            */

            if (AinpEmail.Text == "" || AinpPassword.Text == "")
            {
                UIManager.Instance.WarnPanelTitle("Field Entry");
                UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
                return;
            }
            else
            {
                urlLogin.Instance.Login("email", AinpEmail.Text, AinpPassword.Text);
            }
        }

        IEnumerator C_LoginFirstTime()
        {
            //urlLogin.Instance.Login("email", EmailInput.text, PasswordInput.text);
            urlLogin.Instance.Login("email", AinpEmail.Text, AinpPassword.Text);
            yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();
            Debug.Log("Here to load scene");
        }
    }
}