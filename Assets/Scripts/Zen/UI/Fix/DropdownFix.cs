using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownFix : MonoBehaviour
{
    Dropdown.OptionData thisData;
    List<Dropdown.OptionData> dataMessages = new List<Dropdown.OptionData>();

    TMP_Dropdown thisDropdown;
    string thisString;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        thisDropdown = GetComponent<TMP_Dropdown>();
        thisDropdown.ClearOptions();

        InitDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDropdown()
    {
        thisDropdown = GetComponent<TMP_Dropdown>();
        thisDropdown.ClearOptions();

        for (int i = 80; i < 221; i++)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = i.ToString() + " Pounds";
            dataMessages.Add(data);
        }
    }
}
