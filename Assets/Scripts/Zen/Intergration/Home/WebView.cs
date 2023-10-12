using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebView : MonoSingleton<WebView>
{
    //public GameObject WebViewPanel;
    [SerializeField]
    private WebViewPanel m_WebView;

    public void Open(string url)
    {
        m_WebView.Open(url);
    }
}