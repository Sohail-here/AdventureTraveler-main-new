using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using static ActivityManager;

public class ActivitiesDataManager : MonoSingleton<ActivitiesDataManager>
{

    public bool isDebug = false;
    //public string debugAddress = "http://192.168.1.63:8000/";
    public string debugAddress = "https://staging.i911adventure.com/";
    public string publicAddress = "https://staging.i911adventure.com/";
    int id = 0;


    public void GetVendors(Vector2 pos)
    {
        GetVendors(pos.y, pos.x);
    }

    //Replace behaciour from Activity Manager
    public async void GetVendors(double lat, double lng)
    {
        Debug.Log("++++++++++++++++GetVendoers++++++Called");
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/actives/1/?lat={lat}&lng={lng}", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
        //Debug.Log("--------------GETVendors------- = " + request.ToString());
        var response = await client.ExecuteAsync(request);
        if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
        {
            VendorDatas vendors = JsonConvert.DeserializeObject<VendorDatas>(response.Content);
            ActivitiesDataStructure.Instance.SetVendorDatas(vendors);
            ULog.Penguin.Log($"lat={lat}&lng={lng} Vendors Count : {vendors.msg.Count}");
            if (vendors.current_page < vendors.total_page)
            {
                Debug.Log("More Pages... " + vendors.current_page + " " + vendors.total_page);
                int temp = id;
                AddVendors(lat, lng, ++vendors.current_page, temp);
            }
        }
        else
        {
            ULog.Penguin.LogError(response.Content.ToString());
            ULog.Penguin.LogError($"lat={lat}&lng={lng} Vendors is Null");
        }
    }

    public async void AddVendors(double lat, double lng, int page, int temp)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/actives/{page}/?lat={lat}&lng={lng}", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
        {
            VendorDatas vendors = JsonConvert.DeserializeObject<VendorDatas>(response.Content);
            ActivitiesDataStructure.Instance.AddVendorDatas(vendors);
            ULog.Penguin.Log($"lat={lat}&lng={lng} Vendors Count : {vendors.msg.Count}");
            if (vendors.current_page < vendors.total_page)
            {
                Debug.Log("More Pages... " + vendors.current_page + " " + vendors.total_page);
                AddVendors(lat, lng, ++page, temp);
            }
        }
        else
        {
            ULog.Penguin.LogError(response.Content.ToString());
            ULog.Penguin.LogError($"lat={lat}&lng={lng} Vendors is Null");
        }
    }

    public async Task<IRestResponse> GetActivity(string activity_id)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/active/{activity_id}/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        return response;
    }

    public async Task<IRestResponse> GetVendor(string vendor_email)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/vendor/{vendor_email}/", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response = await client.ExecuteAsync(request);
        return response;
    }

}
