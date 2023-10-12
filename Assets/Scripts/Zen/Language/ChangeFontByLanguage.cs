using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeFontByLanguage : MonoBehaviour
{
    public Font EnglishFont;
    public Font SpanishFont;
    public Font FrenchFont;

    public TMP_FontAsset EnglishFontAsset;
    public TMP_FontAsset SpanishFontAsset;
    public TMP_FontAsset FrenchFontAsset;

    public bool isChangeSize = false;

    public int EnSize;
    public int SpSize;
    public int FrSize;

    public void SwitchToEnglish()
    {
        if (gameObject.GetComponent<Text>())
        {
            gameObject.GetComponent<Text>().font = EnglishFont;
            //Debug.Log("En Font");

            if (isChangeSize)
            {
                gameObject.GetComponent<Text>().fontSize = EnSize;
            }
        }

        if (gameObject.GetComponent<TMP_Text>())
        {
            gameObject.GetComponent<TMP_Text>().font = EnglishFontAsset;
            //Debug.Log("En FontAsset");

            if (isChangeSize)
            {
                gameObject.GetComponent<TMP_Text>().fontSize = EnSize;
            }
        }
    }

    public void SwitchToSpanish()
    {
        if (gameObject.GetComponent<Text>())
        {
            gameObject.GetComponent<Text>().font = SpanishFont;
            //Debug.Log("Sp Font");

            if (isChangeSize)
            {
                gameObject.GetComponent<Text>().fontSize = SpSize;
            }
        }

        if (gameObject.GetComponent<TMP_Text>())
        {
            gameObject.GetComponent<TMP_Text>().font = SpanishFontAsset;
            //Debug.Log("Sp FontAsset");

            if (isChangeSize)
            {
                gameObject.GetComponent<TMP_Text>().fontSize = SpSize;
            }
        }
    }

    public void SwitchToFrench()
    {
        if (gameObject.GetComponent<Text>())
        {
            gameObject.GetComponent<Text>().font = FrenchFont;
            //Debug.Log("Fr FontAsset");

            if (isChangeSize)
            {
                gameObject.GetComponent<Text>().fontSize = FrSize;
            }
        }

        if (gameObject.GetComponent<TMP_Text>())
        {
            gameObject.GetComponent<TMP_Text>().font = FrenchFontAsset;
            //Debug.Log("Fr FontAsset");

            if (isChangeSize)
            {
                gameObject.GetComponent<TMP_Text>().fontSize = FrSize;
            }
        }
    }
}