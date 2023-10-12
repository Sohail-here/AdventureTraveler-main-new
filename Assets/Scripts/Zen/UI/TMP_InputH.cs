using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TMP_InputH : MonoBehaviour, IPointerClickHandler
{
    private GameObject canvas;
    private GameObject scrollViewGO;

    public static GameObject lastSelectedInput;

    // Start is called before the first frame update
    void Start()
    {
        GameObject contentGO = transform.parent.gameObject;
        GameObject viewportGO = contentGO.transform.parent.gameObject;
        scrollViewGO = viewportGO.transform.parent.gameObject;
        ScrollRect scrollRect = scrollViewGO.GetComponent<ScrollRect>();
        canvas = scrollRect.transform.parent.gameObject;

        TMP_InputField inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(
            delegate
            {
                TMP_KeyboardSize ks = scrollViewGO.GetComponent<TMP_KeyboardSize>();
                ks.CloseKeyboard();
            });  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        TMP_Debug.Instance.DebugPrint("OnPointerClick");

        TMP_KeyboardSize ks = scrollViewGO.GetComponent<TMP_KeyboardSize>();
        if (!ks.IsKeyboardOpened())
        {
            Debug.Log("!ks.IsKeyboardOpened()");
            TMP_Debug.Instance.DebugPrint("!ks.IsKeyboardOpened()");

            ks.OpenKeyboard();

            lastSelectedInput = gameObject;
        }
        else
        {
            Debug.Log("Keyboard is already opened");
            TMP_Debug.Instance.DebugPrint("Keyboard is already opened");
        }
    }
}