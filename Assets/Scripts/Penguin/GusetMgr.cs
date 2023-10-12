using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GusetMgr : MonoBehaviour
{
    public Toggle ActivityToggle;
    // Start is called before the first frame update
    void Start()
    {
        if (UnDestroyData.IsGusetLogin)
        {
            ActivityToggle.isOn = true;
        }
    }
}
