using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutFix : MonoBehaviour
{
    [Header("JoinJourneyPage")]
    public GameObject JJP_Title;
    public GameObject JJP_Content;

    [Header("VideoDetail")]
    public GameObject VD_Title;
    public GameObject VD_Announce;
    public GameObject VD_ScreenShot;
    public GameObject VD_Datas;

    public GameObject VD_DP_Title;
    public GameObject VD_DP_Content;

    [Header("ListView")]
    public GameObject LV_svList;

    [Header("AwardDetails")]
    public GameObject AD_TheExplorer;
    public GameObject AD_HorizonSummit;
    public GameObject AD_GoGetter;
    public GameObject AD_PassageOfSilver;
    public GameObject AD_WanderLuster;
    public GameObject AD_TravelBug;
    public GameObject AD_ThurlodgePass;
    public GameObject AD_HerculeanRing;
    public GameObject AD_AdventureTraveler;

    // Start is called before the first frame update
    void Start()
    {
        float screen = (Screen.height) / (float)(Screen.width);

        if (screen < 1.8)
        {
            Fix_All();
        }
    }

    private void Fix_All()
    {
        Fix_JoinJourneyPage();
        Fix_VideoDetail();
        Fix_ListView();
        Fix_AwardDetails();
    }

    private void Fix_JoinJourneyPage()
    {
        JJP_Title.transform.position += new Vector3(0, 25, 0);
        JJP_Content.transform.position += new Vector3(0, 25, 0);
    }


    public void Fix_VideoDetail()
    {
        VD_Title.transform.position += new Vector3(0, 110, 0);
        VD_Announce.transform.position += new Vector3(0, 160, 0);
        VD_ScreenShot.transform.position += new Vector3(0, 60, 0);
        VD_Datas.transform.position += new Vector3(0, -117, 0);

        VD_DP_Title.transform.position += new Vector3(0, 60, 0);
        VD_DP_Content.transform.position += new Vector3(0, 60, 0);
    }

    public void Fix_ListView()
    {
        LV_svList.GetComponent<RectTransform>().sizeDelta = new Vector2(1125, 1200);
        LV_svList.GetComponent<UIAnimations>().DownPos = 1038;
        LV_svList.transform.position += new Vector3(0, 422, 0);
    }

    public void Fix_AwardDetails()
    {
        AD_TheExplorer.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_HorizonSummit.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_GoGetter.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_PassageOfSilver.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_WanderLuster.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_TravelBug.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_ThurlodgePass.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_HerculeanRing.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
        AD_AdventureTraveler.GetComponent<RectTransform>().sizeDelta *= new Vector2(0, 0.75f);
    }
}