using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputEx : Button
{
    [SerializeField] private TMP_InputField mInputField;

    protected override void Start()
    {
        base.Start();
        mInputField = gameObject.GetComponentInChildren<TMP_InputField>();
        mInputField.enabled = false;
        mInputField.onFocusSelectAll = true;
        mInputField.interactable = true;
        targetGraphic = mInputField.targetGraphic;
        mInputField.onEndEdit.AddListener(OnEndEdit);
        onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        mInputField.enabled = true;
        mInputField.Select();

    }

    void OnEndEdit(string str)
    {
        mInputField.enabled = false;
    }
}
