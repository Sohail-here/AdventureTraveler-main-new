using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordJourneyShortTip : MonoBehaviour
{
    public Button CloseBtn;
    public Button BackBtn;
    public Button DiscordBtn;
    Action onBack;
    Action onDiscard;
    private void Start()
    {
        CloseBtn.onClick.AddListener(Close);
        BackBtn.onClick.AddListener(Close);
        DiscordBtn.onClick.AddListener(Discard);
    }

    public void OpenUI(Action backevent, Action discard)
    {
        this.gameObject.SetActive(true);
        onBack = backevent;
        onDiscard = discard;
    }

    void Discard()
    {
        this.gameObject.SetActive(false);
        onDiscard?.Invoke();
    }
    void Close()
    {
        this.gameObject.SetActive(false);
        onBack?.Invoke();
    }
}
