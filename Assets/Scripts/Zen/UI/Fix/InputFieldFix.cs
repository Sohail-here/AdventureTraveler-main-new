using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldFix : MonoBehaviour
{
    private string originText;

    private void Start()
    {
        originText = gameObject.GetComponent<TMP_InputField>().text;
    }

    public void WeightOnSelect()
    {
        transform.GetComponent<TMP_InputField>().text = "";
    }

    public void WeightValueChanged()
    {
        
    }

    public void WeightEndEdit()
    {
        if (transform.GetComponent<TMP_InputField>().text == "")
        {
            transform.GetComponent<TMP_InputField>().text = originText;
        }
        else if (transform.GetComponent<TMP_InputField>().text.EndsWith("Pounds"))
        {
            return;
        }
        else
        {
            transform.GetComponent<TMP_InputField>().text += " Pounds";
        }

        /*
        if (isChanged)
        {
            transform.GetComponent<TMP_InputField>().text += " Pounds";
        }
        else
        {
            transform.GetComponent<TMP_InputField>().text = transform.GetComponent<TMP_InputField>().text;
        }
        isChanged = false;
        */
    }
}