using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordFailPage : MonoBehaviour
{
    public GameObject FailPageObj;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI ContentText;
    public Button TryAgainBtn;
    public TextMeshProUGUI TryAgainBtnText;
    public Button DiscardBtn;
    public TextMeshProUGUI DiscardBtnText;

    private Action mOnDiscard;
    private Action mOnTryAgain;

    private void Start()
    {
        TryAgainBtn.onClick.AddListener(OnBtnDiscard);
        DiscardBtn.onClick.AddListener(OnBtnTryAgain);
    }

    public void OpenPage(string title, string content, Action onTryAgain, Action onDiscard, string btnTryAgainStr = "", string btnDiscardStr = "")
    {
        mOnDiscard = onDiscard;
        mOnTryAgain = onTryAgain;
        TitleText.text = title;
        ContentText.text = content;
        FailPageObj.SetActive(true);
        if (!string.IsNullOrEmpty(btnTryAgainStr))
            TryAgainBtnText.text = btnTryAgainStr;
        if (!string.IsNullOrEmpty(btnDiscardStr))
            DiscardBtnText.text = btnDiscardStr;
    }

    public void ClosePage()
    {
        FailPageObj.SetActive(false);
    }

    void OnBtnDiscard()
    {
        ULog.Penguin.Log("OnBtnDiscard");
        mOnDiscard?.Invoke();
        FailPageObj.SetActive(false);
    }

    void OnBtnTryAgain()
    {
        ULog.Penguin.Log("OnBtnTryAgain");
        mOnTryAgain?.Invoke();
        FailPageObj.SetActive(false);
    }

}
