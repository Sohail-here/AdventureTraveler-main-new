using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class RecordUI : MonoBehaviour
{
    public TextMeshProUGUI DurationText;
    public TextMeshProUGUI MilesText;
    public Button CapBtn;
    public bool IsPause = false;
    private DateTime startTime;

    public Photo_Video mPhotoPage;

    private void Start()
    {
        CapBtn.onClick.AddListener(() =>
        {
            mPhotoPage.gameObject.SetActive(true);
            AR_Controller.Instance.OpenAR();
        });
    }

    public void OnEnable()
    {
        if (ActivityManager.Instance != null && ActivityManager.Instance.ListViewMgr != null)
            ActivityManager.Instance.ListViewMgr.ClearAllMarker();
    }

    public void StartRecoad()
    {
        startTime = DateTime.Now;
        StopInvoke();
        //Time Start
        InvokeRepeating("UpdateTime", 0, 1);

    }

    public void StopInvoke()
    {
        CancelInvoke("UpdateTime");
        DurationText.text = "00:00:00";
    }

    void UpdateTime()
    {
        DurationText.text = (DateTime.Now - startTime).ToString("hh\\:mm\\:ss");
        RecordManager.Instance.UpdateTotalTime(DurationText.text);
    }

    public void AddTime(string time)
    {
        if (string.IsNullOrEmpty(time) == false)
        {
            ULog.Penguin.Log(time);
            startTime = DateTime.Now;
            var t = time.Split(':');

            ULog.Penguin.Log($"{t[0]} {t[1]} {t[2]}");
            if (double.TryParse(t[2], out double sec))
                startTime = startTime.AddSeconds(-sec);
            if (double.TryParse(t[1], out double minute))
                startTime = startTime.AddMinutes(-minute);
            if (double.TryParse(t[0], out double hour))
                startTime = startTime.AddHours(-hour);

            UpdateTime();
        }
    }

    public void UpdateMiles(string miles)
    {
        MilesText.text = $"{miles}";
    }
    public void StartAvatar()
    {
        mPhotoPage.gameObject.SetActive(true);
        AR_Controller.Instance.OpenAR();
    }
}
