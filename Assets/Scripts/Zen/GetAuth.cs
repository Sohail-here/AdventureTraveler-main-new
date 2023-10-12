using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAuth : MonoBehaviour
{
    #region GPS

    public GameObject warnPanel;
    public Text warnText;

    public void GetGpsAuth()
    {
        if (!Input.location.isEnabledByUser)
        {
            warnPanel.SetActive(true);
            warnText.text = "Please open GPS";
        }
        else
        {

        }
    }

    #endregion
}