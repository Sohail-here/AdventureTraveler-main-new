using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TestingOutputActivityDisplay : MonoBehaviour
{

    public void OutputActivityDisplay()
    {
        Debug.Log("TestingOutputActivityDisplay =================================");
        foreach (var a in ActivityDisplayStructure.Instance.VendorDataArray) {
            string txt = JsonConvert.SerializeObject(a);
            Debug.Log(txt);
        }
        Debug.Log("==============================================================");
    }

    public void OutputActivity()
    {
        Debug.Log("TestingOutputActivity =================================");
        foreach (var a in ActivitiesDataStructure.Instance.VendorDataArray)
        {
            string txt = JsonConvert.SerializeObject(a);
            Debug.Log(txt);
        }
        Debug.Log("==============================================================");
    }

}
