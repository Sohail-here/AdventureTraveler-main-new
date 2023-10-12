using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMP_Debug : MonoSingleton<TMP_Debug>
{
    public TMP_Text DebugtText;

    public void DebugPrint(string msg)
    {
        DebugtText.text += msg;
        DebugtText.text += "\n";
    }
}