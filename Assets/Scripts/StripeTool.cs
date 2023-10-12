
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class StripeTool
{
    public static List<StripeData.Price.Datum> GetPrices()
    {
        List<StripeData.Price> prices = new List<StripeData.Price>();
        var json = SendPrice();
        StripeData.Price.Root myDeserializedClass = JsonConvert.DeserializeObject<StripeData.Price.Root>(json);

        return myDeserializedClass.data;
    }

    public static List<StripeData.Product> GetProducts()
    {
        List<StripeData.Product> products = new List<StripeData.Product>();
        var json = SendProducts();

        var obj = (JObject)JsonConvert.DeserializeObject(json);
        foreach (var item in obj["data"])
        {
            var imagePaths = item["images"].ToString().Split('"');
            var imgPath = imagePaths.Length > 1 ? imagePaths[1] : "";
            var d = new StripeData.Product
            {
                id = item["id"].ToString(),
                name = item["name"].ToString(),
                active = bool.Parse(item["active"].ToString()),
                description = item["description"].ToString(),
                updated = item["updated"].ToString(),
                plink = item["metadata"]["plink"] != null ? item["metadata"]["plink"].ToString() : "",
                images = imgPath,
            };
            products.Add(d);
        }

        return products;
    }

    public static StripeData.UserData.Root GetUserData()
    {
        var json = SendUserData();
        StripeData.UserData.Root myDeserializedClass = JsonConvert.DeserializeObject<StripeData.UserData.Root>(json);

        return myDeserializedClass;
    }

    static string SendProducts()
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest("v1/products", Method.GET);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }

    }

    static string SendPrice()
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator("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:", "");
            var request = new RestRequest("v1/prices", Method.GET);

            //var request = new RestRequest("v1/customers", Method.POST);
            //var request = new RestRequest("v1/invoiceitems", Method.POST);
            //var request = new RestRequest("v1/invoices" , Method.POST);
            //var request = new RestRequest("v1/invoices/in_1KJYPKB8H9uBl2teWSSKx71q/send", Method.POST);

            //request.AddParameter("customer", "pginlovemimi");
            //request.AddParameter("invoice", "in_1KJOURB8H9uBl2teCVbAOur7");
            //request.AddParameter("price", "price_1IVgDSB8H9uBl2te6EHKMai8");
            //request.AddParameter("price", "price_1IRjrFB8H9uBl2teBQxsN3Vm");
            //var arr0 = new JsonParameter("days_until_due", "30");
            //request.AddParameter(arr0.Name, arr0.Value);
            //request.AddParameter("collection_method", "send_invoice");
            //request.AddParameter("include", new List<string> { "available_payment_method_types" } , ParameterType.QueryStringWithoutEncode);
            //var arr = new JsonParameter("expand", new List<string>() { "default_tax_rates", "default_payment_method", "default_source" });
            //var arr2 = JsonConvert.SerializeObject(new List<string>() { "default_tax_rates", "default_payment_method", "default_source" });
            //List<string> strs = new List<string>() { "default_tax_rates" , ",default_payment_method" , ",default_source" };
            //request.AddParameter("expand", arr2);
            //request.AddParameter("email", "cutesamllpgin@gmail.com");

            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }

    }

    static string SendUserData()
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator("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:", "");
            var request = new RestRequest("v1/customers/" + StripeData.UserToken, Method.GET);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"{response.StatusCode} response:{response.Content}");
                return response.Content;
            }
            else
            {
                request = new RestRequest($"v1/sources", Method.POST);
                request.AddParameter("type", "ach_credit_transfer");
                request.AddParameter("currency", "usd");
                request.AddParameter("owner[email]", StripeData.UserMail);
                response = client.Execute(request);
                if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
                {
                    StripeData.SourceData.Root source = JsonConvert.DeserializeObject<StripeData.SourceData.Root>(response.Content);

                    request = new RestRequest("v1/customers", Method.POST);
                    request.AddParameter("id", StripeData.UserToken);
                    request.AddParameter("name", StripeData.UserName);
                    request.AddParameter("email", StripeData.UserMail);
                    request.AddParameter("source", source.id);
                    response = client.Execute(request);
                    if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        Debug.Log($"{response.StatusCode} response:{response.Content}");
                        return response.Content;
                    }
                    else
                    {
                        Debug.LogWarning($"{response.StatusCode} response:{response.Content}");
                        return default;
                    }
                }
                else
                {
                    Debug.Log($"{response.StatusCode} response:{response.Content}");
                    return default;
                }

            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }
    }



    static public string SendInvoiceItems(string priceID)
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest("v1/invoiceitems", Method.POST);
            request.AddParameter("customer", StripeData.UserToken);
            request.AddParameter("price", priceID);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }
    }

    static public string SendInvoice()
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest("v1/invoices", Method.POST);
            request.AddParameter("customer", StripeData.UserToken);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }


    }


    static public string SendInvoice(string inID)
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest($"v1/invoices/{inID}", Method.POST);
            request.AddParameter("collection_method", "send_invoice");
            request.AddParameter("days_until_due", "30");
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }


    }

    static public string SendInvoiceInfoSend(string inID)
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest($"v1/invoices/{inID}/send", Method.POST);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }
    }

    /// <summary>
    /// 取得之前已開啟的購買項目避免重複開啟
    /// </summary>
    /// <returns></returns>
    static public string SendInvoiceGetOpen()
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest("v1/invoices", Method.GET);
            request.AddParameter("customer", StripeData.UserToken);
            request.AddParameter("status", "open");
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }


    }

    static public string SendInvoiceGet(string inID)
    {

        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest("v1/invoices/" + inID, Method.GET);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }


    }

    static public string SendSubscriptions(string priceID)
    {
        try
        {
            var client = new RestClient("https://api.stripe.com/");
            client.Authenticator = new HttpBasicAuthenticator(StripeData.SKey, "");
            var request = new RestRequest($"v1/subscriptions", Method.POST);
            //request.AddParameter("days_until_due", "30");
            request.AddParameter("customer", StripeData.UserToken);
            request.AddParameter("items[0][price]", priceID);
            var response = client.Execute(request);
            if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
            {
                Debug.Log($"response:{response.Content}");
                return response.Content;
            }
            else
            {
                Debug.Log($"response:{response.Content}");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default;
        }
    }
}
