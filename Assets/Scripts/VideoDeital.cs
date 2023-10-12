using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnDestroyData;

public class VideoDeital : MonoBehaviour
{
    public TMP_Text DistanceText;
    public TMP_Text DurationText;
    public TMP_Text ActivityText;
    public RawImage ScreenShot;
    public Button PlayBtn;
    public Button CloseBtn;
    public Button ShareBtn;
    public Button PostBtn;
    public string Url;
    [Header("Data")]
    [SerializeField] private JourneyMsg tempMsg;

    [SerializeField] private Texture m_DefaultImage;
    [SerializeField] private TMP_Text m_TitleText;
    [SerializeField] private TMP_Text m_TitleInfoText;
    [SerializeField] private TMP_Text m_DetailAnnounce;
    [SerializeField] private Button m_BtnPost;
    [SerializeField] private Button m_BtnUpload;
    [SerializeField] private Button m_BtnDLViedo;
    [SerializeField] private GameObject m_DLPage;
    Coroutine m_DL_Video;

    private void Start()
    {
        PlayBtn.onClick.AddListener(() =>
        {
#if UNITY_ANDROID
            WebViewMgr.Instance.OpenURL(Url);
#else
            WebView.Instance.Open(Url);
#endif
            Debug.Log("WebViewOpen");
        });
        CloseBtn.onClick.AddListener(() => { gameObject.SetActive(false); });
        ShareBtn.onClick.AddListener(() =>
        {
            Debug.Log("Detail Share");
            NativeUse.Instance.ShareWithOtherApp("AdventureTraveler", "Join Now !", Url);
        });
        PostBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            //WebView.Instance.Open("https://at-b.s3.us-west-2.amazonaws.com/video/qwe4906qwe@gmail.com/270220220700.mp4");
        });
        m_BtnDLViedo.onClick.AddListener(() =>
        {
            if (m_DL_Video == null && string.IsNullOrEmpty(Url) == false)
            {
                m_DL_Video = StartCoroutine(DownloadVideo());
            }
        });
    }

    IEnumerator DownloadVideo()
    {
        m_DLPage.SetActive(true);
        UnityWebRequest www = UnityWebRequest.Get(Url);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.LogError(www.error);
            m_DL_Video = null;
        }
        else
        {
            File.WriteAllBytes(Application.persistentDataPath + "/" + tempMsg.journey_id + ".mp4", www.downloadHandler.data);
            NativeGallery.SaveVideoToGallery(www.downloadHandler.data, "AdventureTraveler", tempMsg.journey_id + ".mp4", (success, path) =>
            {
                if (success)
                {
                    Debug.Log($"path:{path} - {Application.persistentDataPath + "/" + tempMsg.journey_id + ".mp4"}");
                    m_DL_Video = null;
                }
            });
        }

        m_DLPage.SetActive(false);
    }

    public void ShowDetail(ref JourneyMsg itemData, ref RawImage videoRawImage, string url)
    {
        gameObject.SetActive(true);
        Debug.Log($"{itemData.distance}");
        tempMsg = itemData;
        ScreenShot.texture = videoRawImage.texture;
        DistanceText.text = itemData.distance;
        DurationText.text = itemData.duration;
        ActivityText.text = itemData.category;
        Url = url;
        //m_TitleText.text = "Journey Details";
        switch (LanguageManager.Instance.Mode)
        {
            case "English":
                m_TitleText.text = "Journey Details";
                break;
            case "French":
                m_TitleText.text = "Détails du voyage";
                break;
            case "Spanish":
                m_TitleText.text = "Detalles del Viaje";
                break;
        }

        //m_TitleInfoText.text = @"Your journey is posted onto the global feed";
        switch (LanguageManager.Instance.Mode)
        {
            case "English":
                m_TitleInfoText.text = @"Your journey is posted onto the global feed";
                break;
            case "French":
                m_TitleInfoText.text = @"Votre voyage est publié sur le flux global";
                break;
            case "Spanish":
                m_TitleInfoText.text = @"Su viaje se publica en el feed global";
                break;
        }

        m_BtnUpload.gameObject.SetActive(false);
        m_BtnPost.gameObject.SetActive(true);

        m_DetailAnnounce.gameObject.SetActive(true);
        m_BtnDLViedo.gameObject.SetActive(true);
    }

    public void ShowDetail(ref JourneyMsg itemData)
    {
        gameObject.SetActive(true);
        tempMsg = itemData;
        ScreenShot.texture = m_DefaultImage;
        DistanceText.text = itemData.distance;
        DurationText.text = itemData.duration;
        ActivityText.text = itemData.category;
        m_BtnUpload.gameObject.SetActive(true);
        m_BtnPost.gameObject.SetActive(false);
        m_DetailAnnounce.gameObject.SetActive(false);
        m_BtnDLViedo.gameObject.SetActive(false);
        Url = "";

        //m_TitleText.text = "Journey Uploaded";
        switch (LanguageManager.Instance.Mode)
        {
            case "English":
                m_TitleText.text = "Journey Uploaded";
                break;
            case "French":
                m_TitleText.text = "Voyage téléchargé";
                break;
            case "Spanish":
                m_TitleText.text = "Viaje subido";
                break;
        }

        //m_TitleInfoText.text = @"Your journey has been uploaded onto the server. Please give us sometime to convert to a 3D video";
        switch (LanguageManager.Instance.Mode)
        {
            case "English":
                m_TitleInfoText.text = @"Your journey has been uploaded onto the server. Please give us sometime to convert to a 3D video";
                break;
            case "French":
                m_TitleInfoText.text = @"Votre voyage a été téléchargé sur le serveur. Veuillez nous donner un peu de temps pour convertir en vidéo 3D";
                break;
            case "Spanish":
                m_TitleInfoText.text = @"Su viaje ha sido cargado en el servidor. Danos algo de tiempo para convertir a un video 3D";
                break;
        }
    }
}
