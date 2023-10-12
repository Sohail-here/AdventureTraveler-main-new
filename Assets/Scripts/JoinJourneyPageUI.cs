using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinJourneyPageUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_Input;
    [SerializeField] private Button m_JoinBtn;
    [SerializeField] private Button m_MapBackBtn;
    [SerializeField] private Button m_BackBtn;
    [SerializeField] private GameObject m_MapCamear;
    [SerializeField] private GameObject m_MapObject;
    [SerializeField] private GameObject m_AccountPage;
    [SerializeField] private GameObject m_Page;
    [SerializeField] private GameObject m_WarPage;
    [SerializeField] private Button m_CloseWarBtn;

    private string mShareCode = "";
    Action<RecordManager.Root> onJoin;
    private void Start()
    {
        m_JoinBtn.onClick.AddListener(OnJoinJourney);
        m_MapBackBtn.onClick.AddListener(OnMapBack);
        m_BackBtn.onClick.AddListener(OnBackBtn);
        m_CloseWarBtn.onClick.AddListener(OnClosePage);
    }

    public void Open(Action<RecordManager.Root> action)
    {
        onJoin = action;
        m_MapBackBtn.gameObject.SetActive(false);
        m_MapCamear.gameObject.SetActive(false);
        m_BackBtn.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_MapBackBtn.gameObject.SetActive(false);
    }

    private void OnJoinJourney()
    {
        if (string.IsNullOrEmpty(m_Input.text) == false)
        {
            mShareCode = m_Input.text;
            CancelInvoke("UpdateShareJourney");
            InvokeRepeating("UpdateShareJourney", 0, 30);
        }
    }

    private async void UpdateShareJourney()
    {
        RestClient client = new RestClient(RecordManager.API_PATH);
        RestRequest request = new RestRequest($"Values/GetJourney,{mShareCode}", Method.GET);
        var response = await client.ExecuteAsync(request);

        try
        {
            if (string.IsNullOrEmpty(response.Content) == false && response.Content != "None")
            {
                Debug.LogWarning(response.Content);
                RecordManager.Root data = JsonConvert.DeserializeObject<RecordManager.Root>(JsonConvert.DeserializeObject<string>(response.Content));
                Debug.Log($"Data {data.Datas.Count}");
                if (data != null && data.Datas.Count > 0)
                {
                    m_MapObject.SetActive(true);
                    m_MapCamear.gameObject.SetActive(true);
                    m_MapBackBtn.gameObject.SetActive(true);
                    m_BackBtn.gameObject.SetActive(false);
                    m_Page.SetActive(false);
                    m_AccountPage.SetActive(false);
                    onJoin?.Invoke(data);
                }
                else
                {
                    m_WarPage.SetActive(true);
                    CancelInvoke("UpdateShareJourney");
                }
            }
            else
            {
                m_WarPage.SetActive(true);
                CancelInvoke("UpdateShareJourney");
            }
        }
        catch
        {
            m_WarPage.SetActive(true);
            CancelInvoke("UpdateShareJourney");
        }
    }

    private void OnBackBtn()
    {
        CancelInvoke("UpdateShareJourney");
        gameObject.SetActive(false);
    }

    private void OnMapBack()
    {
        if (gameObject.activeInHierarchy)
        {
            CancelInvoke("UpdateShareJourney");
            m_MapCamear.gameObject.SetActive(false);
            m_MapObject.SetActive(false);
            m_Page.SetActive(true);
            m_MapBackBtn.gameObject.SetActive(false);
            m_BackBtn.gameObject.SetActive(true);
            m_AccountPage.SetActive(true);
        }
    }

    private void OnMapBackFromRecord()
    {
        CancelInvoke("UpdateShareJourney");
        m_Page.SetActive(true);
        m_MapBackBtn.gameObject.SetActive(false);
        m_BackBtn.gameObject.SetActive(true);
    }

    private void OnClosePage()
    {
        m_WarPage.SetActive(false);
    }

    public void Close()
    {
        OnMapBackFromRecord();
        OnBackBtn();
    }
}
