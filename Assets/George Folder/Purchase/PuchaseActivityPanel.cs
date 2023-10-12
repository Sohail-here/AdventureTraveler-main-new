using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static StripeItem;
using Newtonsoft.Json;

public class PuchaseActivityPanel : MonoBehaviour
{
    public async void OnPlaceOrder()
    {
        // Temporarily canceled for DEMO
        if (ActivityManager.Instance.CompanyDetail.VendorData == null)
        {
            Debug.LogError("VendorData Is Null!");
            return;
        }

        //var strip_connet_accout_id = ActivityManager.Instance.CompanyDetail.VendorData.stripe_connect_account_id?.ToString();
        Debug.Log("Stripe_connect_account_id=======" + SingletonData.Instance.selectedVendor.stripe_connect_account_id);
        var strip_connet_accout_id = SingletonData.Instance.selectedVendor.stripe_connect_account_id.ToString();


        if (string.IsNullOrEmpty(strip_connet_accout_id))
        {
            Debug.LogError("strip_connet_accout_id is Null!!");
            return;
        }

        List<StripeAPIData> listSpData = new List<StripeAPIData>()
        {
            new StripeAPIData() {price = SingletonData.Instance.selectedActivity.pricing_stripe_id.ToString(), quantity = "1" }
        };

        var response = await TransactionManager.Instance.CreateTransactionLink(strip_connet_accout_id, listSpData);

        var stringResponse = await response.Content.ReadAsStringAsync();
        Debug.Log(stringResponse);
        if (stringResponse != null && response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log($"Count : {stringResponse}");
            var json = new JSONObject(stringResponse);
            Debug.Log($"JSON: {json}");
            SingletonData.Instance.CheckTransaction(json["transaction_id"].str);
            Application.OpenURL(json["msg"].str);
            UIManager.Instance.FindParentObjectByName(gameObject, "WaitingPurchase").SetActive(true);
        }
        else
        {
            Debug.LogError("Error happened.");
        }
    }
}
