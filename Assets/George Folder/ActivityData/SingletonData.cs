using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;

public class SingletonData : MonoSingleton<SingletonData>
{

    public VendorData selectedVendor;
    public Active selectedActivity;
    public string checkingTransaction;

    public void SelectActivity(Active act)
    {
        Debug.Log("Activity Set.");
        selectedActivity = act;
    }

    public void SelectVendor(VendorData vend)
    {
        Debug.Log("Vendor Set.");
        selectedVendor = vend;
    }

    public void CheckTransaction(string transaction_id)
    {
        checkingTransaction = transaction_id;
    }

}
