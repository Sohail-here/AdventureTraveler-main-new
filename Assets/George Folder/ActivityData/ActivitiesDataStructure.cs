using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Events;
using System;

// Behavious specific to data structure of Activity Data
public class ActivitiesDataStructure : MonoSingleton<ActivitiesDataStructure>
{

    public List<VendorData> VendorDataArray;
    public UnityEvent OnDataChanged;


    public void Start()
    {
        VendorDataArray = new List<VendorData>();
    }

    public void AddVendorDatas(VendorDatas data)
    {
        AddVendorDatas(data.msg);
    }

    public void AddVendorDatas(List<VendorData> dataArr)
    {
        VendorDataArray.AddRange(dataArr);
        OnDataChanged.Invoke();
    }

    public void SetVendorDatas(VendorDatas data)
    {
        SetVendorDatas(data.msg);
    }

    public void SetVendorDatas(List<VendorData> dataArr)
    {
        VendorDataArray = DeepClone(dataArr);
        OnDataChanged.Invoke();
    }

    public async Task<List<VendorData>> GetVendors(string[] categoryFilter = null)
    {
        List<VendorData> vendorReturn = new List<VendorData>();
        if (categoryFilter == null)
        {
            return VendorDataArray;
        }
        HashSet<string> filterHash = new HashSet<string>(categoryFilter);
        
        foreach (var v in VendorDataArray)
        {
            await Task.Delay(10);
            VendorData vendor = DeepClone(v);
            vendor.actives = new List<Active>();
            vendor.resource = new List<Resource>();

            List<int> addActivity = new List<int>();

            for (int i = 0; i < v.actives.Count; i++)
            {
                var activity = v.actives[i];
                if (filterHash.Contains(activity.activity_type))
                {
                    addActivity.Add(i);
                }
            }

            foreach (var index in addActivity)
            {
                await Task.Delay(10);
                vendor.actives.Add(v.actives[index]);
                //vendor.resource.Add(v.resource[index]);
            }

            if (addActivity.Count > 0)
            {
                vendorReturn.Add(vendor);
            }
        }
        
        return vendorReturn;
    }

    public void Clear()
    {
        VendorDataArray.Clear();
    }

    public T DeepClone<T>(T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

}
