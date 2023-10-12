using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static ActivityManager;

public class ActivityCompanyItem : MonoBehaviour
{
    public Texture2D IsSponIcon;
    public Texture2D NotSponIcon;
    public Texture2D IsSponSelectIcon;
    public Texture2D NotSponSelectIcon;
    public Sprite ColdIcon;
    public Sprite IndoorIcon;
    public Sprite OtherIcon;
    public Sprite OutdoorIcon;
    public Sprite WarmIcon;

    public RawImage Title_Img;
    public Image Sponsored_Img;
    public Button ActivityContentBtn;
    public TextMeshProUGUI PlaceTxt;
    public TextMeshProUGUI KindOfActivityTxt;
    public Image KindOfActivityIcon;
    public OnlineMapsMarker MapMarker;

    private VendorData mVendor;
    private ActivityCompanyItem _thisItem;
    private Coroutine mLoadImageCo;

    static Dictionary<string, Texture> mDicLoad = new Dictionary<string, Texture>();
    private void Start()
    {
        if (gameObject.activeInHierarchy && ActivityContentBtn != null)
            ActivityContentBtn.onClick.AddListener(ShowDetail);
    }

    public void UpdateData(VendorData vendor)
    {
        if (vendor == null || gameObject.activeInHierarchy == false)
            return;

        mVendor = vendor;
        //Sponsored_Img.sprite = vendor.is_advertisement_sponsorship ? IsSponIcon : NotSponIcon;
        MapMarker = OnlineMapsMarkerManager.CreateItem(vendor.lng, vendor.lat, vendor.is_advertisement_sponsorship ? IsSponIcon : NotSponIcon);
        MapMarker.scale = 0.3f;

        MapMarker.OnClick = (mk) =>
        {
            ActivityManager.Instance.SelectActivityMarker(this);
            ActivityManager.Instance.MiniCompanyItem.UpdateMiniItem(this, mVendor);

            var selectSprite = mVendor.is_advertisement_sponsorship ? IsSponSelectIcon : NotSponSelectIcon;
            MapMarker.texture = selectSprite;
        };
        PlaceTxt.text = vendor.company_name;
        KindOfActivityTxt.text = vendor.company_category;
        var typeStr = vendor.company_category.Split(' ')[0];
        switch (typeStr)
        {
            case "Indoor":
                KindOfActivityIcon.sprite = IndoorIcon;
                break;
            case "OutDoor":
                KindOfActivityIcon.sprite = OutdoorIcon;
                break;
            case "Cold":
                KindOfActivityIcon.sprite = ColdIcon;
                break;
            case "Warm":
                KindOfActivityIcon.sprite = WarmIcon;
                break;
            default:
                KindOfActivityIcon.sprite = OtherIcon;
                break;
        }
        //Clear 
        Title_Img.texture = null;
        if (mLoadImageCo != null)
        {
            this.StopCoroutine(mLoadImageCo);
        }

        if (vendor.resource != null && vendor.resource.Count > 0)
        {
            if (gameObject.activeInHierarchy == false)
                gameObject.SetActive(true);
            string url = vendor.resource[0].resource_url;
            if (mDicLoad.ContainsKey(url))
            {
                Title_Img.texture = mDicLoad[url];
            }
            else
            {
                mLoadImageCo = this.StartCoroutine(LoadImg(url));
            }
        }
    }

    IEnumerator LoadImg(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            yield return ((DownloadHandlerTexture)www.downloadHandler).isDone;
            if (((DownloadHandlerTexture)www.downloadHandler).isDone)
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Title_Img.texture = myTexture;
                if (mDicLoad.ContainsKey(url))
                    ULog.Penguin.Log($"mDicLoad Has Mutil Url");
                else
                    mDicLoad.Add(url, myTexture);
            }
        }
    }

    public void ClearIcon()
    {
        OnlineMapsMarkerManager.instance.Remove(MapMarker);
    }

    public void UpdateMiniItem(ActivityCompanyItem data, VendorData vendor)
    {
        Title_Img.texture = data.Title_Img.texture;
        Sponsored_Img.sprite = data.Sponsored_Img.sprite;
        PlaceTxt.text = data.PlaceTxt.text;
        KindOfActivityTxt.text = data.KindOfActivityTxt.text;
        KindOfActivityIcon.sprite = data.KindOfActivityIcon.sprite;
        mVendor = vendor;
        if (this.gameObject.activeInHierarchy == false)
            this.gameObject.SetActive(true);
    }

    public void ShowDetail()
    {
        if (mVendor != null)
        {
            SingletonData.Instance.SelectVendor(mVendor);
            ActivityManager.Instance.ShowVendonDetail(mVendor);
        }
    }

    public void ResetIcon()
    {
        if (mVendor != null)
        {
            var sprite = mVendor.is_advertisement_sponsorship ? IsSponIcon : NotSponIcon;
            MapMarker.texture = sprite;
        }
    }
}