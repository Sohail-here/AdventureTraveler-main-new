using System.Collections.Generic;
using UnityEngine;
using HT.InfiniteList;

public class LoopScrollView : MonoBehaviour
{
    public InfiniteListScrollRect infiniteList;

    private string[] journeyAcrivity = new string[]
    {
        "Cycling", "Hiking", "Walking", "Boating", "Canoeing",  "Kayaking",
        "E-biking", "Fishing",  "Golfing",  "Skiing",  "Ice Skating",  "Jetskiing",
        "Mountain Biking",  "Rowing",  "Sailing",  "Surfing",  "Swimming",  "Walking",
        "Snow Boarding",  "Paddle Boarding",  "Other"
    };

    // Start is called before the first frame update
    void Start()
    {
        //AddTwoData();

        //请注意，由于此事件会在列表到达尾部时添加2条数据，而如果删除数据，会收缩列表，导致列表到达尾部，从而又触发添加数据，导致删除数据无效
        //所以，如果有这个需求（列表滚动到尾部自动添加数据），则最好禁用删除数据功能
        /*infiniteList.onValueChanged.AddListener((value) =>
        {
            if (infiniteList.ListingDirection == InfiniteListScrollRect.Direction.Vertical && value.y <= 0)
            {
                //Debug.Log("到達列表尾端，在尾端新增2筆資料！");
                AddTwoData();
            }
            else if (infiniteList.ListingDirection == InfiniteListScrollRect.Direction.Horizontal && value.x >= 1)
            {
                //Debug.Log("到達列表尾端，在尾端新增2筆資料！");
                AddTwoData();
            }
        });*/
    }

    public void AddTwoData()
    {
        List<InfiniteListTestData> datas = new List<InfiniteListTestData>();

        for (int i = 0; i < 21; i++)
        {
            InfiniteListTestData data = new InfiniteListTestData();
            data.Name = journeyAcrivity[i];
            datas.Add(data);
        }
        infiniteList.AddDatas(datas);

        //Debug.Log("目前列表數據總量：" + infiniteList.DataCount);
    }
}
