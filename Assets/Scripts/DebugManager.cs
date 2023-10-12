using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoSingleton<DebugManager>
{
    public GameObject Root;
    public Button SpButton;
    public GameObject ZenDebugPanel;
    public GameObject PGINDebugPanel;
    public Text DebugText;
    public Toggle PGINTG;
    public Toggle ZenTG;
    public Toggle LogTG;
    private int checkCount = 0;
    private bool StartDebug = false;
    void Start()
    {
        // Click 6times to turn it on,
        // otherwise it will be permanently turned off.
        Application.targetFrameRate = 240;
        DontDestroyOnLoad(this.gameObject);
        SpButton.onClick.AddListener(() =>
        {
            if (!StartDebug)
            {
                if (Time.time >= 120)
                {
                    //SpButton.onClick = null;
                }

                if (checkCount == 0)
                {
                    Invoke("ClearCheckCount", 5);
                }
                else if (checkCount > 5)
                {
                    StartDebug = true;
                }
                checkCount++;
            }
            else
            {
                SwitchDebugUI();
            }
        });

        PGINTG.onValueChanged.AddListener((b) => { PGINDebugPanel.SetActive(b); });
        ZenTG.onValueChanged.AddListener((b) => { ZenDebugPanel.SetActive(b); });
    }

    void ClearCheckCount()
    {
        checkCount = 0;
    }

    [ContextMenu("OpenPGINPanel")]
    void OpenPPanel()
    {
        PGINTG.isOn = true;
        PGINDebugPanel.SetActive(true);
        ZenDebugPanel.SetActive(false);
    }

    [ContextMenu("Open Debug UI")]
    void SwitchDebugUI()
    {
        Root.SetActive(!Root.activeInHierarchy);
        SpButton.image.color = Root.activeInHierarchy ? Color.white : new Color(1, 1, 1, 0f);
    }
}

[Flags]
public enum ULog
{ All, Penguin, Zen }
public class CDebug
{
    public static ULog LookUserLog = ULog.All;
    public static Dictionary<ULog, StringBuilder> CacheMessage
    {
        get
        {
            if (cacheMessage == null)
            {
                cacheMessage = new Dictionary<ULog, StringBuilder>();
                cacheMessage.Add(ULog.All, new StringBuilder());
                cacheMessage.Add(ULog.Penguin, new StringBuilder());
                cacheMessage.Add(ULog.Zen, new StringBuilder());
            }
            return cacheMessage;
        }
    }
    static Dictionary<ULog, StringBuilder> cacheMessage = null;
    public static void SelectUserLog(ULog user)
    {
        LookUserLog = user;
        DebugManager.Instance.DebugText.text = cacheMessage[LookUserLog].ToString();
    }

    public static void AllClear()
    {
        foreach (var item in cacheMessage)
        {
            item.Value.Clear();
        }
    }
}

public static class CustomLog
{
    public static void Log(this ULog t, object obj = null, UnityEngine.Object selectObject = null)
    {
        if (obj != null)
        {
            string msg = $"[Log {t}] {obj.ToString()}";
            if (CDebug.CacheMessage != null)
            {
                if (CDebug.CacheMessage.ContainsKey(t))
                    CDebug.CacheMessage[t].AppendLine(msg);
                if (CDebug.CacheMessage.ContainsKey(ULog.All))
                    CDebug.CacheMessage[ULog.All].AppendLine(msg);

                if (DebugManager.Instance != null && DebugManager.Instance.DebugText != null)
                    DebugManager.Instance.DebugText.text = CDebug.CacheMessage[CDebug.LookUserLog].ToString();
            }
            Debug.Log(msg, selectObject);
        }
        else
            Debug.Log(obj.ToString());
    }
    public static void LogWarning(this ULog t, object obj = null, UnityEngine.Object selectObject = null)
    {
        string msg = $"[Warning {t}] {obj.ToString()}";
        CDebug.CacheMessage[t].AppendLine(msg);
        CDebug.CacheMessage[ULog.All].AppendLine(msg);
        if (DebugManager.Instance != null)
            DebugManager.Instance.DebugText.text = CDebug.CacheMessage[CDebug.LookUserLog].ToString();
        Debug.LogWarning(msg, selectObject);
    }
    public static void LogError(this ULog t, object obj = null, UnityEngine.Object selectObject = null)
    {
        string msg = $"[LogError {t}] {obj.ToString()}";
        CDebug.CacheMessage[t].AppendLine(msg);
        CDebug.CacheMessage[ULog.All].AppendLine(msg);
        if (DebugManager.Instance != null)
            DebugManager.Instance.DebugText.text = CDebug.CacheMessage[CDebug.LookUserLog].ToString();
        Debug.LogError(msg, selectObject);
    }
    public static void Clear(this ULog t)
    {
        CDebug.CacheMessage[t].Clear();
        t.RefreshText();
    }
    public static void RefreshText(this ULog t)
    {
        DebugManager.Instance.DebugText.text = CDebug.CacheMessage[CDebug.LookUserLog].ToString();
    }
}
