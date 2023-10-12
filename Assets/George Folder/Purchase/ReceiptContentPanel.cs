using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class ReceiptContentPanel : MonoBehaviour
{
    public TMP_Text title_text;
    public TMP_Text price_text;
    public TMP_Text item_text;
    public TMP_Text date_text;
    public JSONObject receiptData;

    public void OnEnable()
    {
        
    }

    public async void Populate(JSONObject receiptData)
    {
        this.receiptData = receiptData;

        string item_text_str;
        receiptData.GetField(out item_text_str, "active_title", "N/A");
        item_text.text = item_text_str;

        string date_text_str;
        receiptData.GetField(out date_text_str, "purchase_datetime", "N/A");
        date_text.text = date_text_str;

        var escapedJsonString = Regex.Unescape(receiptData["receipt_json"].str);
        JSONObject receiptjson = new JSONObject(escapedJsonString);
        Debug.Log("receiptjson : " + receiptjson);
        if (receiptjson != null)
        {
            float price_text_float;
            receiptjson.GetField(out price_text_float, "total_amt", -0.1f);
            price_text.text = (price_text_float != -0.1f) ? price_text_float.ToString("C") : "";
        }
        
        var vendor_result = await ActivitiesDataManager.Instance.GetVendor(receiptData["vendor_email"].str);
        if (!vendor_result.IsSuccessful)
        {
            return;
        }

        var vendor_json = new JSONObject(vendor_result.Content);
        if (vendor_json == null)
        {
            return;
        }
        string title_text_str;
        vendor_json.GetField(out title_text_str, "company_name", "N/A");
        title_text.text = title_text_str;

    }

    public void OnClick()
    {
        SingletonData.Instance.CheckTransaction(receiptData["id"].str);
        UIManager.Instance.FindParentObjectByName(gameObject, "pnl_PurchaseDetail").SetActive(true);
    }
}
