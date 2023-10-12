using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;

public class ActivityViewListMgr : MonoBehaviour
{
    public Transform ItemRootTrans;
    public GameObject Prefab;
    public List<ActivityCompanyItem> Items = new List<ActivityCompanyItem>();
    public void UpdateData(List<VendorData> vendorlist)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Destroy(Items[i].gameObject);
        }
        Items.Clear();

        ULog.Penguin.Log("vendorlist : " + vendorlist.Count);
        foreach (var item in Items)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < vendorlist.Count; i++)
        {
            ActivityCompanyItem item = null;
            if (i >= Items.Count)
            {
                var obj = GameObject.Instantiate(Prefab, ItemRootTrans);
                item = obj.GetComponent<ActivityCompanyItem>();
                Items.Add(item);
            }
            else
            {
                item = Items[i];
            }

            if (item != null)
            {
                item.gameObject.SetActive(true);
                item.UpdateData(vendorlist[i]);
            }
        }
    }




    public void ClearAllMarker()
    {
        foreach (var item in Items)
        {
            item.ClearIcon();
        }
    }

    public void OnDataChanged()
    {
        var vendorData = ActivityDisplayStructure.Instance.VendorDataArray;
        UpdateData(vendorData);
    }
}
