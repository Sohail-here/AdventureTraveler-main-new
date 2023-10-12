using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMP_ScrollContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            TMP_InputField inputField = child.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                Debug.Log("Has inputField");
                child.gameObject.AddComponent<TMP_InputH>();
            }
            else
            {
                Debug.Log("Don't have inputField");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
