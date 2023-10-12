using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject warnPanel;
    public GameObject waitingPanel;
    public TMP_Text warnPanelTitle;
    public TMP_Text warnPanelContent;
    public Text DebugText;
    public string Filename;
    public RegisterPanel RegisterP;

    private void Start()
    {
        UnSleep();
        if (UnDestroyData.IsGusetLogin)
        {

        }

        RegisterP.FirstNameInput.Text = "";
        RegisterP.LastNameInput.Text = "";
    }

    public void OpenObject(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void CloseObject(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void InstantiateObject(GameObject prefab)
    {
        Instantiate(prefab, gameObject.transform);
    }

    public void InstantiateObjInCanvas(GameObject prefab)
    {
        Instantiate(prefab, GameObject.Find("Canvas").transform);
    }

    public void Logging(GameObject logging)
    {
        StartCoroutine(LogginPanel(logging));
    }

    IEnumerator LogginPanel(GameObject logging)
    {
        logging.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        logging.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnSleep()
    {
        Application.runInBackground = true;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.sleepTimeout = 300;
    }

    public void ToggleSwitchObj(GameObject obj)
    {
        Debug.Log(gameObject.name);
    }

    // Lagecy
    public bool registerCheck(string input)
    {
        bool hasNum = false;
        bool hasCap = false;
        bool hasLow = false;
        char currentChr;

        if (input.Length < 8)
        {
            return false;
        }

        for (int i = 0; i < input.Length; i++)
        {
            currentChr = input[i];

            if (char.IsDigit(currentChr))
            {
                hasNum = true;
            }
            else if (char.IsUpper(currentChr))
            {
                hasCap = true;
            }
            else if (char.IsLower(currentChr))
            {
                hasLow = true;
            }

            if (hasNum && hasCap && hasLow)
            {
                return true;
            }
        }

        return false;
    }

    //public bool CorrectEmail(string email)
    //{
    //    if (string.IsNullOrWhiteSpace(email))
    //    {
    //        Debug.Log("string.IsNullOrWhiteSpace");
    //        return false;
    //    }

    //    try
    //    {
    //        // Normalize the domain
    //        email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
    //                              RegexOptions.None, TimeSpan.FromMilliseconds(200));

    //        // Examines the domain part of the email and normalizes it.
    //        string DomainMapper(Match match)
    //        {
    //            // Use IdnMapping class to convert Unicode domain names.
    //            var idn = new IdnMapping();

    //            // Pull out and process domain name (throws ArgumentException on invalid)
    //            string domainName = idn.GetAscii(match.Groups[2].Value);

    //            return match.Groups[1].Value + domainName;
    //        }
    //    }
    //    catch (RegexMatchTimeoutException e)
    //    {
    //        return false;
    //    }
    //    catch (ArgumentException e)
    //    {
    //        return false;
    //    }

    //    try
    //    {
    //        return Regex.IsMatch(email,
    //            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
    //            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
    //    }
    //    catch (RegexMatchTimeoutException)
    //    {
    //        return false;
    //    }
    //}

    public bool CorrectEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Debug.Log("string.IsNullOrWhiteSpace");
            return false;
        }
            

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            Debug.Log("Catch exception");
            return false;
        }
    }


    public void WarnPanelTitle(string title)
    {
        warnPanel.SetActive(true);
        warnPanelTitle.text = title;
        warnPanelTitle.text = title;
    }

    public void WarnPanelContent(string errorMsg)
    {
        Debug.Log("Warn--");
        warnPanel.SetActive(true);
        warnPanelContent.text = errorMsg;
    }

    public void OpenWaitingPanel()
    {
        waitingPanel.SetActive(true);
        Debug.Log("OpenWaitingPanel");
    }

    public void CloseWaitingPanel()
    {
        waitingPanel.SetActive(false);
        Debug.Log("CloseWaitingPanel");
    }

    public void InstantiateObj(GameObject obj)
    {
        Instantiate(obj, gameObject.transform);
        Debug.Log("Instantiate!" + obj.name);
    }

    public void CloseApp()
    {
        ES3.DeleteKey(AutoLogin.AUTO_LOGIN_KEY);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //Application.Quit();
    }

    public void DebugUserData()
    {
        Debug.Log(UnDestroyData.userData.email);
        Debug.Log(UnDestroyData.userData.lang);
        Debug.Log(UnDestroyData.userData.created_at);
        Debug.Log(UnDestroyData.userData.account_type);
        Debug.Log(UnDestroyData.userData.last_name);
        Debug.Log(UnDestroyData.userData.first_name);
        Debug.Log(UnDestroyData.userData.birthday_day);
        Debug.Log(UnDestroyData.userData.gender);
        Debug.Log(UnDestroyData.userData.height);
        Debug.Log(UnDestroyData.userData.weight);
        Debug.Log(UnDestroyData.userData.blood_type);
        Debug.Log(UnDestroyData.userData.medical_allergies);
        Debug.Log(UnDestroyData.userData.emergency_contact_number);
        Debug.Log(UnDestroyData.userData.list_of_medical_conditions);
        Debug.Log(UnDestroyData.userData.additional_notes);
        Debug.Log(UnDestroyData.userData.status);
        Debug.Log(UnDestroyData.userData.full_name);
        Debug.Log(UnDestroyData.userData.user_name);
    }

    /// <summary>
    /// Confirm there is no null in data
    /// </summary>
    public void UserDataRecheck()
    {
        if (UnDestroyData.userData.email == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.email = "null";
        }

        if (UnDestroyData.userData.lang == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.lang = "null";
        }

        if (UnDestroyData.userData.created_at == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.created_at = "null";
        }

        if (UnDestroyData.userData.account_type == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.account_type = "null";
        }

        if (UnDestroyData.userData.last_name == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.last_name = "null";
        }

        if (UnDestroyData.userData.first_name == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.first_name = "null";
        }

        if (UnDestroyData.userData.birthday_day == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.birthday_day = "null";
        }

        if (UnDestroyData.userData.gender == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.gender = "null";
        }

        if (UnDestroyData.userData.height == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.height = "null";
        }

        if (UnDestroyData.userData.weight == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.weight = "null";
        }

        if (UnDestroyData.userData.blood_type == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.blood_type = "null";
        }

        if (UnDestroyData.userData.medical_allergies == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.medical_allergies = "null";
        }

        if (UnDestroyData.userData.emergency_contact_number == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.emergency_contact_number = "null";
        }

        if (UnDestroyData.userData.list_of_medical_conditions == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.list_of_medical_conditions = "null";
        }

        if (UnDestroyData.userData.additional_notes == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.additional_notes = "null";
        }

        if (UnDestroyData.userData.full_name == null || UnDestroyData.userData.full_name == "")
        {
            UnDestroyData.userData.full_name = "null";
        }

        if (UnDestroyData.userData.user_name == null || UnDestroyData.userData.user_name == "")
        {
            UnDestroyData.userData.user_name = "null";
        }
    }

    public void GetScreen()
    {
        ScreenShot.TakeScreenShot_Static(1600, 800);
    }

    public string GetUUID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    public void DebugPrint(string msg)
    {
        DebugText.text += "\n" + msg;
        Debug.Log(msg);
    }

    public void ReturnFunc()
    {
        return;
    }

    public Texture2D ScaleTexture(Texture2D source, float targetWidth, float targetHeight)
    {
        Texture2D result = new Texture2D((int)targetWidth, (int)targetHeight, source.format, false);

        float incX = (1.0f / targetWidth);
        float incY = (1.0f / targetHeight);

        for (int i = 0; i < result.height; i++)
        {
            for (int j = 0; j < result.width; j++)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }

        result.Apply();
        return result;
    }

    public string GetTime()
    {
        return DateTime.UtcNow.ToString("ddMMyyyyHHmmss");
    }

    public GameObject FindChildGameObjectByName(GameObject topParentGameObject, string gameObjectName)
    {
        for (int i = 0; i < topParentGameObject.transform.childCount; i++)
        {
            if (topParentGameObject.transform.GetChild(i).name == gameObjectName)
            {
                return topParentGameObject.transform.GetChild(i).gameObject;
            }

            GameObject tmp = FindChildGameObjectByName(topParentGameObject.transform.GetChild(i).gameObject, gameObjectName);

            if (tmp != null)
            {
                return tmp;
            }
        }

        return null;
    }

    public GameObject FindParentObjectByName(GameObject selfGameObject, string gameObjectName)
    {
        Debug.Log(selfGameObject.name);
        Debug.Log(selfGameObject.transform.parent.name);

        for (int i = 0; i < selfGameObject.transform.parent.childCount; i++)
        {
            if (selfGameObject.transform.parent.GetChild(i).name == gameObjectName)
            {
                return selfGameObject.transform.parent.GetChild(i).gameObject;
            }
        }

        GameObject tmp = FindParentObjectByName(selfGameObject.transform.parent.transform.parent.gameObject, gameObjectName);

        if (tmp != null)
        {
            return tmp;
        }

        return null;
    }
}