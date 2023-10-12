using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using AdventureTraveler.Scroll;
using Vuplex.WebView;

public class Home : MonoSingleton<Home>
{
    //Global
    public GlobalPanelScrollView DataScrollView;
    //Ower
    public GlobalPanelScrollView MyJourneyDataScrollView;
    public Toggle GlobalToggle;
    public Toggle MyJourneyToggle;
    public VideoDeital VideoDetailItem;
    //SV Content
    public GameObject GlobalContent;
    public GameObject MyJourneyContent;

    public GameObject MyJourneysHasNothing;
    Coroutine mCoroutine = null;
    Dictionary<bool, List<UnDestroyData.JourneyMsg>> mAllJourneyData = new Dictionary<bool, List<UnDestroyData.JourneyMsg>>();


    private int globalPage = 1;
    private int myJourneyPage = 1;
    private bool isMyJourney = false;

    void Start()
    {
        //init Data
        mAllJourneyData.Add(false, new List<UnDestroyData.JourneyMsg>());
        mAllJourneyData.Add(true, new List<UnDestroyData.JourneyMsg>());

        GlobalLoadMore();
        Refresh();

        GlobalToggle.onValueChanged.AddListener((b) =>
        {
            if (b && GlobalContent.activeInHierarchy != b) GlobalLoadMore();
        });
        MyJourneyToggle.onValueChanged.AddListener((b) =>
        {
            if (b && MyJourneyContent.activeInHierarchy != b) MyJourneysLoadMore();
        });

#if !UNITY_ANDROID
        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
#endif
    }

    public void AddPage()
    {
        if (isMyJourney)
        {
            myJourneyPage++;
        }
        else
        {
            globalPage++;
        }
        Refresh();
    }

    [ContextMenu("ClearAllJourney")]
    public void ClearAllJourney()
    {
        ULog.Penguin.Log("Clear All AllJourneyData");
        StopCoroutine(mCoroutine);
        mAllJourneyData.Clear();
        mAllJourneyData.Add(false, new List<UnDestroyData.JourneyMsg>());
        mAllJourneyData.Add(true, new List<UnDestroyData.JourneyMsg>());
        MyJourneyDataScrollView.UpdateData(new List<UnDestroyData.JourneyMsg>());
        DataScrollView.UpdateData(new List<UnDestroyData.JourneyMsg>());
        myJourneyPage = 1;
        globalPage = 1;
    }

    public void Refresh()
    {
        if (this.gameObject.activeInHierarchy == false)
            return;

        int readIndex = isMyJourney ? myJourneyPage : globalPage;
        if (readIndex > UnDestroyData.journeys.total_page)
        {
            readIndex = 1;
            if (isMyJourney)
                myJourneyPage = 1;
            else
                globalPage = 1;
        }

        mCoroutine = StartCoroutine(GetJourneysData(OnComplete, isMyJourney, readIndex));
    }

    public void OnComplete(bool isMy, int index, List<UnDestroyData.JourneyMsg> newData)
    {
        foreach (var data in newData)
        {
            var journeyIndex = mAllJourneyData[isMy].FindIndex(i => { return i.journey_id == data.journey_id; });
            if (journeyIndex > -1)
            {
                mAllJourneyData[isMy][journeyIndex] = data;
            }
            else
            {
                mAllJourneyData[isMy].Add(data);
            }
        }
        var updateData = mAllJourneyData[isMy];

        if (isMy)
        {
            MyJourneysHasNothing.SetActive(updateData.Count == 0);
            MyJourneyDataScrollView.UpdateData(updateData);
        }
        else
        {
            DataScrollView.UpdateData(updateData);
        }

        //DataScrollView.ScrollTo(0, 1f, EasingCore.Ease.Linear);
    }


    // https://api.i911adventure.com/journeys/1 GET    working
    //{
    //        "journey_id": "5af7a040-f812-4c36-b933-5320a7407a60",
    //        "name": "",
    //        "duration": "00:03:10",
    //        "distance": "0.08",
    //        "created": 1693694022,
    //        "resources": [],
    //        "category": "Walking"
    //}
IEnumerator GetJourneysData(Action<bool, int, List<UnDestroyData.JourneyMsg>> onComplete, bool myJourney, int page)
    {
        WWWForm form = new WWWForm();
        string wwwStr = $@"https://api.i911adventure.com/journeys/{page}";
        if (myJourney)
            wwwStr += "/?me=true";

        ULog.Penguin.Log($"wwwStr={wwwStr}");
        List<UnDestroyData.JourneyMsg> newData = new List<UnDestroyData.JourneyMsg>();
        using (UnityWebRequest www = UnityWebRequest.Get(wwwStr))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "JWT " + UnDestroyData.token);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                UIManager.Instance.WarnPanelTitle("GetJourneysError");
                UIManager.Instance.WarnPanelContent(www.downloadHandler.text);
            }
            else
            {
                UnDestroyData.journeys = JsonUtility.FromJson<UnDestroyData.Journeys>(www.downloadHandler.text);
                newData = UnDestroyData.journeys.msg;
            }
        }

        onComplete?.Invoke(myJourney, page, newData);
    }

    #region Global

    public void GlobalLoadMore()
    {
        Debug.Log("GlobalLoadMore");
        if (!GlobalContent.activeInHierarchy)
            GlobalContent.SetActive(true);
        if (MyJourneyContent.activeInHierarchy)
            MyJourneyContent.SetActive(false);

        isMyJourney = false;
        globalPage = 1;
        Refresh();
    }

    #endregion

    #region MyJourneys

    public void MyJourneysLoadMore()
    {
        if (GlobalContent.activeInHierarchy)
            GlobalContent.SetActive(false);
        if (!MyJourneyContent.activeInHierarchy)
            MyJourneyContent.SetActive(true);

        isMyJourney = true;
        myJourneyPage = 1;
        Refresh();
    }

    #endregion
}