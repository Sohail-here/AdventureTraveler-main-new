using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AdvancedInputFieldPlugin;
using TMPro;

public class Account : MonoSingleton<Account>
{
    [Header("Title Name")]
    public TMP_Text TitleName;

    #region Profile

    [Header("Profile")]
    public AdvancedInputField Username;
    public AdvancedInputField Firstname;
    public AdvancedInputField Lastname;
    public AdvancedInputField Email;

    #endregion

    #region Medical

    [Header("Medical")]
    public TMP_Text txtDateOfBirthday;
    public TMP_Dropdown drpGender;
    public TMP_Dropdown drpHeight;
    //public TMP_InputField inpWeight;
    public AdvancedInputField Weight;
    public TMP_Dropdown drpBloodType;
    /*
    public TMP_InputField inpContactNumber;
    public TMP_InputField inpAllergies;
    public TMP_InputField inpConditions;
    public TMP_InputField intAdditional;
    */
    public AdvancedInputField ContactNumber;
    public AdvancedInputField Allergies;
    public AdvancedInputField Conditions;
    public AdvancedInputField Additional;

    #endregion

    [Header("Language")]
    public Toggle English;
    public Toggle French;
    public Toggle Spanish;

    #region MyBoat
    // TO DO: Figure out how to load these variables from the UNITY interface.
    [Header("MyBoat")]
    public TMP_Dropdown drpBoatType;
    public AdvancedInputField boatStyle;
    public AdvancedInputField boatRegistration;
    public AdvancedInputField boatHullColours;
    public AdvancedInputField boatTopColours;
    public AdvancedInputField boatTrailer;
    public AdvancedInputField trailerLicense;
    public AdvancedInputField towVehicle;
    public AdvancedInputField vehicleYear;
    public AdvancedInputField vehicleColour;
    public AdvancedInputField vehicleLicenseNo;
    public AdvancedInputField additionalNotes;

    #endregion


