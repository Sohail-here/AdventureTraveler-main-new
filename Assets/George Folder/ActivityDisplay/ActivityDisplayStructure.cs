using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Events;

public class ActivityDisplayStructure : MonoSingleton<ActivityDisplayStructure>
{

    public List<VendorData> VendorDataArray;
    public UnityEvent OnDataChanged;

    public void SetVendorDatas(VendorDatas data)
    {
        SetVendorDatas(data.msg);
    }

    public void SetVendorDatas(List<VendorData> dataArr)
    {
        VendorDataArray = DeepClone(dataArr);
        OnDataChanged.Invoke();
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
