using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class ActivityManager : MonoSingleton<ActivityManager>
{
    public List<VendorData> VendorList;
    public ActivityViewListMgr ListViewMgr;
    public ActivityCompanyItem MiniCompanyItem;
    public ActivityCompanyDetail CompanyDetail;
    public ActivityCompanyItem SelectItem;

    public bool isDebug = false;
    public string debugAddress = "http://192.168.1.63:8000/";
    public string publicAddress = "https://api.i911adventure.com/";
    public bool forceDebugPoints = false;

    void Start()
    {
        OnlineMaps.instance.OnChangePosition += () =>
        {
            SelectActivityMarker(null);
            if (MiniCompanyItem != null &&
                MiniCompanyItem.gameObject != null &&
                MiniCompanyItem.gameObject.activeInHierarchy)
            {
                MiniCompanyItem.gameObject.SetActive(false);
            }
        };

        Refresh();
    }

    private void OnEnable()
    {
        RecordManager.Instance?.CancelRecord();
    }

    public async void GetVendon(double lat, double lng)
    {
        var client = new RestClient((isDebug) ? debugAddress : publicAddress);
        var request = new RestRequest($"/actives/1/?lat={lat}&lng={lng}", Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
        Debug.Log("--------------GETVendon------- = " + request);
        var response = await client.ExecuteAsync(request);
        if (response.Content != null && response.StatusCode == HttpStatusCode.OK)
        {
            VendorDatas vendors = JsonConvert.DeserializeObject<VendorDatas>(response.Content);
            VendorList = vendors.msg;
            if (ListViewMgr != null)
            {
                ListViewMgr.UpdateData(VendorList);
            }
            ULog.Penguin.Log($"lat={lat}&lng={lng} Vendors Count : {vendors.msg.Count}");
        }
        else
        {
            ULog.Penguin.LogError(response.Content.ToString());
            ULog.Penguin.LogError($"lat={lat}&lng={lng} Vendors is Null");
            if (forceDebugPoints)
            {
                if (ListViewMgr != null)
                {
                    var a = TestingActivitiesData.GetDebugVendorDatas();
                    VendorList = a.msg;
                    ListViewMgr.UpdateData(VendorList);
                }
            }
        }
    }

    bool refreshStarted = false;
    public async void Refresh()
    {
        if (ActivityManager.Instance != null && ActivityManager.Instance.gameObject.activeInHierarchy)
        {
            if (refreshStarted)
            {
                ULog.Penguin.Log("Denied double request.");
                return;
            }
            refreshStarted = true;
            ULog.Penguin.Log("Started Request");
            await Task.Delay(1000);
            refreshStarted = false;
            ListViewMgr.ClearAllMarker();
            if (ActivitiesDataManager.Instance != null && OnlineMaps.instance != null)
                ActivitiesDataManager.Instance.GetVendors(OnlineMaps.instance.position.y, OnlineMaps.instance.position.x);
        }
    }

    public void ShowVendonDetail(VendorData vendor)
    {
        CompanyDetail.ShowCompanyDetail(vendor);
    }

    [System.Serializable]
    public class Active
    {
        //ttxtActivity_Id
        public string id { get; set; }
        //ttxtActivityTitle
        public string title { get; set; }
        //ttxtPrice
        public string pricing { get; set; }
        //ttxtActivityDescription
        public string about { get; set; }
        public string status { get; set; }
        public string activity_type { get; set; }
        public object pricing_stripe_id { get; set; }
        //ttxtPriceId
        public object stripe_id { get; set; }
    }

    [System.Serializable]
    public class Resource
    {
        public string resource_url { get; set; }
        public string resource_id { get; set; }
    }

    [System.Serializable]
    public class VendorData
    {
        //ttxtVendorId;
        public string id { get; set; }
        public string email { get; set; }
        public int created_at { get; set; }
        public string company_email { get; set; }
        //ttxtVendorName;
        public string company_name { get; set; }
        public string company_support_number { get; set; }
        public object stripe_connect_account_id { get; set; }
        public bool is_advertisement_sponsorship { get; set; }
        public bool is_approve { get; set; }
        public string vendor_category { get; set; }
        public object last_name { get; set; }
        public object first_name { get; set; }
        public string status { get; set; }
        public string about { get; set; }
        public string fine_print { get; set; }
        public string company_category { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string address { get; set; }
        public List<Active> actives { get; set; }
        public List<Resource> resource { get; set; }
    }

    [System.Serializable]
    public class VendorDatas
    {
        public List<VendorData> msg { get; set; }
        public int current_page { get; set; }
        public int total_page { get; set; }
    }

    public void SelectActivityMarker(ActivityCompanyItem selectItem)
    {
        if (SelectItem != null)
            SelectItem.ResetIcon();

        SelectItem = selectItem;
    }
}