    #region MyCrew
    // TO DO: Figure out how to load these variables from the UNITY interface.
    [Header("MyBoat")]
    //public TMP_Dropdown drpBoatType;
    public AdvancedInputField emergencyContactName1;
    public AdvancedInputField contactName1;
    public TMP_Dropdown drpRelationType1;
    public AdvancedInputField emergencyContactName2;
    public AdvancedInputField contactName2;
    public TMP_Dropdown drpRelationType2;
    public AdvancedInputField emergencyContactName3;
    public AdvancedInputField contactName3;
    public TMP_Dropdown drpRelationType3;
    public AdvancedInputField myCrewAdditionalNotes;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            StartCoroutine(LoadData());
            //StartCoroutine(LoadBoatData());
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        switch (LanguageManager.Instance.Mode)
        {
            case "English":
                English.isOn = true;
                break;
            case "French":
                French.isOn = true;
                break;
            case "Spanish":
                Spanish.isOn = true;
                break;
            default:
                break;
        }
    }

    IEnumerator LoadData()
    {
        yield return urlGetProfile.getProfile.urlGET_GetProfileDontLoadScene();
        ProfileLoad();
        MedicalLoad();
    }

    IEnumerator LoadBoatData()
    {
        // TO DO: need to write urlGET_BoatAndCrewData();
        yield return urlGetProfile.getProfile.urlGET_GetProfileDontLoadScene();
        MyBoatLoad();
        MyCrewLoad();
    }

    public void MyBoatLoad()
    {
        

        // TO DO:  Needs to load boat data.
        drpBoatType.value = drpBoatType.options.FindIndex(drpBoatType => drpBoatType.text == UnDestroyData._BoaterData.boatType);
        boatStyle.Text = UnDestroyData._BoaterData.boatStyle;
        boatRegistration.Text=UnDestroyData._BoaterData.boatRegistration;
        
        boatHullColours.Text=UnDestroyData._BoaterData.boatHullColours;
        boatTopColours.Text = UnDestroyData._BoaterData.boatTopColours;
        boatTrailer.Text=UnDestroyData._BoaterData.boatTrailer;
        trailerLicense.Text=UnDestroyData._BoaterData.trailerLicense;
        //Debug.Log("trailerLicense-------------" + UnDestroyData._BoaterData.trailerLicense);
        vehicleYear.Text = UnDestroyData._BoaterData.vehicleYear;
        //Debug.Log("vehicleYear--------------" + UnDestroyData._BoaterData.vehicleYear);
        vehicleColour.Text = UnDestroyData._BoaterData.vehicleColour;
        towVehicle.Text = UnDestroyData._BoaterData.towVehicle;
       // Debug.Log(UnDestroyData._BoaterData.towVehicle);
        vehicleLicenseNo.Text = UnDestroyData._BoaterData.vehicleLicenseNo;
       // Debug.Log(UnDestroyData._BoaterData.vehicleLicenseNo);
        additionalNotes.Text = UnDestroyData._BoaterData.additionalNotes;
       // Debug.Log(UnDestroyData._BoaterData.additionalNotes);
    }
    public void MyCrewLoad()
    {
        // TO DO:  Needs to load crew data.
        emergencyContactName1.Text = UnDestroyData._CrewData.emergencyContactName1;
        contactName1.Text = UnDestroyData._CrewData.contactName1;
        drpRelationType1.value = drpRelationType1.options.FindIndex(drpRelationType1 => drpRelationType1.text == UnDestroyData._CrewData.drpRelationType1);

        emergencyContactName2.Text = UnDestroyData._CrewData.emergencyContactName2;
        contactName2.Text = UnDestroyData._CrewData.contactName2;
        drpRelationType2.value = drpRelationType2.options.FindIndex(drpRelationType2 => drpRelationType2.text == UnDestroyData._CrewData.drpRelationType2);

        emergencyContactName3.Text = UnDestroyData._CrewData.emergencyContactName3;
        contactName3.Text = UnDestroyData._CrewData.contactName3;
        drpRelationType3.value = drpRelationType3.options.FindIndex(drpRelationType3 => drpRelationType3.text == UnDestroyData._CrewData.drpRelationType3);

        myCrewAdditionalNotes.Text = UnDestroyData._CrewData.myCrewAdditionalNotes;
    }

    public void ProfileLoad()
    {
        TitleName.text = UnDestroyData.userData.user_name;

        Username.Text = UnDestroyData.userData.user_name;
        Firstname.Text = UnDestroyData.userData.first_name;
        Lastname.Text = UnDestroyData.userData.last_name;
        Email.Text = UnDestroyData.userData.email;
    }

    public void MedicalLoad()
    {
        string[] birthday = UnDestroyData.userData.birthday_day.Split('T');
        txtDateOfBirthday.text = birthday[0];

        drpGender.value = drpGender.options.FindIndex(drpGender => drpGender.text == UnDestroyData.userData.gender);
        drpHeight.value = drpHeight.options.FindIndex(drpHeight => drpHeight.text == UnDestroyData.userData.height);
        //inpWeight.transform.GetComponent<TMP_InputField>().text = UnDestroyData.userData.weight;
        Weight.Text = UnDestroyData.userData.weight;
        drpBloodType.value = drpBloodType.options.FindIndex(drpBloodType => drpBloodType.text == UnDestroyData.userData.blood_type);

        /*
        inpContactNumber.GetComponent<TMP_InputField>().text = UnDestroyData.userData.emergency_contact_number;
        inpAllergies.GetComponent<TMP_InputField>().text = UnDestroyData.userData.medical_allergies;
        inpConditions.GetComponent<TMP_InputField>().text = UnDestroyData.userData.list_of_medical_conditions;
        intAdditional.GetComponent<TMP_InputField>().text = UnDestroyData.userData.additional_notes;
        */

        ContactNumber.Text = UnDestroyData.userData.emergency_contact_number;
        Allergies.Text = UnDestroyData.userData.medical_allergies;
        Conditions.Text = UnDestroyData.userData.list_of_medical_conditions;
        Additional.Text = UnDestroyData.userData.additional_notes;
    }

    public void BtnSaveEdit()
    {
        if (SaveFieldCheck())
        {
            StartCoroutine(ReLoadData());
        }
        else
        {
            UIManager.Instance.WarnPanelTitle("Entry Field!");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
        }
    }

    public void BtnSaveCrewData()
    {
        StartCoroutine(ReLoadCrewData());

        /*if (SaveBoatFieldCheck())
        {
            StartCoroutine(ReLoadBoatData());
        }
        else
        {
            UIManager.Instance.WarnPanelTitle("Entry Field!");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
        }*/
    }
    public void BtnSaveBoatData()
    {
        StartCoroutine(ReLoadBoatData());

        /*if (SaveBoatFieldCheck())
        {
            StartCoroutine(ReLoadBoatData());
        }
        else
        {
            UIManager.Instance.WarnPanelTitle("Entry Field!");
            UIManager.Instance.WarnPanelContent("Please complete entry on all" + "\n" + "fields to continue");
        }*/

    }


    IEnumerator ReLoadBoatData()
    {
        yield return new WaitUntil(() => SetBoaterData() == true);
        //yield return urlUpdateBoater.Instance.urlPUT_UpdateBoater();
        urlUpdateBoater.Instance.UpdateBoater();

        //TitleName.text = Username.Text;


    }
    IEnumerator ReLoadCrewData()
    {
        yield return new WaitUntil(() => SetCrewData() == true);
        //yield return urlUpdateBoater.Instance.urlPUT_UpdateBoater();
        urlUpdateCrew.Instance.UpdateCrew();

        //TitleName.text = Username.Text;


    }


    IEnumerator ReLoadData()
    {
        yield return new WaitUntil(() => SetData() == true);
        yield return urlUpdateProfile.Instance.urlPUT_UpdateProfile();

        TitleName.text = Username.Text;

        /*
        try
        {
            ProfileLoad();
            MedicalLoad();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        */
    }
    private bool SaveBoatFieldCheck()
    {
        if (drpBoatType.transform.GetChild(0).GetComponent<TMP_Text>().text == "Select")
        {
            return false;
        }

        if (boatStyle.Text == "")
        {
            return false;
        }

        if (boatRegistration.Text == "")
        {
            return false;
        }

        if (boatHullColours.Text == "")
        {
            return false;
        }

        if (boatTopColours.Text == "")
        {
            return false;
        }

        if (boatTrailer.Text == "")
        {
            return false;
        }

        if (trailerLicense.Text == "")
        {
            return false;
        }

        if (towVehicle.Text == "")
        {
            return false;
        }
        if (vehicleYear.Text == "")
        {
            return false;
        }
        if (vehicleColour.Text == "")
        {
            return false;
        }
        if (vehicleLicenseNo.Text == "")
        {
            return false;
        }
        if (additionalNotes.Text == "")
        {
            return false;
        }

        return true;
    }
    private bool SaveFieldCheck()
    {
        if (Username.Text == "")
        {
            return false;
        }

        if (Lastname.Text == "")
        {
            return false;
        }

        if (Firstname.Text == "")
        {
            return false;
        }

        if (txtDateOfBirthday.text == "")
        {
            return false;
        }

        if (drpGender.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            return false;
        }

        if (drpHeight.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            return false;
        }

        if (Weight.Text == "")
        {
            return false;
        }

        if (drpBloodType.transform.GetChild(0).GetComponent<TMP_Text>().text == "")
        {
            return false;
        }

        return true;
    }

    public bool SetBoaterData()
    {
        UnDestroyData._BoaterData.boatType = drpBoatType.transform.GetChild(0).GetComponent<TMP_Text>().text;
        UnDestroyData._BoaterData.boatStyle = boatStyle.Text;
        UnDestroyData._BoaterData.boatRegistration = boatRegistration.Text;
        UnDestroyData._BoaterData.boatHullColours = boatHullColours.Text;
        UnDestroyData._BoaterData.boatTopColours = boatTopColours.Text;
        UnDestroyData._BoaterData.boatTrailer = boatTrailer.Text;
        UnDestroyData._BoaterData.trailerLicense = trailerLicense.Text;
        UnDestroyData._BoaterData.towVehicle = towVehicle.Text;
        UnDestroyData._BoaterData.vehicleYear = vehicleYear.Text;
        UnDestroyData._BoaterData.vehicleColour = vehicleColour.Text;
        UnDestroyData._BoaterData.vehicleLicenseNo = vehicleLicenseNo.Text;
        UnDestroyData._BoaterData.additionalNotes = additionalNotes.Text;
        return true;
    }
    public bool SetCrewData()
    {
        UnDestroyData._CrewData.emergencyContactName1 = emergencyContactName1.Text;
        UnDestroyData._CrewData.contactName1 = contactName1.Text;
        UnDestroyData._CrewData.drpRelationType1 = drpRelationType1.transform.GetChild(0).GetComponent<TMP_Text>().text;

        UnDestroyData._CrewData.emergencyContactName2 = emergencyContactName2.Text;
        UnDestroyData._CrewData.contactName2 = contactName2.Text;
        UnDestroyData._CrewData.drpRelationType2 = drpRelationType2.transform.GetChild(0).GetComponent<TMP_Text>().text;

        UnDestroyData._CrewData.emergencyContactName3 = emergencyContactName3.Text;
        UnDestroyData._CrewData.contactName3 = contactName3.Text;
        UnDestroyData._CrewData.drpRelationType3 = drpRelationType3.transform.GetChild(0).GetComponent<TMP_Text>().text;

        UnDestroyData._CrewData.myCrewAdditionalNotes = myCrewAdditionalNotes.Text;
        return true;
    }

    private bool SetData()
    {
        UnDestroyData._UpLoadUserData.user_name = Username.Text;
        UnDestroyData._UpLoadUserData.first_name = Firstname.Text;
        UnDestroyData._UpLoadUserData.last_name = Lastname.Text;

        UnDestroyData._UpLoadUserData.birthday_day = txtDateOfBirthday.text;
        UnDestroyData._UpLoadUserData.gender = drpGender.transform.GetChild(0).GetComponent<TMP_Text>().text;
        UnDestroyData._UpLoadUserData.height = drpHeight.transform.GetChild(0).GetComponent<TMP_Text>().text;
        //UnDestroyData._UpLoadUserData.weight = inpWeight.transform.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.weight = Weight.Text;
        UnDestroyData._UpLoadUserData.blood_type = drpBloodType.transform.GetChild(0).GetComponent<TMP_Text>().text;

        /*
        UnDestroyData._UpLoadUserData.emergency_contact_number = inpContactNumber.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.medical_allergies = inpAllergies.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.list_of_medical_conditions = inpConditions.GetComponent<TMP_InputField>().text;
        UnDestroyData._UpLoadUserData.additional_notes = intAdditional.GetComponent<TMP_InputField>().text;
        */

        UnDestroyData._UpLoadUserData.emergency_contact_number = ContactNumber.Text;
        UnDestroyData._UpLoadUserData.medical_allergies = Allergies.Text;
        UnDestroyData._UpLoadUserData.list_of_medical_conditions = Conditions.Text;
        UnDestroyData._UpLoadUserData.additional_notes = Additional.Text;

        return true;
    }
}