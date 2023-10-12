using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoSingleton<MessageBox>
{
    public GameObject MsgBox;
    public Text MsgText;
    public Image LoadingImage;
    protected bool IsLock = false;

    private void Update()
    {
        if (LoadingImage.IsActive() && this.gameObject.activeInHierarchy)
        {
            LoadingImage.rectTransform.Rotate(Vector3.forward * Time.realtimeSinceStartup * 0.05f, Space.Self);
        }
    }
    public void CloseMsg()
    {
        if (IsLock == false)
        {
            MsgBox.SetActive(false);
        }
    }

    public void SysCloseMsg()
    {
        if (MsgBox.activeInHierarchy)
            MsgBox.SetActive(false);
    }

    public void ShowText(string msg, bool showLoading = false, bool isLock = false)
    {
        LoadingImage.enabled = showLoading;
        MsgText.text = msg;
        IsLock = isLock;
        if (MsgBox.activeInHierarchy == false)
            MsgBox.SetActive(true);
    }


}
