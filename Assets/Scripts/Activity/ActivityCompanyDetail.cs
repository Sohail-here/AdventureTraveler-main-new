using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ActivityManager;

public class ActivityCompanyDetail : MonoBehaviour
{
    public ActivityCompanyItem DetailItem;
    public TextMeshProUGUI FinePrintsContextText;
    public TextMeshProUGUI AboutCompanyContextText;
    public TextMeshProUGUI CompanyAddressText;
    public VendorData VendorData = null;
    public void ShowCompanyDetail(VendorData vendor)
    {
        VendorData = vendor;
        this.gameObject.SetActive(true);
        //Update Vonder Detail Page
        DetailItem.UpdateData(vendor);
        //Update Vonder ActivityStripe List
        StripeManager.Instance.UpdateStripeData(vendor.actives);
        AboutCompanyContextText.text = vendor.about;
    }

    public void CloseCompanyDetail()
    {
        this.gameObject.SetActive(false);
    }
}