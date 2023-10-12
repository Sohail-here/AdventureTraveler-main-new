using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FfmpegUnity;
using UnityEngine.Video;
using DanielLochner.Assets.SimpleScrollSnap;

public class Photo_Video : MonoSingleton<Photo_Video>
{
    public RawImage CaptureRawImg;
    public RawImage FinishRawImg;
    public RawImage LoadImage;
    public GameObject GalleryPanel;
    public GameObject FinishMediaPanel;

    public Camera AR_Camera;

    public OnlineMaps _2D_Map;

    public Texture2D[] PinIcon;

    //public List<string> Filenames = new List<string>();
    //public List<string> TypeOfActivity = new List<string>();

    public Image BlueCircle;
    public Image WhiteCircle;
    public Image RedCircle;
    public Image Recording;
    public Image StopRecrod;

    [HideInInspector]
    public bool isPhoto = true;

    public VideoPlayer GetVideoPlayer;

    const float VIDEO_RECORD_MAXTIME = 30;
    private float videoTime = 0;
    private bool isVideo = false;
    public Camera mediaCamera;

    [Header("SwtichCamera")]
    [SerializeField]
    private Button btnSwitchCamera;
    [SerializeField]
    private RawImage rimgSwitchCamera;

    [Header("Natcoder")]
    [SerializeField] private NatcoderManager m_NatcoderManager;

    public GameObject EndVideoButton;

    void OnEnable()
    {
        FinishRawImg.rectTransform.sizeDelta = new Vector2(1125, 2436);
        m_NatcoderManager.enabled = true;
        RecordManager.Instance?.PauseRecord(true);
    }

    void OnDisable()
    {
        m_NatcoderManager.enabled = false;
        RecordManager.Instance?.PauseRecord(false);
        AR_Controller.Instance.FaceTipControl(false);
    }

    void Start()
    {
        mediaCamera = AR_Camera;
    }

    void Update()
    {
        if (isVideo)
        {
            RedCircle.fillAmount = videoTime / VIDEO_RECORD_MAXTIME;

            videoTime += Time.deltaTime;

            if (videoTime > VIDEO_RECORD_MAXTIME)
            {
                StopVideo();
                videoTime = 0;
            }
        }
        else
        {
            videoTime = 0;
        }
    }

    public void SwitchCaptureMode()
    {
        Debug.Log($"0 isPhoto = {isPhoto}");
        isPhoto = !isPhoto;
        Debug.Log($"1 isPhoto = {isPhoto}");
        ResetUI();
        AR_Controller.Instance?.AR_Swap();
    }

    public void ResetUI()
    {
        Debug.Log($"2 isPhoto = {isPhoto}");

        if (isPhoto)
        {
            ResetPhotoUI();
        }
        else
        {
            ResetVideoUI();
        }
    }

    public void ResetPhotoUI()
    {
        BlueCircle.gameObject.SetActive(true);
        WhiteCircle.gameObject.SetActive(false);
        StopRecrod.gameObject.SetActive(false);
        Recording.gameObject.SetActive(false);

        ChangeCamera.Instance.CloseAR_Plane();
        //if (PlaceOnPlane.Instance != null)
        PlaceOnPlane.Instance?.DestroyPrefab();
    }

    public void ResetVideoUI()
    {
        BlueCircle.gameObject.SetActive(false);
        WhiteCircle.gameObject.SetActive(true);
        StopRecrod.gameObject.SetActive(true);
        Recording.gameObject.SetActive(false);

        ChangeCamera.Instance.CloseAR_Plane();
        ChangeCamera.Instance.Session.Reset();
    }

    public void TakePhoto()
    {
        Debug.Log("TakePhoto");
        FinishRawImg.gameObject.SetActive(true);
        StartCoroutine(C_TakePhoto());
    }

