using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using Gilson.UniversalMethods;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace AdventureTraveler.Language
{
    public enum LanguageType
    {
        English,
        French,
        Spanish
    }

    [System.Serializable]
    public class LanguageContent
    {
        [TextArea(1, 2)] public string english;
        [TextArea(1, 2)] public string french;
        [TextArea(1, 2)] public string spanish;
    }
    [System.Serializable]
    public class LanguageJsonFormatBase
    {
        [TextArea(1, 2)] public string label;
        public LanguageContent content;
    }
    [System.Serializable]
    public class LanguageJsonFormat
    {
        public List<LanguageJsonFormatBase> languages = new List<LanguageJsonFormatBase>();
    }
    [System.Serializable]
    public class LanguageUnit : LanguageJsonFormatBase
    {
        public List<Text> unitText = new List<Text>();
        public List<TextMeshProUGUI> unitTextMesh = new List<TextMeshProUGUI>();

        public void SetContent(LanguageType type)
        {
            if (unitText.Count == 0 && unitTextMesh.Count == 0) return;

            string targetString;

            switch (type)
            {
                default:
                case LanguageType.English:
                    targetString = content.english;
                    break;
                case LanguageType.French:
                    targetString = content.french;
                    break;
                case LanguageType.Spanish:
                    targetString = content.spanish;
                    break;
            }

            for (int i = 0; i < unitText.Count; i++)
            {
                if (unitText[i])
                {
                    unitText[i].text = targetString;
                    Debug.Log("Switch Type-- " + type);
                    switch (type)
                    {
                        case LanguageType.English:
                            try
                            {
                                //unitText[i].font = unitText[i].GetComponent<ChangeFontByLanguage>().EnglishFont;
                                unitText[i].GetComponent<ChangeFontByLanguage>().SwitchToEnglish();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitText[i].name + e);
                            }
                            break;
                        case LanguageType.Spanish:
                            try
                            {
                                //unitText[i].font = unitText[i].GetComponent<ChangeFontByLanguage>().SpanishFont;
                                unitText[i].GetComponent<ChangeFontByLanguage>().SwitchToSpanish();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitText[i].name + e);
                            }
                            break;
                        case LanguageType.French:
                            try
                            {
                                //unitText[i].font = unitText[i].GetComponent<ChangeFontByLanguage>().FrenchFont;
                                unitText[i].GetComponent<ChangeFontByLanguage>().SwitchToFrench();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitText[i].name + e);
                            }
                            break;
                    }
                }
            }

            for (int j = 0; j < unitTextMesh.Count; j++)
            {
                if (unitTextMesh[j])
                {
                    unitTextMesh[j].text = targetString;
                    Debug.Log("Switch Type TMP-- " + type);
                    switch (type)
                    {
                        case LanguageType.English:
                            try
                            {
                                //unitTextMesh[j].font = unitTextMesh[j].GetComponent<ChangeFontByLanguage>().EnglishFontAsset;
                                unitTextMesh[j].GetComponent<ChangeFontByLanguage>().SwitchToEnglish();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitTextMesh[j].name + e);
                            }
                            break;
                        case LanguageType.Spanish:
                            try
                            {
                                //unitTextMesh[j].font = unitTextMesh[j].GetComponent<ChangeFontByLanguage>().SpanishFontAsset;
                                unitTextMesh[j].GetComponent<ChangeFontByLanguage>().SwitchToSpanish();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitTextMesh[j].name + e);
                            }
                            break;
                        case LanguageType.French:
                            try
                            {
                                //unitTextMesh[j].font = unitTextMesh[j].GetComponent<ChangeFontByLanguage>().FrenchFontAsset;
                                unitTextMesh[j].GetComponent<ChangeFontByLanguage>().SwitchToFrench();
                            }
                            catch (System.Exception e)
                            {
                                Debug.Log(unitTextMesh[j].name + e);
                            }
                            break;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class EventForLanguage : UnityEvent<LanguageType> { }

    public class LanguageSystem : MonoBehaviour
    {

#if UNITY_EDITOR
        [SerializeField] private LanguageJsonFormat excelDemoData;
#endif

        [SerializeField] private List<LanguageUnit> languageUnits = new List<LanguageUnit>();

        public EventForLanguage OnLanguageSetting;

        static LanguageType languageType = LanguageType.English;
        public static LanguageType GetLanguage => languageType;

        readonly string saveTypeKey = "languageType";

        void Awake()
        {
            if (!ES3.KeyExists(saveTypeKey))
                ES3.Save(saveTypeKey, languageType);
            else
                languageType = ES3.Load<LanguageType>(saveTypeKey);

            // Auto Setting 
            //Setting(languageType);

            if (languageType == LanguageType.English)
            {
                LanguageManager.Instance.SelectEn();
            }
            else if (languageType == LanguageType.French)
            {
                LanguageManager.Instance.SelectFr();
            }
            else if (languageType == LanguageType.Spanish)
            {
                LanguageManager.Instance.SelectSp();
            }
        }

        public void Setting(LanguageType type)
        {
            Debug.Log($"languageType = {type}");

            for (int i = 0; i < languageUnits.Count; i++)
            {
                Debug.Log("ININII-- " + type);
                languageUnits[i].SetContent(type);
            }

            if (languageType != type)
            {
                languageType = type;
                ES3.Save(saveTypeKey, languageType);
            }           
        }

#if UNITY_EDITOR
        public void FindLanguageTexts(Text[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                var selectUnit = from unit in languageUnits
                                 where unit.label.Contains(texts[i].text)
                                 select unit;

                if (selectUnit.Count() > 0)
                {
                    if (!selectUnit.First().unitText.Contains(texts[i]))
                        selectUnit.First().unitText.Add(texts[i]);
                }
            }
        }

        public void FindLanguageTexts(TextMeshProUGUI[] textMeshPros)
        {
            for (int i = 0; i < textMeshPros.Length; i++)
            {
                var selectUnit = from unit in languageUnits
                                 where unit.label.Contains(textMeshPros[i].text)
                                 select unit;

                if (selectUnit.Count() > 0)
                {
                    if (!selectUnit.First().unitTextMesh.Contains(textMeshPros[i]))
                        selectUnit.First().unitTextMesh.Add(textMeshPros[i]);
                }
            }
        }

        public void LoadExcel(string excelPath)
        {
            LanguageJsonFormat languageJsonFormat = new LanguageJsonFormat();
            List<LanguageJsonFormatBase> languages = new List<LanguageJsonFormatBase>();

            var sheet = new ES3Spreadsheet();
            sheet.Load(excelPath);

            Debug.Log(sheet.ColumnCount);
            Debug.Log(sheet.RowCount);

            for (int row = 1; row < sheet.RowCount; row++)
            {
                LanguageJsonFormatBase data = new LanguageJsonFormatBase();
                data.content = new LanguageContent();

                for (int col = 0; col < sheet.ColumnCount; col++)
                {
                    switch ((LanguageType)col)
                    {
                        case LanguageType.English:
                            data.label = sheet.GetCell<string>(col, row);
                            data.content.english = sheet.GetCell<string>(col, row);
                            break;
                        case LanguageType.French:
                            data.content.spanish = sheet.GetCell<string>(col, row);
                            break;
                        case LanguageType.Spanish:
                            data.content.french = sheet.GetCell<string>(col, row);
                            break;
                    }
                }

                if (string.IsNullOrEmpty(data.label))
                    continue;

                if (languages.Count == 0)
                {
                    languages.Add(data);
                    continue;
                }

                var languageSelect = from unit in languages
                                     where unit.label.Equals(data.label)
                                     select unit;

                if (languageSelect.Count() > 0)
                    continue;

                languages.Add(data);
            }

            languageJsonFormat.languages = languages;
            excelDemoData = languageJsonFormat;
        }

        public void ExcelToEditor()
        {
            languageUnits.Clear();

            for (int i = 0; i < excelDemoData.languages.Count; i++)
            {
                LanguageUnit unit = new LanguageUnit();
                unit.label = excelDemoData.languages[i].label;
                unit.content = excelDemoData.languages[i].content;
                languageUnits.Add(unit);
            }
        }
#endif
    }
}