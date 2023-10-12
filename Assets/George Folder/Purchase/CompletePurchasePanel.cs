using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class CompletePurchasePanel : MonoBehaviour
{

    public TMP_Text value_text;
    public TMP_Text company_name_text;
    public TMP_Text kind_of_ticket_text;
    public TMP_Text date_text;
    // Maybe show the transaction ID
    public TMP_Text activity_content_text;
    public TMP_Text sponsored_listing_text;
    public TMP_Text phone_number_text;
    public TMP_Text address_text;
    public TMP_Text about_text;
    public TMP_Text fine_text;


    private async void OnEnable()
    {
        var transaction_id = SingletonData.Instance.checkingTransaction;

        var res = await TransactionManager.Instance.GetReceipt(transaction_id);
        Debug.Log(res.Content);
        JSONObject json = new JSONObject(res.Content);
        Debug.Log(json);
        JSONObject msgjson = json["msg"][0];
        var escapedJsonString = Regex.Unescape(msgjson["receipt_json"].str);
        print(escapedJsonString);
        JSONObject receiptjson = new JSONObject(escapedJsonString);
        Debug.Log(receiptjson);
        value_text.text = $"{receiptjson["total_amt"].f.ToString("C")}";
        kind_of_ticket_text.text = msgjson["active_title"].str;
        date_text.text = msgjson["purchase_datetime"].str;

        var activity_result = await ActivitiesDataManager.Instance.GetActivity(msgjson["active_id"].str);
        Debug.Log(activity_result.Content);
        var activity_json = new JSONObject(activity_result.Content);
        Debug.Log(activity_json);
        activity_content_text.text = activity_json["about"].str;
        sponsored_listing_text.text = activity_json["is_sponsor"].b ? "Is Sponsored" : "Is Not Sponsored";

        var vendor_result = await ActivitiesDataManager.Instance.GetVendor(msgjson["vendor_email"].str);
        Debug.Log(vendor_result.Content);
        var vendor_json = new JSONObject(vendor_result.Content);
        Debug.Log(vendor_json);
        phone_number_text.text = vendor_json["company_support_number"].str;
        address_text.text = vendor_json["address"].str;
        about_text.text = vendor_json["about"].str;
        fine_text.text = vendor_json["fine_print"].str;
        company_name_text.text = vendor_json["company_name"].str;

    }
}
