using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intergration;
using TMPro;

public class PanelMedical : MonoBehaviour
{
    public TMP_Text txtDateOfBirthday;
    public TMP_Dropdown drpGender;
    public TMP_Dropdown drpHeight;
    //public TMP_InputField inpWeight;
    public TMP_Text WeightText;
    public TMP_Dropdown drpBloodType;

    //public TMP_InputField inpContactNumber;
    public TMP_Text ContactNumText;
    //public TMP_InputField inpAllergies;
    public TMP_Text AllergiesText;
    //public TMP_InputField inpConditions;
    public TMP_Text ConditionsText;
    //public TMP_InputField intAdditional;
    public TMP_Text AdditionalText;

    /// <summary>
    /// Save personal data
    /// </summary>
    ///
    private void Start()
    {
        //drpGender.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        //drpHeight.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        //WeightText.text = "";
        //drpBloodType.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }
    private void SavePersonalData()
    {
        UnDestroyData._UpLoadUserData.birthday_day = txtDateOfBirthday.text;
        UnDestroyData._UpLoadUserData.gender = drpGender.transform.GetChild(0).GetComponent<TMP_Text>().text;
        UnDestroyData._UpLoadUserData.height = drpHeight.transform.GetChild(0).GetComponent<TMP_Text>().text;
        //UnDestroyData._UpLoadUserData.weight = inpWeight.transform.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.weight = WeightText.text;
        UnDestroyData._UpLoadUserData.blood_type = drpBloodType.transform.GetChild(0).GetComponent<TMP_Text>().text;
        //UnDestroyData._UpLoadUserData.emergency_contact_number = inpContactNumber.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.emergency_contact_number = ContactNumText.text;

        //UnDestroyData._UpLoadUserData.medical_allergies = inpAllergies.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.medical_allergies = AllergiesText.text;
        //UnDestroyData._UpLoadUserData.list_of_medical_conditions = inpConditions.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.list_of_medical_conditions = ConditionsText.text;
        //UnDestroyData._UpLoadUserData.additional_notes = intAdditional.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.additional_notes = AdditionalText.text;
    }

    private bool SaveFieldCheck()
    {
        if (txtDateOfBirthday.text == "")
        {
            Debug.Log("inpBirthday error");
            return false;
        }

        if (drpGender.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            Debug.Log("Gender error");
            return false;
        }

        if (drpHeight.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            Debug.Log("Height error");
            return false;
        }

        /*
        if (inpWeight.transform.GetComponent<TMP_InputField>().text == "")
        {
            Debug.Log("Weight error");
            return false;
        }
        */
        if (WeightText.text == "")
        {
            Debug.Log("Weight error");
            return false;
        }

        if (drpBloodType.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            Debug.Log("BloodType error");
            return false;
        }

        /*
        if (inpAllergies.GetComponent<TMP_InputField>().text == "")
        {
            Debug.Log("Allergies error");
            return false;
        }

        if (inpConditions.GetComponent<TMP_InputField>().text == "")
        {
            Debug.Log("conditions error");
            return false;
        }

        if (intAdditional.GetComponent<TMP_InputField>().text == "")
        {
            Debug.Log("additional error");
            return false;
        }
        */

        return true;
    }

    public void SkipMedicalForm()
    {
        StartCoroutine(C_UrlPostRegister());
    }

    public void btnCreateMedical()
    {
        Debug.Log("CreateMedical");
        UIManager.Instance.OpenWaitingPanel();

        if (SaveFieldCheck())
        {
            Debug.Log("SaveCheck");

            StartCoroutine(C_UrlPostRegister());
        }
        else
        {
            UIManager.Instance.CloseWaitingPanel();

            UIManager.Instance.WarnPanelTitle("Entry Field!");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
        }
    }

    IEnumerator C_UrlPostRegister()
    {
        SavePersonalData();

        Login.FirstLogin = true;

        switch (Login.loginMode)
        {
            case "email":
                Debug.Log("Case Email");
                yield return urlRegister.Instance.urlPOST_Register();
                break;
            case "google":
                Debug.Log("Case Google");
                yield return urlRegister.Instance.urlPOST_GoogleRegister();
                break;
            case "facebook":
                Debug.Log("Case Facebook");
                yield return urlRegister.Instance.urlPOST_FacebookRegister();
                break;
            case "apple":
                Debug.Log("Case Apple");
                yield return urlRegister.Instance.urlPOST_AppleRegister();
                break;
            default:
                break;
        }

        UIManager.Instance.CloseWaitingPanel();
    }
}