using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intergration;
using TMPro;
using AdvancedInputFieldPlugin;

public class PanelCreateName : MonoBehaviour
{
    public GameObject PanelCreateAccount;
    public GameObject PanelMedical;

    /*
    public TMP_InputField usernameInput;
    public TMP_InputField firstnameInput;
    public TMP_InputField lastnameInput;
    */

    public AdvancedInputField ainp_Username;
    public AdvancedInputField ainp_Firstname;
    public AdvancedInputField ainp_Lastname;
    private void OnEnable()
    {
        ainp_Username.Text = "";
        ainp_Firstname.Text = "";
        ainp_Lastname.Text = "";
    }

    // Make sure the input field is not empty
    private bool NameCheck()
    {
        if (ainp_Username.Text == null || ainp_Username.Text == "")
        {
            return false;
        }
        else if (ainp_Firstname.Text == null || ainp_Firstname.Text == "")
        {
            return false;
        }
        else if (ainp_Lastname.Text == null || ainp_Lastname.Text == "")
        {
            return false;
        }

        return true;
    }


    public void NameEditing(TMP_InputField input)
    {
        if (input.text == "")
        {
            input.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            input.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void NameEditing(AdvancedInputField input)
    {
        if (input.Text == "")
        {
            input.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            input.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void NameContinue()
    {
        if (NameCheck())
        {
            UnDestroyData._UpLoadUserData.user_name = ainp_Username.Text;
            UnDestroyData._UpLoadUserData.first_name = ainp_Firstname.Text;
            UnDestroyData._UpLoadUserData.last_name = ainp_Lastname.Text;

            Debug.Log("Import namecontinue");
            Debug.Log(Login.loginMode);

            if (Login.loginMode == "email")
            {
                PanelCreateAccount.SetActive(true);
            }
            else
            {
                PanelMedical.SetActive(true);
            }
        }
        else
        {
            UIManager.Instance.WarnPanelTitle("Field Entry");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
        }
    }
}