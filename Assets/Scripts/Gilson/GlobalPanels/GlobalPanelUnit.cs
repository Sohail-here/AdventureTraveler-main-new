using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FancyScrollView;
using AdventureTraveler.Scroll;
using static UnDestroyData;
using Gilson.Network;

namespace AdventureTraveler
{
    public class GlobalPanelUnit : FancyScrollRectCell<JourneyMsg, ScrollUnitContext>
    {
        [Header("UI")]
        [SerializeField] private RawImage VideoRawImage;
        [SerializeField] private Button VideoBtn;
        [SerializeField] private TMP_Text username;
        [SerializeField] private TMP_Text date;
        [SerializeField] private TMP_Text distance;
        [SerializeField] private TMP_Text distanceUnit;
        [SerializeField] private Button MoreBtn;
        [SerializeField] private Button ReadMoreBtn;
        [SerializeField] private GameObject LoadBar;
        [SerializeField] private Texture2D defultLoadImg;
        [Header("Data")]
        [SerializeField] private JourneyMsg tempMsg;
        [SerializeField] private string videoUrl;
        NetLoaderDataForTexture2D cacha_download_data;

        void OnEnable()
        {
            LoadBar.LeanRotateZ(-3600, 10).setOnStart(() => { LoadBar.LeanResume(); }).setOnComplete(() =>
            {
                LoadBar.SetActive(false);
            });
        }

        void Start()
        {
            Debug.Log("Unit Start");

            ButtonExpend.Instance.AddBtn(ReadMoreBtn);
            ReadMoreBtn.onClick.AddListener(Home.Instance.AddPage);

            VideoBtn.onClick.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(videoUrl))
                {
                    Debug.Log($"VideoBtn Url={videoUrl}");
#if !UNITY_ANDROID
                    WebView.Instance.Open(videoUrl);
#else
                    WebViewMgr.Instance.OpenURL(videoUrl);
#endif
                }
                else
                {
                    Home.Instance.VideoDetailItem.ShowDetail(ref tempMsg);
                    Debug.Log("Url == null !");
                }
            });

            MoreBtn.onClick.AddListener(ShowDetail);
        }

        public void ShowDetail()
        {
            if (string.IsNullOrEmpty(videoUrl))
            {
                Home.Instance.VideoDetailItem.ShowDetail(ref tempMsg);
            }
            else
            {
                Home.Instance.VideoDetailItem.ShowDetail(ref tempMsg, ref VideoRawImage, videoUrl);
            }
        }

        public override void UpdateContent(JourneyMsg itemData)
        {
            videoUrl = string.Empty;
            tempMsg = itemData;
            username.text = itemData.name;
            date.text = itemData.duration;
            distance.text = itemData.distance;
            LoadBar.SetActive(true);
            VideoRawImage.texture = defultLoadImg;
            bool logoLoadDone = false;
            foreach (var res in itemData.resources)
            {
                if (res.resource_url.Contains("Record.mp4"))
                {
                    videoUrl = res.resource_url;
                }

                if (logoLoadDone == false && res.resource_url.Contains("Logo"))
                {
                    if (cacha_download_data == null || cacha_download_data.url != res.resource_url)
                    {
                        logoLoadDone = true;
                        cacha_download_data = new NetLoaderDataForTexture2D
                        {
                            url = res.resource_url,
                            SuccessAction = (url, tex) =>
                            {
                                LoadBar.SetActive(false);
                                if (url.Contains(".mp4"))
                                {
                                    Debug.Log("url.Contains.mp4");
                                    VideoRawImage.texture = defultLoadImg;
                                }
                                else if (cacha_download_data.url == url)
                                {
                                    VideoRawImage.texture = tex;
                                }

                            }

                        };
                    }
                    NetLoaderToolForTexture.Instance.DownLoader(cacha_download_data);
                }


            }

            if (Index == Home.Instance.DataScrollView.DataCount - 1)
            {
                ReadMoreBtn.gameObject.SetActive(true);
            }
            else if (ReadMoreBtn.gameObject.activeInHierarchy)
            {
                ReadMoreBtn.gameObject.SetActive(false);
            }
        }
    }
}