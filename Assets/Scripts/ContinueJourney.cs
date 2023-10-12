using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueJourney : MonoBehaviour
{

    [SerializeField] Button ContinueBtn;
    [SerializeField] Button DiscardBtn;
    Action mOnContinue;
    Action mOnDiscard;

    void Start()
    {
        ContinueBtn.onClick.AddListener(OnContinueClick);
        DiscardBtn.onClick.AddListener(OnDiscardClick);
    }

    void OnContinueClick()
    {
        MainToggle.Instance.tgl_Record.isOn = true;
        mOnContinue?.Invoke();
        ClosePage();
    }

    void OnDiscardClick()
    {
        mOnDiscard?.Invoke();
        ClosePage();
    }

    void ClosePage()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenPage(Action onContinue, Action onDiscard)
    {
        mOnContinue = onContinue;
        mOnDiscard = onDiscard;
        this.gameObject.SetActive(true);
    }
}
