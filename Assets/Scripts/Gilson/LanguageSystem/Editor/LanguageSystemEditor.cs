using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

namespace AdventureTraveler.Language
{
    [CustomEditor(typeof(LanguageSystem))]
    public class LanguageSystemEditor : Editor
    {
        LanguageSystem languageSystem;
        string ExcelPath;

        public override void OnInspectorGUI()
        {
            languageSystem = (LanguageSystem)target;

            if (GUILayout.Button("LoadExcel"))
            {
                string path = EditorUtility.OpenFilePanel("Load Excel", "", "csv");
                if (string.IsNullOrEmpty(path)) return;
                languageSystem.LoadExcel(path);
            }

            if (GUILayout.Button("Excel To Editor"))
            {
                languageSystem.ExcelToEditor();
            }

            if (GUILayout.Button("Find Text"))
            {
                var findText = FindObjectsOfType<Text>(true);
                var findTextPro = FindObjectsOfType<TextMeshProUGUI>(true);

                languageSystem.FindLanguageTexts(findText);
                languageSystem.FindLanguageTexts(findTextPro);
            }

            EditorGUILayout.LabelField("language Type : ", LanguageSystem.GetLanguage.ToString());

            EditorGUILayout.BeginHorizontal("Toggle");

            if (GUILayout.Button("Engilsh"))
            {
                //languageSystem.Setting(LanguageType.English);
                LanguageManager.Instance.SelectEn();
            }

            if (GUILayout.Button("French"))
            {
                //languageSystem.Setting(LanguageType.French);
                LanguageManager.Instance.SelectFr();
            }
            if (GUILayout.Button("Spanish"))
            {
                //languageSystem.Setting(LanguageType.Spanish);
                LanguageManager.Instance.SelectSp();
            }

            EditorGUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
}