    IEnumerator C_TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        FinishMediaPanel.SetActive(true);
        AR_Controller.Instance.FaceTipControl(false);
        FinishRawImg.uvRect = isPhoto ? CaptureRawImg.uvRect : new Rect(0, 0, 1, 1);
        FinishRawImg.texture = RTImage();
        //CaptureRawImg.gameObject.SetActive(false);
    }

    Texture2D RTImage()
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = mediaCamera.targetTexture;
        // Render the camera's view.
        mediaCamera.Render();
        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(mediaCamera.targetTexture.width, mediaCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, mediaCamera.targetTexture.width, mediaCamera.targetTexture.height), 0, 0);
        image.Apply();
        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }

    public void SetAR_Camera()
    {
        mediaCamera = AR_Camera;
    }

    #region Native

    public void AddItem()
    {
        if (string.IsNullOrEmpty(mGalleryVedioStr) == false)
        {
            UIManager.Instance.Filename = UIManager.Instance.GetTime() + ".mp4";
            File.Copy(mGalleryVedioStr, Application.persistentDataPath + "/" + UIManager.Instance.Filename, true);
            mGalleryVedioStr = string.Empty;
            return;
        }

        if (isPhoto)
        {
            Texture2D rawImageTexture = (Texture2D)FinishRawImg.texture;
            byte[] pngData = rawImageTexture.EncodeToPNG();

            UIManager.Instance.Filename = UIManager.Instance.GetTime() + ".png";
            File.WriteAllBytes(Application.persistentDataPath + "/" + UIManager.Instance.Filename, pngData);
        }
        else
        {
            //UIManager.Instance.Filename = Application.persistentDataPath + "/" + UIManager.Instance.GetTime() + ".mp4";

            // Save the video to Gallery
#if UNITY_EDITOR_OSX
            NativeGallery.SaveVideoToGallery(UIManager.Instance.Filename, "AT_Gallery", UIManager.Instance.Filename, null);
            //Test.Instance.DebugPrint(UIManager.Instance.Filename);
#elif UNITY_IPHONE
            NativeGallery.SaveVideoToGallery(UIManager.Instance.Filename, "AT_Gallery", UIManager.Instance.Filename, null);
            //Test.Instance.DebugPrint(UIManager.Instance.Filename);
#else
            NativeGallery.SaveVideoToGallery(Application.persistentDataPath + "/" + UIManager.Instance.Filename, "AT_Gallery", UIManager.Instance.Filename, null);
            //Test.Instance.DebugPrint("photovideo additem video-- " + Application.persistentDataPath + "/" + UIManager.Instance.Filename);
#endif
            Debug.Log("Save video to Gallery");
        }
    }

    private string mGalleryVedioStr;
    public void OpenGallery()
    {
        if (NativeGallery.CanSelectMultipleMediaTypesFromGallery())
        {
            NativeGallery.Permission permission = NativeGallery.GetMixedMediaFromGallery((path) =>
            {
                Debug.Log("Media path : " + path);
                if (path != null)
                {
                    FinishRawImg.uvRect = isPhoto ? CaptureRawImg.uvRect : new Rect(0, 0, 1, 1);
                    switch (NativeGallery.GetMediaTypeOfFile(path))
                    {
                        case NativeGallery.MediaType.Image:
                            Debug.Log("Pick Image");

                            FinishMediaPanel.SetActive(true);
                            FinishRawImg.uvRect = CaptureRawImg.uvRect;
                            FinishRawImg.texture = NativeGallery.LoadImageAtPath(path, 2048, false);
                            FinishRawImg.SetNativeSize();
                            var size = FinishRawImg.rectTransform.sizeDelta;
                            Debug.Log($"Load Image Size {FinishRawImg.rectTransform.sizeDelta}");
                            float ratioX = 1125 / size.x;
                            float ratioY = 2436 / size.y;
                            float r = Mathf.Min(ratioX, ratioY);
                            FinishRawImg.rectTransform.sizeDelta = new Vector2(size.x * r, size.y * r);

                            //Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                            if (FinishRawImg.texture == null)
                            {
                                Debug.Log("Couldn't load texture from " + path);
                                return;
                            }
                            isPhoto = true;
                            break;
                        case NativeGallery.MediaType.Video:
                            if (path != null)
                            {
                                // Play the selected video
                                mGalleryVedioStr = GetVideoPlayer.url = $"{path}";
                                FinishRawImg.texture = GetVideoPlayer.targetTexture;
                                FinishMediaPanel.SetActive(true);
                                GetVideoPlayer.enabled = true;
                                GetVideoPlayer.Play();

                                isPhoto = false;
                            }
                            break;
                        default:
                            Debug.Log("Probably picked something else");
                            break;
                    }
                }
            }, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video, "Select an Image or Video");

            Debug.Log("Permission result : " + permission);
        }
    }

    public void Share()
    {
        if (isPhoto)
        {
            NativeUse.Instance.ShareWithOtherApp(FinishMediaPanel, "AdventureTraveler", "Join Now!", "https://www.google.com/");
        }
        else
        {
            //string filePath = UIManager.Instance.Filename;
            //string filePath = Application.persistentDataPath + "/" + UIManager.Instance.Filename;

#if UNITY_EDITOR_WIN
            string filePath = Application.persistentDataPath + "/" + UIManager.Instance.Filename;
            //Test.Instance.DebugPrint("share filePath-- " + filePath);
#elif UNITY_EDITOR_OSX
            string filePath = UIManager.Instance.Filename;
            //Test.Instance.DebugPrint("share filePath-- " + filePath);
#elif UNITY_IPHONE
            string filePath = UIManager.Instance.Filename;
            //Test.Instance.DebugPrint("share filePath-- " + filePath);
#elif UNITY_ANDROID
            string filePath = UIManager.Instance.Filename;
            //Test.Instance.DebugPrint("share filePath-- " + filePath);
#else
            string filePath = UIManager.Instance.Filename;
            //Test.Instance.DebugPrint("share filePath-- " + filePath);
#endif
            NativeUse.Instance.ShareVideoWithOtherApp(filePath);
            Debug.Log("Save video to Gallery");
        }
    }

    #endregion

    private void AddPin(Texture2D pin, string type)
    {
        _2D_Map.GetComponent<OnlineMapsMarkerManager>().Create(new Vector2(_2D_Map.position.x, _2D_Map.position.y), pin, type).scale = 0.3f;
    }

    // For btn_Upload
    // The files are not upload to cloud now, they are just saved in local device.
    public void Upload()
    {
        //Filenames.Add(UIManager.Instance.Filename);

        string type = PinSelecter.Instance.SV_Pins_Content.transform.GetChild(PinSelecter.Instance.SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel).name;
        //Texture2D pinIcon = PinIcon[PinSelecter.Instance.SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel];
        //TypeOfActivity.Add(type);
        //AddPin(pinIcon as Texture2D, type);

        CloseVideo();

        try
        {
            RecordManager.Instance.UploadFile(type, UIManager.Instance.Filename);
        }
        catch (System.Exception e)
        {
            UIManager.Instance.WarnPanelTitle("Upload Fail !");
            UIManager.Instance.WarnPanelContent(e.ToString());
        }

        ChangeCamera.Instance.OpenUserCamera();
        ChangeCamera.Instance.CloseAR_Plane();
        ChangeCamera.Instance.Session.Reset();

        PlaceOnPlane.Instance.DestroyPrefab();

        //Test.Instance.DebugPrint("editor photovideo upload filename-- " + UIManager.Instance.Filename);
    }

    public Texture2D GetPinIcon(string iconName)
    {
        foreach (var icon in PinIcon)
        {
            Debug.Log(icon.name);
            if (icon.name == iconName)
                return icon;
        }
        return null;
    }

    #region Take video

    public void StartVideo()
    {
        //Debug.Log("StartVideo");

        isVideo = true;

        //Ffmpeg.StartFfmpeg();
        NatcoderManager.Instance.StartRecording();

        RedCircle.gameObject.SetActive(true);
        StopRecrod.gameObject.SetActive(false);
        Recording.gameObject.SetActive(true);
    }

    public void StopVideo()
    {
        //Debug.Log("StopVideo");
        FinishRawImg.uvRect = isPhoto ? CaptureRawImg.uvRect : new Rect(0, 0, 1, 1);

        if (videoTime == 0)
        {
            videoTime = 0;
            isVideo = false;
            NatcoderManager.Instance.CancelRecording();
            return;
        }
        else if (videoTime < 3)
        {
            Debug.Log("Video Error");
            // Error
            //Ffmpeg.StopFfmpeg();
            try
            {
                NatcoderManager.Instance.StopRecording();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                throw;
            }

            UIManager.Instance.WarnPanelTitle("Warning Video!");
            UIManager.Instance.WarnPanelContent("Can't be less than 3 seconds!");
        }
        else if (videoTime > VIDEO_RECORD_MAXTIME)
        {
            Debug.Log("Video Success Stop");
            // Success
            //Ffmpeg.StopFfmpeg();
            try
            {
                NatcoderManager.Instance.StopRecording();
                EndVideoButton.SetActive(false);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                throw;
            }

            Debug.Log("After Stop-- " + UIManager.Instance.Filename);
            //MonoExpend.Instance.CheckFileRunDone(Application.persistentDataPath + "/" + UIManager.Instance.Filename, PlayVideoOnRawImage);          
            UIManager.Instance.OpenWaitingPanel();
            //CaptureRawImg.gameObject.SetActive(false);
            FinishRawImg.gameObject.SetActive(false);
            FinishMediaPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Video Success");
            // Success
            //Ffmpeg.StopFfmpeg();
            try
            {
                NatcoderManager.Instance.StopRecording();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                throw;
            }

            Debug.Log("After Stop-- " + UIManager.Instance.Filename);
            //MonoExpend.Instance.CheckFileRunDone(Application.persistentDataPath + "/" + UIManager.Instance.Filename, PlayVideoOnRawImage);
            UIManager.Instance.OpenWaitingPanel();
            //CaptureRawImg.gameObject.SetActive(false);
            FinishRawImg.gameObject.SetActive(false);
            FinishMediaPanel.SetActive(true);
        }

        videoTime = 0;
        isVideo = false;

        RedCircle.fillAmount = 0;
        RedCircle.gameObject.SetActive(false);
        StopRecrod.gameObject.SetActive(true);
        Recording.gameObject.SetActive(false);
    }

    public void PlayVideoOnRawImage()
    {
        GetVideoPlayer.enabled = true;

        //Test.Instance.DebugPrint("photovideo playvideoonrawimage-- " + UIManager.Instance.Filename);

#if UNITY_EDITOR_WIN
        GetVideoPlayer.url = "file://" + Application.persistentDataPath + "/" + UIManager.Instance.Filename;
#elif UNITY_EDITOR_OSX
        GetVideoPlayer.url = "file://" + UIManager.Instance.Filename;
#elif UNITY_ANDROID
        GetVideoPlayer.url = "file://" + UIManager.Instance.Filename;
#elif UNITY_IPHONE
        GetVideoPlayer.url = "file://" + UIManager.Instance.Filename;
#else
        GetVideoPlayer.url = "file://" + UIManager.Instance.Filename;
#endif
        FinishMediaPanel.GetComponent<RawImage>().texture = GetVideoPlayer.targetTexture;
        GetVideoPlayer.Play();
        UIManager.Instance.CloseWaitingPanel();
    }

    public void CloseVideo()
    {
        GetVideoPlayer.Stop();
        FinishMediaPanel.GetComponent<RawImage>().texture = null;
        GetVideoPlayer.enabled = false;
    }

    #endregion

    public void OpenBtnSwitchCamera()
    {
        btnSwitchCamera.interactable = true;
        rimgSwitchCamera.color = new Color(1, 1, 1, 1);
    }

    public void CloseBtnSwitchCamera()
    {
        btnSwitchCamera.interactable = false;
        rimgSwitchCamera.color = new Color(0.776f, 0.776f, 0.776f);
    }

}