using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMP_KeyboardSize : MonoBehaviour
{
    [SerializeField] private RectTransform bufferImage;
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private ScrollRect scrollRect;

    private float height = -1;

    public void OpenKeyboard()
    {
        TouchScreenKeyboard.Open("");
        StartCoroutine(GetKeyboardHeight());
    }

    public void CloseKeyboard()
    {
        bufferImage.sizeDelta = Vector2.zero;     
    }

    public IEnumerator GetKeyboardHeight()
    {
        yield return new WaitForSeconds(0.5f);

#if UNITY_EDITOR

#elif UNITY_IOS

        TMP_Debug.Instance.DebugPrint("IOS_START");
        //On iOS we can use the native TouchScreenKeyboard.area.height
        height = TouchScreenKeyboard.area.height;
        TMP_Debug.Instance.DebugPrint("IOS_STOP-- " + height);

#elif UNITY_ANDROID

        Debug.Log("UNITY_ANDROID Start");
        TMP_Debug.Instance.DebugPrint("UNITY_ANDROID START");
        
        //On Android TouchScreenKeyboard.area.height returns 0, so we get it from an AndroidJavaObject instead.
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                height = Screen.height - Rct.Call<int>("height");
            }
        }

        Debug.Log("UNITY_ANDROID Stop");
        TMP_Debug.Instance.DebugPrint("UNITY_ANDROID STOP");
#endif

        bufferImage.sizeDelta = new Vector2(1, height);

        StartCoroutine(CalculateNormalizedPosition());
    }

    IEnumerator CalculateNormalizedPosition()
    {
        yield return new WaitForEndOfFrame();
        var newContentHeight = contentParent.sizeDelta.y;
        Debug.Log("contentParent.sizeDelta.y-- " + contentParent.sizeDelta.y);
        TMP_Debug.Instance.DebugPrint("sizeDelta.y-- " + contentParent.sizeDelta.y);
        var selectedInputHeight = TMP_InputH.lastSelectedInput.transform.localPosition.y;
        Debug.Log("InputH.lastSelectedInput.transform.localPosition.y-- " + TMP_InputH.lastSelectedInput.transform.localPosition.y);
        TMP_Debug.Instance.DebugPrint("localPosition.y-- " + TMP_InputH.lastSelectedInput.transform.localPosition.y);
        var selectedInputfieldHeightNormalized = 1 - selectedInputHeight / -newContentHeight;
        Debug.Log("1 - selectedInputHeight / -newContentHeight-- " + (1 - selectedInputHeight / -newContentHeight));
        TMP_Debug.Instance.DebugPrint("selectedInputfieldHeightNormalized-- " + (1 - selectedInputHeight / -newContentHeight));
        scrollRect.verticalNormalizedPosition = selectedInputfieldHeightNormalized;
        Debug.Log("selectedInputfieldHeightNormalized-- " + selectedInputfieldHeightNormalized);
        TMP_Debug.Instance.DebugPrint("selectedInputfieldHeightNormalized-- " + selectedInputfieldHeightNormalized);
    }

    public bool IsKeyboardOpened()
    {
        Debug.Log("TouchScreenKeyboard.visible-- " + TouchScreenKeyboard.visible);
        TMP_Debug.Instance.DebugPrint("TouchScreenKeyboard.visible-- " + TouchScreenKeyboard.visible);
        return TouchScreenKeyboard.visible;
    }
}