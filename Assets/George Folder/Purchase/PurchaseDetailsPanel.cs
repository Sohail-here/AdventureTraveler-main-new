using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseDetailsPanel : MonoBehaviour
{
    public TMP_Text PriceText;
    public TMP_Text VendorNameText;
    public TMP_Text ActivityEventNameText;
    public TMP_Text ContentText;

    void OnEnable()
    {
        PriceText.text = SingletonData.Instance.selectedActivity.pricing;
        VendorNameText.text = SingletonData.Instance.selectedVendor.company_name;
        ActivityEventNameText.text = SingletonData.Instance.selectedActivity.title;
        ContentText.text = SingletonData.Instance.selectedActivity.about;
    }
}
