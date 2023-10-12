using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewPanel : MonoSingleton<WebViewPanel>
{
    public RectTransform frame;

    private UniWebView webView;

    public void Open(string url)
    {
        this.gameObject.SetActive(true);

        this.CreateNewWebView(url, () =>
        {

        });
    }

    public void Close()
    {
        if (this.webView != null)
        {
            Destroy(this.webView.gameObject);
            this.webView = null;
            this.gameObject.SetActive(false);
        }
    }

    private void CreateNewWebView(string loadUrl, System.Action onPageFinished)
    {
        var go = new GameObject("UniWebView");
        this.webView = go.AddComponent<UniWebView>();
        this.webView.ReferenceRectTransform = this.frame;
        this.webView.Load(loadUrl);
        this.webView.Show();

        // add events
        this.webView.OnPageFinished += (view, statusCode, url) =>
        {
            onPageFinished();
        };

        this.webView.OnShouldClose += (view) =>
        {
            this.Close();
            return true;
        };
    }

    public void ForcePortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
