using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using FfmpegUnity;
using TMPro;
using System;
using RestSharp;
using System.IO;

public class LoadingSchedule : MonoBehaviour
{
    public GameObject LoadingBar;

    [SerializeField] private Image LoadBarImg;
    [SerializeField] private TextMeshProUGUI LoadPText;
    [SerializeField] private Transform m_LoadingLine;

    List<string> mUploadList = new List<string>();
    Action mOnFinish = null;
    Action<string> mOnFail = null;
    string mJourneyid = string.Empty;
    Coroutine co_upload;

    LTDescr mLTD;

    const string UP_GPSDATA_KEY = "GPSData";
    const string MAKE_RECORD_KEY = "MakeRecord";

    private void Start()
    {
        mLTD = m_LoadingLine.LeanMoveLocalX(890, 2);
        mLTD.setOnStart(() => { m_LoadingLine.localPosition = new Vector3(-890, 0, 0); });
        mLTD.setLoopClamp();
    }

    private void OnEnable()
    {
        if (mLTD != null)
        {
            mLTD.passed = 0;
            mLTD.resume();
        }
        Reset();
    }

    private void OnDisable()
    {
        mLTD.pause();
    }

    public void StartUploadAllFile(List<string> uploadList, string journeyid, Action onFinish, Action<string> onFail)
    {
        LoadingBar.SetActive(true);
        Reset();

        if (uploadList == null || uploadList.Count == 0)
            uploadList = new List<string>();

        uploadList.Add(UP_GPSDATA_KEY);
        uploadList.Add(MAKE_RECORD_KEY);

        if (uploadList != null && uploadList.Count > 0)
        {
            ULog.Penguin.Log($"StartUpload -- {uploadList.Count}");
            mUploadList = uploadList;
            mJourneyid = journeyid;
            mOnFinish = onFinish;
            mOnFail = onFail;
            co_upload = StartCoroutine(UploadAllDatas());
        }
        else
        {
            LoadingBar.SetActive(true);
            LoadPText.text = $"100%";
            LoadBarImg.fillAmount = 1;
            onFinish?.Invoke();
        }
    }
    //Here
    IEnumerator UploadAllDatas()
    {
        yield return new WaitForEndOfFrame();
        int allUploadCount = mUploadList.Count;
        int uploadDonecount = 0;
        float progressRate = 0;

        ULog.Penguin.Log($"---- UploadAllDatas -- {mUploadList.Count}");
        foreach (var key in mUploadList)
        {
            switch (key)
            {
                case UP_GPSDATA_KEY:
                    if (RecordManager.Instance.SaveBuildRecordData() == false)
                    {
                        mOnFail?.Invoke(key);
                        StopCoroutine(co_upload);
                    }
                    else
                        ULog.Penguin.Log($"UP_GPSDATA Done");
                    break;
                case MAKE_RECORD_KEY:
                    if (RecordManager.Instance.CallBuildRecord() == false)
                    {
                        mOnFail?.Invoke(key);
                        StopCoroutine(co_upload);
                    }
                    else
                        ULog.Penguin.Log($"MAKE_RECORD_KEY Done");
                    break;
                default:
                    ULog.Penguin.Log($"---- UploadAllDatas -- {key}");
                    var client = new RestClient("https://api.i911adventure.com/");
                    var request = new RestRequest($"/user_upload/{mJourneyid}/", Method.POST);
                    request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
                    var uploadfullPath = Application.persistentDataPath + "/" + key;
                    byte[] bytes = File.ReadAllBytes(uploadfullPath);

                    request.AddFile("file", bytes, uploadfullPath);

                    IRestResponse response;// = client.Execute(request);
                    yield return response = client.Execute(request);
                    ULog.Penguin.Log($"----UploadAllDatas response -- {response.StatusCode}");
                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    {
                        mOnFail?.Invoke(key);
                        StopCoroutine(co_upload);
                    }
                    ULog.Penguin.Log(response.StatusCode + " UploadImage : " + response.Content);
                    break;
            }

            uploadDonecount++;
            progressRate = (float)uploadDonecount / (float)allUploadCount;
            LoadPText.text = $"{progressRate * 100:0.0}%";
            LoadBarImg.fillAmount = progressRate;
            yield return new WaitForEndOfFrame();
        }

        LoadPText.text = $"100%";
        LoadBarImg.fillAmount = 1;
        yield return new WaitForEndOfFrame();
        mUploadList.Clear();
        ClosePage();
        mOnFinish?.Invoke();
    }

    public void ClosePage()
    {
        LoadingBar.SetActive(false);
    }

    private void Reset()
    {
        LoadPText.text = "0%";
        LoadBarImg.fillAmount = 0;
    }
}