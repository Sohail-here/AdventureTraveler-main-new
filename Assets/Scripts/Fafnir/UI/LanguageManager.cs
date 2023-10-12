using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using AdventureTraveler.Language;

public class LanguageManager : MonoSingleton<LanguageManager>
{
    /*Zen
    private static LanguageManager instance = null;

    public static LanguageManager Instance
    {
        get { return instance; }
    }
    */

    //[SerializeField]
    //private SystemLanguage language;
    [SerializeField]
    public LanguageSystem languageSystem;

    public GameObject[] ModifyButtonsFr;
    public GameObject[] ModifyButtonsSp;
    public TMP_Text[] ModifyTtxtsFr;
    public TMP_Text[] ModifyTtxtsSp;

    //public Dictionary<string, string> dict = new Dictionary<string, string>();

    [HideInInspector]
    public string Mode;

    void Start() 
    {
        //var findText = FindObjectsOfType<Text>(true);
        //var findTextPro = FindObjectsOfType<TextMeshProUGUI>(true);

        //languageSystem.FindLanguageTexts(findText);
        //languageSystem.FindLanguageTexts(findTextPro);
    }

    public void SelectEn()
    {
        languageSystem.Setting(LanguageType.English);

        for (int i = 0; i < ModifyButtonsFr.Length; i++)
        {
            ModifyButtonsFr[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1015, 245);
        }

        for (int i = 0; i < ModifyButtonsSp.Length; i++)
        {
            ModifyButtonsSp[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1015, 245);
        }

        for (int i = 0; i < ModifyTtxtsFr.Length; i++)
        {
            ModifyTtxtsFr[i].transform.localPosition = new Vector3(ModifyTtxtsFr[i].transform.localPosition.x, 64, ModifyTtxtsFr[i].transform.localPosition.z);
        }

        for (int i = 0; i < ModifyTtxtsSp.Length; i++)
        {
            ModifyTtxtsSp[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 64, ModifyTtxtsSp[i].transform.localPosition.z);
        }

        Mode = "English";
    }

    public void ToggleEn()
    {
        if (Account.Instance.English.isOn)
        {
            languageSystem.Setting(LanguageType.English);

            for (int i = 0; i < ModifyButtonsFr.Length; i++)
            {
                ModifyButtonsFr[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1015, 245);
            }

            for (int i = 0; i < ModifyButtonsSp.Length; i++)
            {
                ModifyButtonsSp[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1015, 245);
            }

            for (int i = 0; i < ModifyTtxtsFr.Length; i++)
            {
                ModifyTtxtsFr[i].transform.localPosition = new Vector3(ModifyTtxtsFr[i].transform.localPosition.x, 64, ModifyTtxtsFr[i].transform.localPosition.z);
            }

            for (int i = 0; i < ModifyTtxtsSp.Length; i++)
            {
                ModifyTtxtsSp[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 64, ModifyTtxtsSp[i].transform.localPosition.z);
            }

            Mode = "English";
        }
    }

    public void SelectFr()
    {      
        languageSystem.Setting(LanguageType.French);

        for (int i = 0; i < ModifyButtonsFr.Length; i++)
        {
            ModifyButtonsFr[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1268.75f, 306.25f);
        }

        for (int i = 0; i < ModifyTtxtsFr.Length; i++)
        {
            ModifyTtxtsFr[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 100, ModifyTtxtsSp[i].transform.localPosition.z);
        }

        Mode = "French";
    }

    public void ToggleFr()
    {
        if (Account.Instance.French.isOn)
        {
            languageSystem.Setting(LanguageType.French);

            for (int i = 0; i < ModifyButtonsFr.Length; i++)
            {
                ModifyButtonsFr[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1268.75f, 306.25f);
            }

            for (int i = 0; i < ModifyTtxtsFr.Length; i++)
            {
                ModifyTtxtsFr[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 100, ModifyTtxtsSp[i].transform.localPosition.z);
            }

            Mode = "French";
        }
    }

    public void SelectSp()
    {        
        languageSystem.Setting(LanguageType.Spanish);

        for (int i = 0; i < ModifyButtonsSp.Length; i++)
        {
            ModifyButtonsSp[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1268.75f, 306.25f);
        }

        for (int i = 0; i < ModifyTtxtsSp.Length; i++)
        {
            ModifyTtxtsSp[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 100, ModifyTtxtsSp[i].transform.localPosition.z);
        }

        Mode = "Spanish";
    }

    public void ToggleSp()
    {
        if (Account.Instance.Spanish)
        {
            languageSystem.Setting(LanguageType.Spanish);

            for (int i = 0; i < ModifyButtonsSp.Length; i++)
            {
                ModifyButtonsSp[i].GetComponent<RectTransform>().sizeDelta = new Vector2(1268.75f, 306.25f);
            }

            for (int i = 0; i < ModifyTtxtsSp.Length; i++)
            {
                ModifyTtxtsSp[i].transform.localPosition = new Vector3(ModifyTtxtsSp[i].transform.localPosition.x, 100, ModifyTtxtsSp[i].transform.localPosition.z);
            }

            Mode = "Spanish";
        }
    }

    //void Awake()
    //{
    //    instance = this;
    //    LoadLanguage();

    //    DontDestroyOnLoad(this);     
    //}

    //private void LoadLanguage()
    //{
    //    TextAsset ta = Resources.Load<TextAsset>(language.ToString());

    //    if (ta == null)
    //    {
    //        Debug.LogWarning("This language does not exist.");
    //        return;
    //    }

    //    // Get every lines.
    //    string[] lines = ta.text.Split('\n');

    //    // Get key value
    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        // Detect
    //        if (string.IsNullOrEmpty(lines[i]))
    //        {
    //            continue;
    //        }

    //        // Get key:kv[0] value kv[1]
    //        string[] kv = lines[i].Split(':');

    //        // Save into dict
    //        dict.Add(kv[0], kv[1]);

    //        Debug.Log(string.Format("key:{0}, value:{1}", kv[0], kv[1]));
    //    }
    //}

    //public string GetText(string key)
    //{
    //    if (dict.ContainsKey(key))
    //    {
    //        return dict[key];
    //    }
    //    // This key does not exist.
    //    else
    //    {
    //        return string.Empty;
    //    }
    //}
}