using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedInputFieldPlugin;

public class ClearTextOnEnable : MonoBehaviour
{
    public AdvancedInputField AinpEmail;
    public AdvancedInputField AinpPassword;

    private void OnEnable()
    {
        AinpEmail.Text = "";
        AinpPassword.Text = "";
    }
}
