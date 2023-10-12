using System.Collections.Generic;
using UnityEngine;
using static ActivityManager;
using static StripeData;

public class StripeManager : MonoSingleton<StripeManager>
{
    public List<Product> ProductDatas = new List<Product>();
    public List<Price.Datum> PriceDatas = new List<Price.Datum>();
    public List<StripeItem> StripeList = new List<StripeItem>();
    public UserData.Root User = new UserData.Root();
    public GameObject ItemPrefab;
    public Transform Root;
    private string mProductID;
    private string mMsg;
    private bool isFoucsChange = true;
    private void Start()
    {
        //MessageBox.Instance.ShowText("Get Sprite Data...", true, true);
        //StartCoroutine(LoadSpriteData());
    }

    //IEnumerator LoadSpriteData()
    //{
    //    //User = StripeTool.GetUserData();
    //    //ProductDatas = StripeTool.GetProducts();
    //    //PriceDatas = StripeTool.GetPrices();
    //    //Debug.Log(ProductDatas.Count);
    //    //Debug.Log(PriceDatas.Count);
    //    //foreach (var item in ProductDatas)
    //    //{
    //    //    var o = GameObject.Instantiate(ItemPrefab, Root);
    //    //    var priceItem = PriceDatas.FirstOrDefault(p => p.product == item.id);
    //    //    StripeItem sp = o.GetComponent<StripeItem>();
    //    //    sp.InitData(item, priceItem);
    //    //}

    //    //yield return new WaitForSeconds(0.5f);
    //    //MessageBox.Instance.SysCloseMsg();
    //}

    public void UpdateStripeData(List<Active> activeList)
    {
        int index = 0;
        foreach (var active in activeList)
        {
            StripeItem sp = null;
            if (index >= StripeList.Count)
            {
                var o = GameObject.Instantiate(ItemPrefab, Root);
                sp = o.GetComponent<StripeItem>();
                StripeList.Add(sp);
            }
            else
            {
                sp = StripeList[index];
            }
            sp.InitData(active);
            index++;
        }
    }
}