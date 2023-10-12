using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityDisplayManager : MonoSingleton<ActivityDisplayManager>
{
    
    public async void DisplayVendorsByFilter(string[] categoryFilter = null)
    {
        var d = await ActivitiesDataStructure.Instance.GetVendors(categoryFilter);
        ActivityDisplayStructure.Instance.SetVendorDatas(d);
    }

}