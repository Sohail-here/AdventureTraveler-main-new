using UnityEngine.UI;
using UnityEngine;
#if UNITY_ANDROID
using Vuplex.WebView;
#endif
using System.Collections;

public class WebViewMgr : MonoSingleton<WebViewMgr>
{
#if UNITY_ANDROID
    public CanvasWebViewPrefab AndroidWebView;
    private CanvasWebViewPrefab cachePrefab;
#endif
    public GameObject WebView;
    public Transform RootTrans;
    public GameObject WebViewPanel;
    public Button CloseBtn;
#if UNITY_ANDROID
    private void Start()
    {
        WebView.SetActive(false);
        CloseBtn.onClick.AddListener(() =>
        {
            cachePrefab?.Destroy();
            WebViewPanel.SetActive(false);
        });
    }
    public void OpenURL(string url)
    {
        WebViewPanel.SetActive(true);
        CreateWebView(url);
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    IEnumerator CheckForChange()
    {
        ScreenOrientation sO = Screen.orientation;
        DeviceOrientation cacheD = DeviceOrientation.LandscapeRight;
        while (cachePrefab != null)
        {
            if (sO != Screen.orientation || cacheD != Input.deviceOrientation)
            {
                cacheD = Input.deviceOrientation;
                ULog.Penguin.Log(cacheD);
                var rectTrasn = cachePrefab.transform as RectTransform;
                if (cacheD == DeviceOrientation.Unknown || Screen.orientation == ScreenOrientation.Portrait || cacheD == DeviceOrientation.Portrait || cacheD == DeviceOrientation.FaceUp || cacheD == DeviceOrientation.FaceDown)
                {
                    rectTrasn.offsetMax = new Vector2(0, -130);
                }
                else
                {
                    rectTrasn.offsetMax = Vector2.zero;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    async void CreateWebView(string url)
    {
        var webView = GameObject.Instantiate(AndroidWebView, RootTrans);
        cachePrefab = webView;
        var rectTrasn = cachePrefab.transform as RectTransform;
        await webView.WaitUntilInitialized();
        webView.Visible = true;
        webView.Destroyed += (o, e) =>
        {
            ULog.Penguin.Log($"Close WebView");
            Screen.orientation = ScreenOrientation.Portrait;
        };

        StartCoroutine(CheckForChange());
        //webView.Clicked += (o, e) =>
        //{
        //    ULog.Penguin.Log($"Close WebView {o.ToString()} {e.Point}");
        //    Screen.orientation = ScreenOrientation.Portrait;
        //    webView.Destroy();
        //};

        webView.WebView.CloseRequested += (o, e) =>
        {
            ULog.Penguin.Log($"Close WebView");
            Screen.orientation = ScreenOrientation.Portrait;
        };
        webView.WebView.LoadUrl(url);
    }
#else
    private void Start()
    {
        WebView.SetActive(true);
    }
#endif
}
