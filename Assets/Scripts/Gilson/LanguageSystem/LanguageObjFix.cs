using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageObjFix : MonoBehaviour
{
    [Header("Mode")]
    public bool FixText;
    public bool FixSize;

    [Header("Variable")]
    public string English;
    public string French;
    public string Spanish;

    public float AfterFixSize;
    public float OriginSize;

    public TMP_Text Content;    

    // Start is called before the first frame update
    void Start()
    {
        if (FixText)
        {
            switch (LanguageManager.Instance.Mode)
            {
                case "English":
                    Content.text = English;
                    break;
                case "French":
                    Content.text = French;
                    break;
                case "Spanish":
                    Content.text = Spanish;
                    break;
                default:
                    break;
            }
        }

        if (FixSize)
        {
            switch (LanguageManager.Instance.Mode)
            {
                case "English":
                    Content.fontSize = OriginSize;
                    break;
                case "French":
                    Content.fontSize = AfterFixSize;
                    break;
                case "Spanish":
                    Content.fontSize = AfterFixSize;
                    break;
                default:
                    break;
            }
        }
    }    
}