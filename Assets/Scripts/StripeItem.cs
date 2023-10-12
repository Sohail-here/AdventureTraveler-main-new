using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RestSharp;
using System.Net;

public class StripeItem : MonoBehaviour
{
    public StripeData.Product ProductData;
    public StripeData.Price.Datum PriceData;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI PriceText;
    public RawImage Img;

    private ActivityManager.Active activeData;
    public class StripeAPIData
    {
        public string price;    //"price" : "script_price_id"
        public string quantity; //"quantity" : 1},
    }
    StripeAPIData sp_Data;

    public void InitData(ActivityManager.Active active)
    {
        if (active != null)
        {
            activeData = active;
            TitleText.text = $"{active.title}";
            InfoText.text = $"{active.activity_type}";
            PriceText.text = $"${active.pricing}";

            sp_Data = new StripeAPIData { price = active.pricing_stripe_id?.ToString(), quantity = "1" };
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("Stripe Click");

        SingletonData.Instance.SelectActivity(activeData);

        // Temporarily canceled for DEMO
        /*
        if (ActivityManager.Instance.CompanyDetail.VendorData == null)
        {
            Debug.LogError("VendorData Is Null!");
            return;
        }

        var strip_connet_accout_id = ActivityManager.Instance.CompanyDetail.VendorData.stripe_connect_account_id?.ToString();

        if (string.IsNullOrEmpty(strip_connet_accout_id))
        {
            Debug.LogError("strip_connet_accout_id is Null!!");
            return;
        }

        var client = new RestClient("https://api.i911adventure.com/");
        var request = new RestRequest($"/stripe_link/{strip_connet_accout_id}/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
        List<StripeAPIData> listSpData = new List<StripeAPIData>();
        request.AddJsonBody(listSpData);

        var response = client.Execute(request);
        if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log($"Count : {response.Content}");
        }
        else
        {

        }
        */

        // For Demo
        UIManager.Instance.FindParentObjectByName(gameObject, "PurchaseDetails").SetActive(true);
    }

    #region Old Code Not Use
    //public void InitData(StripeData.Product productData, StripeData.Price.Datum priceData)
    //{
    //    if (productData != null && priceData != null)
    //    {
    //        ProductData = productData;
    //        PriceData = priceData;
    //        TitleText.text = $"{productData.name}";
    //        InfoText.text = $"{productData.description}";
    //        PriceText.text = $"{(priceData.unit_amount * 0.01f).ToString("C", new CultureInfo("en-US"))}";
    //        //StartCoroutine(LoadImg(productData.images));
    //    }
    //}

    //IEnumerator LoadImg(string url)
    //{
    //    UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.ConnectionError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    //        Img.texture = myTexture;
    //    }
    //}

    //IEnumerator SendProductData()
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        if (ProductData != null && PriceData != null)
    //        {
    //            Debug.LogWarning($"{ProductData.id} | {PriceData.product} {PriceData.id} | {PriceData.type}");
    //            string json = "";
    //    string url = "";
    //    StripeData.InvoiceInfo.Root info = null;
    //    json = StripeTool.SendInvoiceGetOpen();

    //            if (string.IsNullOrEmpty(json) == false)
    //            {
    //                var infos = JsonConvert.DeserializeObject<StripeData.InvoicesDatas.Root>(json);
    //    var i = infos.data.FirstOrDefault(i => i.lines.data.FirstOrDefault(li => li.price.id == PriceData.id) != null);
    //    url = i == null ? "" : i.hosted_invoice_url.ToString();
    //            }

    //if (string.IsNullOrEmpty(url))
    //{
    //    if (PriceData.type == "recurring")
    //    {
    //        json = StripeTool.SendInvoice();
    //        json = StripeTool.SendSubscriptions(PriceData.id);
    //        StripeData.SubscriptionsData.Root subitem = JsonConvert.DeserializeObject<StripeData.SubscriptionsData.Root>(json);
    //        json = StripeTool.SendInvoiceGet(subitem.latest_invoice);
    //        info = JsonConvert.DeserializeObject<StripeData.InvoiceInfo.Root>(json);
    //    }
    //    else
    //    {
    //        json = StripeTool.SendInvoiceItems(PriceData.id);
    //        StripeData.InvoiceItem.Root invoiceItemData = JsonConvert.DeserializeObject<StripeData.InvoiceItem.Root>(json);
    //        json = StripeTool.SendInvoice();
    //        info = JsonConvert.DeserializeObject<StripeData.InvoiceInfo.Root>(json);
    //        StripeTool.SendInvoice(info.id);
    //        json = StripeTool.SendInvoiceInfoSend(info.id);
    //        info = JsonConvert.DeserializeObject<StripeData.InvoiceInfo.Root>(json);
    //    }

    //    url = info.hosted_invoice_url.ToString();
    //}
    //yield return new WaitForSeconds(0.5f);
    //if (string.IsNullOrEmpty(url) == false)
    //{
    //    StripeManager.Instance.WaitShowMsg(ProductData.id,
    //        $"Thank you for choosing the item : {ProductData.name}, System needs some time to confirm information\nIf you have any question with purchase result, Welcome to mail : ABCDE@.cc.com\nOr Call this phone 0987654321\n\n\nAmount : {(PriceData.unit_amount * 0.01f).ToString("C", new CultureInfo("en-US"))}");
    //    MessageBox.Instance.SysCloseMsg();
    //    yield return new WaitForSeconds(1f);
    //    Application.OpenURL(url);
    //}
    //else
    //{
    //    MessageBox.Instance.ShowText("Unable to get the link of purchase, please contact the engineer to deal with");
    //}
    //        }
    //    }
    #endregion
}