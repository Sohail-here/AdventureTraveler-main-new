using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using static StripeItem;
using static ActivityManager;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text;

public class TransactionManager : MonoSingleton<TransactionManager>
{

    public bool isDebug = false;
    public string debugAddress = "http://192.168.1.63:8000/";
    public string publicAddress = "https://api.i911adventure.com/";

    //Replace behaciour from Activity Manager
    public async Task<Dictionary<string, string>> CheckTransaction(string transaction_id)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/deal/enduser/{SingletonData.Instance.checkingTransaction}/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
        {
            var dresponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            return dresponse;
        }
        else
        {
            ULog.Penguin.LogError(response.Content.ToString());
            return null;
        }
    }

    public async Task<HttpResponseMessage> CreateTransactionLink(string strip_connet_accout_id, List<StripeAPIData> listSpData)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri((isDebug) ? debugAddress : publicAddress);
        client.DefaultRequestHeaders.Add("Authorization", "JWT " + UnDestroyData.token);

        var p = JsonConvert.SerializeObject(listSpData);
        var a = new JSONObject(p);
        var b = new JSONObject();
        b.AddField("list_items", a);

        HttpContent content = new StringContent(b.ToString(), Encoding.UTF8, "application/json");
        var res = await client.PostAsync($"/stripe_link/{strip_connet_accout_id}/", content);
        return res;

        // RestSharp does not work on android in certain calls. Need to replace for HttpClient
        /*
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/stripe_link/{strip_connet_accout_id}/", Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        Debug.Log("JWT " + UnDestroyData.token);

        request.AddJsonBody(new
        {
            list_items = listSpData
        });

        Debug.Log("Test items.");

        var response = await client.ExecuteAsync(request);
        Debug.Log("Test items1.");
        return response;
        */
    }

    public async Task<IRestResponse> GetReceipt(string transaction_id)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/histories/receipt/{transaction_id}/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        return response;
    }

    public async Task<IRestResponse> GetReceipts()
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/histories/user/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        return response;
    }

}
