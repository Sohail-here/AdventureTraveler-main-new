using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedInputFieldPlugin;
using TMPro;

public class AInpWeightFix : MonoBehaviour
{
    public AdvancedInputField InputField;
    private string originText;

    // Start is called before the first frame update
    void Start()
    {
        originText = InputField.Text;
        InputField.OnEndEdit.AddListener((str, reason) =>
        {
            if (string.IsNullOrEmpty(InputField.Text))
            {
                InputField.Text = originText;
            }
            else
            {
                if (InputField.Text.Contains("Pounds") == false)
                {
                    InputField.Text += " Pounds";
                }
            }
        });
        InputField.OnSelectionChanged.AddListener(selected =>
        {
            if (selected)
                InputField.Text = "";
        });
    }
}