using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveShareJourney : MonoBehaviour
{
    [SerializeField] private Button LiveJourneyBtn;
    [SerializeField] private JoinJourneyPageUI JoinJourneyUI;

    void Start()
    {
        LiveJourneyBtn.onClick.AddListener(OnLiveJourney);
    }

    void OnLiveJourney()
    {
        JoinJourneyUI.Open((recordData) =>
        {
            RecordManager.Instance.ReView(recordData.Datas[0]);
        });
    }

    public void Close()
    {
        JoinJourneyUI.Close();
    }

}
