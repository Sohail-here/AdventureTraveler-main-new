using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class HistoryObj : MonoBehaviour
{
    public GameObject VideoRawImage;
    //public VideoPlayer videoPlayer;
    public string videoUrl = "Input Url";

    [Header("VideoDetail")]
    public GameObject Distance;
    public GameObject Duration;
    public GameObject Activity;
    public GameObject ScreenShot;

    private GameObject BtnDetailPlay;

    void Start()
    {
        VideoRawImage = GameObject.Find("Canvas").transform.Find("PlayVideoRawImage").gameObject;
        //videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        videoUrl = gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;

        GameObject detail = GameObject.Find("Canvas").transform.GetChild(9).GetChild(0).gameObject;

        Distance = detail.transform.GetChild(4).GetChild(1).gameObject;
        Duration = detail.transform.GetChild(4).GetChild(0).gameObject;
        Activity = detail.transform.GetChild(4).GetChild(2).gameObject;
        ScreenShot = detail.transform.GetChild(3).gameObject;

        BtnDetailPlay = detail.transform.GetChild(3).GetChild(0).gameObject;
    }

    public void PlayUrlVideo()
    {
#if UNITY_ANDROID
        WebViewMgr.Instance.OpenURL(videoUrl);
#else
        WebView.Instance.Open(videoUrl);
#endif
        Debug.Log("WebViewOpen");
    }

    public void OpenDetail()
    {
        Debug.Log("OpenDetail");

        // load distance
        Distance.GetComponent<TMP_Text>().text =
        transform.GetChild(0).
            gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text;

        // load duration
        Duration.GetComponent<TMP_Text>().text =
        transform.GetChild(3).
            gameObject.GetComponent<Text>().text;

        // load activity
        Activity.GetComponent<TMP_Text>().text =
        transform.GetChild(4).
            gameObject.GetComponent<Text>().text;

        // load screenshot
        ScreenShot.GetComponent<RawImage>().texture = gameObject.transform.GetChild(0).GetComponent<RawImage>().texture;

        BtnDetailPlay.GetComponent<Button>().onClick.AddListener(PlayUrlVideo);

        GameObject.Find("Canvas").transform.Find("GlobalPanels").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Share()
    {

    }

    public void Post()
    {

    }
}