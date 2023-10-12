using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;
using Newtonsoft.Json;

public class TestingActivitiesData : MonoBehaviour
{

    public ActivitiesDataStructure str;
    public bool test = false;
    public bool testSetDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private async void Update()
    {
        if (test)
        {
            test = false;
            var v = GetDebugVendorDatas();
            str.SetVendorDatas(v);
            var vendors = await str.GetVendors(new string[] { "B" });
            foreach (var a in vendors)
            {
                string txt = JsonConvert.SerializeObject(a);
                Debug.Log(txt);
            }
        }

        if (testSetDebug)
        {
            testSetDebug = false;
            var v = GetDebugVendorDatas();
            str.SetVendorDatas(v);
        }
    }



    public static VendorDatas GetDebugVendorDatas()
    {
        string s = "1";
        string email = "abc@abc.com";
        string company = "company";
        LoremIpsumBuilder buil = new LoremIpsumBuilder();
        int n = 1;
        List<Active> a = new List<Active>();
        List<Active> b = new List<Active>();
        List<Active> c = new List<Active>();
        a.Add(new Active()
        {
            id = s,
            title = buil.GetWords(10),
            pricing = s,
            about = buil.GetParagraphs(1),
            status = s,
            activity_type = "A",
        });

        a.Add(new Active()
        {
            id = s,
            title = buil.GetWords(10),
            pricing = s,
            about = buil.GetParagraphs(1),
            status = s,
            activity_type = "B",
        });

        b.Add(new Active()
        {
            id = s,
            title = buil.GetWords(10),
            pricing = s,
            about = buil.GetParagraphs(1),
            status = s,
            activity_type = "A",
        });

        b.Add(new Active()
        {
            id = s,
            title = buil.GetWords(10),
            pricing = s,
            about = buil.GetParagraphs(1),
            status = s,
            activity_type = "C",
        });

        c.Add(new Active()
        {
            id = s,
            title = buil.GetWords(10),
            pricing = s,
            about = buil.GetParagraphs(1),
            status = s,
            activity_type = "D",
        });

        List<Resource> r = new List<Resource>();
        r.Add(new Resource()
        {
            resource_url = "https://www.k1speed.com/wp-content/uploads/2021/07/christmas-holiday-party.jpeg",
            resource_id = s
        });

        r.Add(new Resource()
        {
            resource_url = "https://www.k1speed.com/wp-content/uploads/2021/07/christmas-holiday-party.jpeg",
            resource_id = s
        });

        List<VendorData> v = new List<VendorData>();
        v.Add(new VendorData()
        {
            id = s,
            email = email,
            created_at = n,
            company_email = email,
            company_name = company,
            company_support_number = s,
            is_advertisement_sponsorship = true,
            is_approve = true,
            vendor_category = s,
            status = s,
            about = buil.GetParagraphs(1),
            fine_print = s,
            company_category = s,
            lat = 34.0779285,
            lng = -117.6852931,
            address = buil.GetWords(10),
            actives = a,
            resource = r
        });

        v.Add(new VendorData()
        {
            id = s,
            email = s,
            created_at = n,
            company_email = email,
            company_name = company,
            company_support_number = s,
            is_advertisement_sponsorship = false,
            is_approve = true,
            vendor_category = s,
            status = s,
            about = buil.GetParagraphs(1),
            fine_print = s,
            company_category = s,
            lat = 34.0781452,
            lng = -117.5966181,
            address = buil.GetWords(10),
            actives = a,
            resource = r
        });

        v.Add(new VendorData()
        {
            id = s,
            email = s,
            created_at = n,
            company_email = email,
            company_name = company,
            company_support_number = s,
            is_advertisement_sponsorship = false,
            is_approve = true,
            vendor_category = s,
            status = s,
            about = buil.GetParagraphs(1),
            fine_print = s,
            company_category = s,
            lat = 34.0864767,
            lng = -117.5948274,
            address = buil.GetWords(10),
            actives = a,
            resource = r
        });

        v.Add(new VendorData()
        {
            id = s,
            email = s,
            created_at = n,
            company_email = email,
            company_name = company,
            company_support_number = s,
            is_advertisement_sponsorship = false,
            is_approve = true,
            vendor_category = s,
            status = s,
            about = buil.GetParagraphs(1),
            fine_print = s,
            company_category = s,
            lat = 35.0864767,
            lng = -118.5948274,
            address = buil.GetWords(10),
            actives = b,
            resource = r
        });

        v.Add(new VendorData()
        {
            id = s,
            email = s,
            created_at = n,
            company_email = email,
            company_name = company,
            company_support_number = s,
            is_advertisement_sponsorship = false,
            is_approve = true,
            vendor_category = s,
            status = s,
            about = buil.GetParagraphs(1),
            fine_print = s,
            company_category = s,
            lat = 33.0864767,
            lng = -113.5948274,
            address = buil.GetWords(10),
            actives = c,
            resource = r
        });


        var vs = new VendorDatas()
        {
            msg = v,
            current_page = 1,
            total_page = 1
        };

        return vs;
    }
}
