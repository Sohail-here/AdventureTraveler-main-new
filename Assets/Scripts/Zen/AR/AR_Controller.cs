using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using UnityEngine.SpatialTracking;

public class AR_Controller : MonoSingleton<AR_Controller>
{
    public GameObject[] PhotoBtns;
    public GameObject[] VideoBtns;

    public GameObject[] WorldModels;
    public GameObject[] VideoCircles;

    public Camera AR_Camera;
    public Camera Art_Camera;

    public RawImage CaptureImage;
    public RenderTexture AR_CameraRender;
    public RenderTexture Art_CameraRender;
    public Image MaskImage;

    public SimpleScrollSnap AR_Selecter;

    public int SwapCounterIndex => AR_Selecter == null ? 0 : AR_Selecter.CurrentPanel;
    private int mCaechSwapIndex = -1;

    [Header("FaceRenderer")]
    public Renderer FaceRenderer;

    [Header("AR Session Components")]
    public GameObject SessionOriginObj;
    public ARSessionOrigin SessionOrigin;
    public ARSession Session;
    [SerializeField] private ARInputManager m_ARInput;
    [SerializeField] private ARPlaneManager m_PlaneManager;
    [SerializeField] private ARFaceManager m_FaceMgr;
    [SerializeField] private Camera m_FaceCamera;
    [SerializeField] private TrackedPoseDriver m_TrackedPoseDriver;
    [SerializeField] private ARCameraManager m_ARCameraManager;
    [SerializeField] private UIAnimations UIAni;

    [Header("Open AR System")]
    public Photo_Video PhotoVideoPanel;
    public GameObject CapturePanel;
    public GameObject ZeroPoint;
    [SerializeField] private GameObject m_ArtCamera;

    [SerializeField] private List<GameObject> m_CacheArtRend = new List<GameObject>();
    private Dictionary<int, Renderer> mRendererDic = new Dictionary<int, Renderer>();
    [Header("Art")]
    [SerializeField] private List<GameObject> m_ListArt = new List<GameObject>();

    private Rect mCam_Rect = new Rect(0, 0, 1, 1);
    private Rect mArt_Rect = new Rect(0.25f, 0, 0.5f, 1);
    private bool mMyFaceGet = false;
    private bool mFaceOutOfRange = false;
    private bool mIsUseFaceTrack => SwapCounterIndex > 0 && SwapCounterIndex < 4;

    public Camera FCam;
    public GameObject FaceImageObj;
    public GameObject RendObj;

    const float WF = 1125;
    const float WH = 2436;
    public float Ratio = 1.5f;
    [SerializeField] private GameObject m_GetFaceTip;
    [SerializeField] private RenderTexture RendImg;
    [SerializeField] private Button m_Btn_OpenGallery;
    private Dictionary<int, Transform> mTransDict = new Dictionary<int, Transform>();
    private Transform mCacheObject = null;
    private Touch oldTouch1;
    private Touch oldTouch2;

    protected override void Awake()
    {
        base.Awake();
        m_FaceMgr.enabled = false;
    }

    private void Update()
    {
        if (SwapCounterIndex >= 4 && mCacheObject != null)
        {
            UpdateOBJScale();
        }

    }

    public void FaceTipControl(bool isTracking)
    {
        if (PhotoVideoPanel.FinishMediaPanel.activeInHierarchy || Photo_Video.Instance.gameObject.activeInHierarchy == false)
        {
            m_GetFaceTip.SetActive(false);
            return;
        }

        if (PenguinDebugPanel.Instance != null && PenguinDebugPanel.Instance.gameObject.activeInHierarchy)
        {
            isTracking = false;
            m_GetFaceTip.SetActive(false);
        }

        if (mIsUseFaceTrack == false)
        {
            if (m_GetFaceTip.activeInHierarchy)
                m_GetFaceTip.SetActive(false);
        }

        mMyFaceGet = isTracking;
        ULog.Penguin.Log($"FaceTip {isTracking} -- {mFaceOutOfRange}");
        if (isTracking && mFaceOutOfRange == false)
        {
            if (m_GetFaceTip.activeInHierarchy)
                m_GetFaceTip.SetActive(false);
            if (SwapCounterIndex > 0 && SwapCounterIndex < 4 && m_CacheArtRend[SwapCounterIndex - 1].activeInHierarchy == false)
                m_CacheArtRend[SwapCounterIndex - 1].SetActive(true);
            FaceImageObj.SetActive(true);
        }
        else
        {
            //FaceRenderer.material.mainTextureScale = Vector2.one;
            //FaceRenderer.material.mainTextureOffset = Vector2.zero;

            if (!m_GetFaceTip.activeInHierarchy)
            {
                m_GetFaceTip.SetActive(true);
                Session.Reset();
            }

            if (SwapCounterIndex > 0 && SwapCounterIndex < 4 && m_CacheArtRend[SwapCounterIndex - 1].activeInHierarchy)
                m_CacheArtRend[SwapCounterIndex - 1].SetActive(false);
            FaceImageObj.SetActive(false);
        }
    }

    public void RegisterSacleObj(Transform trans)
    {
        if (mTransDict.ContainsKey(SwapCounterIndex) == false)
        {
            mTransDict.Add(SwapCounterIndex, trans);
        }
        else
        {
            mTransDict[SwapCounterIndex] = trans;
        }

        mCacheObject = trans;
    }

    float mOffsetBase = 300;
    public void SetOffsetBase(float v)
    {
        mOffsetBase = v;
    }

    public void UpdateFacePos(FacePoint point)
    {
        if (PenguinDebugPanel.Instance != null && PenguinDebugPanel.Instance.gameObject.activeInHierarchy || Photo_Video.Instance.FinishMediaPanel.activeInHierarchy)
        {
            FaceTipControl(false);
            return;
        }

        if (mIsUseFaceTrack && FaceImageObj != null && mMyFaceGet)
        {
            if (mCaechSwapIndex != SwapCounterIndex)
            {
                Debug.Log($"1 mCaechSwapIndex = {mCaechSwapIndex} localScale = {point.FaceRoot.localScale}");

                mCaechSwapIndex = SwapCounterIndex;

                switch (mCaechSwapIndex)
                {
                    case 1:
                        point.FaceRoot.localScale = Vector3.one * 0.175f;
                        Debug.Log($"mCaechSwapIndex Parse = {Vector3.one * 0.175f} point.FaceRoot.localScale = {point.FaceRoot.localScale.x}");
                        mOffsetBase = 200;
                        break;
                    case 2:
                        point.FaceRoot.localScale = Vector3.one * 0.276f;
                        Debug.Log($"mCaechSwapIndex Parse = {Vector3.one * 0.276f} point.FaceRoot.localScale = {point.FaceRoot.localScale.x}");
                        mOffsetBase = 425;
                        break;
                    case 3:
                        point.FaceRoot.localScale = Vector3.one * 0.281f;
                        Debug.Log($"mCaechSwapIndex Parse = {Vector3.one * 0.281f} point.FaceRoot.localScale = {point.FaceRoot.localScale.x}");
                        mOffsetBase = 450;
                        break;
                }

                Debug.Log($"2 mCaechSwapIndex = {mCaechSwapIndex} localScale = {point.FaceRoot.localScale}");
            }


            var C = m_FaceCamera.WorldToScreenPoint(point.CPoint.position);
            var sc = new Vector2(C.x / WF, C.y / WH);

            var TR = m_FaceCamera.WorldToScreenPoint(point.TRPoint.position);
            var TL = m_FaceCamera.WorldToScreenPoint(point.TLPoint.position);
            var BR = m_FaceCamera.WorldToScreenPoint(point.BRPoint.position);
            var BL = m_FaceCamera.WorldToScreenPoint(point.BLPoint.position);
            var w = Mathf.Abs(TL.x - TR.x) / WF;

            FCam.orthographicSize = 0.5f * w;
            FCam.transform.localPosition = new Vector3(sc.x - .5f, sc.y - .5f, -0.5f);
            FCam.transform.localEulerAngles = new Vector3(0, 0, point.CPoint.eulerAngles.z - m_FaceCamera.transform.eulerAngles.z);

            /*
            if (Screen.safeArea.Contains(TR) && Screen.safeArea.Contains(TL) &&
                Screen.safeArea.Contains(BR) && Screen.safeArea.Contains(BL))
            */

            float offsetVal = mOffsetBase * w;
            if (TR.x < -offsetVal || TR.x > Screen.width + offsetVal || TR.y < -offsetVal || TR.y > Screen.height + offsetVal ||
                TL.x < -offsetVal || TL.x > Screen.width + offsetVal || TL.y < -offsetVal || TL.y > Screen.height + offsetVal ||
                BR.x < -offsetVal || BR.x > Screen.width + offsetVal || BR.y < -offsetVal || BR.y > Screen.height + offsetVal ||
                BL.x < -offsetVal || BL.x > Screen.width + offsetVal || BL.y < -offsetVal || BL.y > Screen.height + offsetVal)
            {
                if (!mFaceOutOfRange)
                {
                    mFaceOutOfRange = true;
                    FaceTipControl(mMyFaceGet);
                }
            }
            else
            {
                if (mFaceOutOfRange)
                {
                    mFaceOutOfRange = false;
                    FaceTipControl(mMyFaceGet);
                }
            }

            ULog.Penguin.Log($" offsetVal : {offsetVal} --  W: {w}  -- {TR} : {TL} -- {BR} : {BL} -- {Screen.width} : {Screen.height} : {Screen.safeArea} --- {mFaceOutOfRange}");
            //Debug.Log($"-- Update Face Pos --{FCam.orthographicSize } -- {FCam.transform.localPosition} -- {FCam.transform.eulerAngles} -- {point.CPoint.eulerAngles} ");
            //(0.0, -0.2, -0.5) -- (0.0, 0.0, 1.5) -- (17.7, 185.4, 3.8) 
            /* v1
            var C = m_FaceCamera.WorldToScreenPoint(point.CPoint.position);
            var TR = m_FaceCamera.WorldToScreenPoint(point.TRPoint.position);
            var TL = m_FaceCamera.WorldToScreenPoint(point.TLPoint.position);
            var BR = m_FaceCamera.WorldToScreenPoint(point.BRPoint.position);
            var BL = m_FaceCamera.WorldToScreenPoint(point.BLPoint.position);

            var w = Mathf.Abs(TL.x - TR.x);
            var h = Mathf.Abs(TR.y - BR.y);
            var sc = new Vector2(w / WF, h / WH) * Ratio;

            FaceRenderer.material.mainTextureScale = sc;
            var ofx = (1 - sc.x) / 2 - (((WF / 2 - C.x) / WF));
            var ofy = (1 - sc.y) / 2 - (((WH / 2 - C.y) / WH));
            FaceRenderer.material.mainTextureOffset = new Vector2(ofx, ofy);
            */
        }
    }

    public void AR_Swap()
    {
        CameraFacingDirection c = CameraFacingDirection.User;
        OpenARKit(false);
        m_Btn_OpenGallery.interactable = true;
        m_Btn_OpenGallery.targetGraphic.color = new Color(1f, 1f, 1f);
        m_GetFaceTip.SetActive(false);
        FaceImageObj.SetActive(false);
        OpenARKit(true);
        m_TrackedPoseDriver.enabled = false;
        m_ARInput.enabled = false;
        m_FaceMgr.enabled = false;

        mCacheObject = null;

        ClearOutRenderTexture(RendImg);
        // Buttons controller

#if !UNITY_EDITOR
        LoaderUtility.Initialize();
#endif
        Application.targetFrameRate = 30;
        Photo_Video.Instance.ResetUI();
        Debug.Log("SwapPhoto");
        for (int i = 0; i < PhotoBtns.Length; i++)
        {
            PhotoBtns[i].SetActive(Photo_Video.Instance.isPhoto && i == SwapCounterIndex);
        }

        for (int i = 0; i < VideoBtns.Length; i++)
        {
            VideoBtns[i].SetActive(!Photo_Video.Instance.isPhoto && i == SwapCounterIndex);
        }

        if (NatcoderManager.Instance.TargetCamera != AR_Camera)
            NatcoderManager.Instance.TargetCamera = AR_Camera;

        m_ArtCamera.SetActive(SwapCounterIndex > 0 && SwapCounterIndex < 4);
        Photo_Video.Instance.mediaCamera = AR_Camera;
        NatcoderManager.Instance.TargetCamera = AR_Camera;

        // Origin camera
        if (SwapCounterIndex == 0)
        {
            CaptureImage.uvRect = mCam_Rect;
            CaptureImage.texture = AR_CameraRender;
            ChangeCamera.Instance.CloseAR_Plane();

            Photo_Video.Instance.OpenBtnSwitchCamera();

            CloseAR_Objs();
            PlaceOnPlane.Instance.DestroyPrefab();

            Photo_Video.Instance.SetAR_Camera();
            ChangeCamera.Instance.OpenUserCamera();
        }
        // User camera
        else if (SwapCounterIndex < 4)
        {
            m_Btn_OpenGallery.interactable = false;
            m_Btn_OpenGallery.targetGraphic.color = new Color(0.776f, 0.776f, 0.776f);
            m_GetFaceTip.SetActive(true);
            m_FaceMgr.enabled = true;
            m_TrackedPoseDriver.enabled = true;
            m_ARInput.enabled = true;

            CaptureImage.uvRect = mArt_Rect;
            CaptureImage.texture = Art_CameraRender;

            Photo_Video.Instance.mediaCamera = Art_Camera;
            NatcoderManager.Instance.TargetCamera = Art_Camera;

            ChangeCamera.Instance.CloseAR_Plane();
            Photo_Video.Instance.CloseBtnSwitchCamera();

            CloseAR_Objs();

            ChangeCamera.Instance.CloseComponents();
            PlaceOnPlane.Instance?.DestroyPrefab();

            //Photo_Video.Instance.SetSelfCamera();
            ChangeCamera.Instance.OpenUserCamera();
            int index = SwapCounterIndex - 1;

            var obj = m_ListArt[index];
            obj.SetActive(true);
            Renderer rend = null;
            if (mRendererDic.ContainsKey(index))
            {
                rend = mRendererDic[index];
            }
            else
            {
                rend = UIManager.Instance.FindChildGameObjectByName(obj, "ConceptFace").GetComponent<MeshRenderer>();
                mRendererDic.Add(index, rend);
            }

            FaceRenderer = rend;

            if (PenguinDebugPanel.Instance != null)
            {
                PenguinDebugPanel.Instance.RegisterFaceUV(FaceRenderer.material);
            }
            FaceTipControl(false);
        }
        // World camera
        else
        {
            c = CameraFacingDirection.World;
            ULog.Penguin.Log($"World camera -- ");
            CaptureImage.uvRect = mCam_Rect;
            CaptureImage.texture = AR_CameraRender;
            m_TrackedPoseDriver.enabled = true;
            m_ARInput.enabled = true;

            PlaceOnPlane.Instance.planeManager.planePrefab = PlaceOnPlane.Instance.PlanePrefab;
            foreach (var planes in PlaceOnPlane.Instance.planeManager.trackables)
            {
                planes.gameObject.SetActive(true);
            }
            Photo_Video.Instance.CloseBtnSwitchCamera();

            CloseAR_Objs();
            var obj = WorldModels[SwapCounterIndex - 4];
            PlaceOnPlane.Instance.placePrefab = obj;

            if (mTransDict.ContainsKey(SwapCounterIndex))
                mCacheObject = mTransDict[SwapCounterIndex];

            Photo_Video.Instance.SetAR_Camera();
            ChangeCamera.Instance.OpenWorldCamera();
            PlaceOnPlane.Instance.DestroyPrefab();
        }

        if (co_CheckCam != null)
            StopCoroutine(co_CheckCam);

        co_CheckCam = StartCoroutine(CoCheckCam(c));
    }
    Coroutine co_CheckCam;
    IEnumerator CoCheckCam(CameraFacingDirection cDirection)
    {
#if !UNITY_EDITOR
#if UNITY_IOS
        while (ChangeCamera.Instance.CameraManager.currentFacingDirection != cDirection)
        {
            AR_Camera.gameObject.SetActive(false);
            CaptureImage.gameObject.SetActive(false);
            MaskImage.gameObject.SetActive(true);

            //ULog.Penguin.Log($"{ChangeCamera.Instance.CameraManager.currentFacingDirection} -- {cDirection}");
            yield return new WaitForEndOfFrame();
        }
#elif UNITY_ANDROID
        while (ChangeCamera.Instance.CameraManager.requestedFacingDirection != cDirection)
        {
            AR_Camera.gameObject.SetActive(false);
            CaptureImage.gameObject.SetActive(false);
            MaskImage.gameObject.SetActive(true);

            //ULog.Penguin.Log($"{ChangeCamera.Instance.CameraManager.requestedFacingDirection} -- {cDirection}");
            yield return new WaitForEndOfFrame();
        }
#endif
#endif
        yield return new WaitForSeconds(1.5f);
        MaskImage.gameObject.SetActive(false);
        AR_Camera.gameObject.SetActive(true);
        CaptureImage.gameObject.SetActive(true);
        Session.Reset();
    }

    [ContextMenu("Clear Test")]
    public void TestClear()
    {
        ClearOutRenderTexture(RendImg);
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        renderTexture.Release();
        renderTexture.DiscardContents();
    }

    public void OpenARKit(bool open)
    {
        Photo_Video.Instance.gameObject.SetActive(open);

        if (ZeroPoint == null)
            return;

        if (ZeroPoint.activeInHierarchy != open)
            ZeroPoint.SetActive(open);
        if (SessionOriginObj.activeInHierarchy != open)
            SessionOriginObj.SetActive(open);
        if (Session.gameObject.activeInHierarchy != open)
            Session.gameObject.SetActive(open);

        Session.Reset();
        GCCollect();
    }

    public void CloseAR_Objs()
    {
        for (int i = 0; i < m_ListArt.Count; i++)
        {
            if (m_ListArt[i] != null)
                m_ListArt[i].gameObject.SetActive(false);
        }

        ChangeCamera.Instance?.CloseAR_Plane();
        PlaceOnPlane.Instance?.DestroyPrefab();
    }

    public void InitAR_ObjsForPhoto()
    {
        for (int i = 0; i < PhotoBtns.Length; i++)
        {
            VideoBtns[i].SetActive(false);
        }

        PhotoBtns[0].SetActive(true);

        AR_Selecter.GoToPanel(0);
    }

    public void InitAR_ObjsForVideo()
    {
        for (int i = 0; i < VideoBtns.Length; i++)
        {
            PhotoBtns[i].SetActive(false);
        }

        VideoBtns[0].SetActive(true);

        AR_Selecter.GoToPanel(0);
    }

    public void FinishAR()
    {
        Destroy(SessionOriginObj.transform.Find("Trackbles"));
        // This function has to be implemented last.
        SessionOriginObj.SetActive(false);
    }

    /// <summary>
    /// Button Capture in Photo/Video panel.
    /// </summary>
    public void OpenAR()
    {
        OpenARKit(true);

        CapturePanel.SetActive(true);

        CaptureImage.texture = AR_CameraRender;
        InitAR_ObjsForPhoto();

        ChangeCamera.Instance.OpenUserCamera();

        CaptureImage.uvRect = mCam_Rect;
        PhotoVideoPanel.isPhoto = true;
        PhotoVideoPanel.AR_Camera = AR_Camera;
        PhotoVideoPanel.mediaCamera = AR_Camera;
        UIAni.InitCaptureMode();
        NatcoderManager.Instance.TargetCamera = AR_Camera;

        AR_Selecter.onPanelChanged.RemoveAllListeners();
        AR_Selecter.onPanelChanged.AddListener(AR_Swap);
    }

    public void CloseAR()
    {       
        CapturePanel.SetActive(false);
        CloseAR_Objs();
        ChangeCamera.Instance.OpenUserCamera();
        CaptureImage.texture = null;
        m_GetFaceTip.SetActive(false);
        OpenARKit(false);

#if UNITY_ANDROID
        StartCoroutine(ResetAR_Camera());
#endif

        Application.targetFrameRate = 60;
    }

    IEnumerator ResetAR_Camera()
    {
        ZeroPoint.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        m_ARCameraManager.requestedFacingDirection = CameraFacingDirection.World;
        yield return new WaitForSeconds(0.75f);
        ZeroPoint.SetActive(false);
    }

    public void GCCollect()
    {
        GC.Collect();

        for (int k = 0; k <= GC.MaxGeneration; k++)
        {
            GC.Collect(k);
            GC.WaitForPendingFinalizers();
        }
    }

    public void RendereChangeSettting(int index, float v)
    {
        if (FaceRenderer != null)
        {
            var mat = FaceRenderer.material;

            switch (index)
            {
                case 1:
                    FaceRenderer.material.mainTextureScale = new Vector2(v, mat.mainTextureScale.y);
                    break;
                case 2:
                    FaceRenderer.material.mainTextureScale = new Vector2(mat.mainTextureScale.x, v);
                    break;
                case 3:
                    FaceRenderer.material.mainTextureOffset = new Vector2(v, mat.mainTextureOffset.y);
                    break;
                case 4:
                    FaceRenderer.material.mainTextureOffset = new Vector2(mat.mainTextureOffset.x, v);
                    break;
            }

            Debug.Log($"RendereChange - index : {index} value : {v}");
        }
    }

    public void UpdateOBJScale()
    {
        if (Input.touchCount <= 1)
        {
            return;
        }
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        float offset = newDistance - oldDistance;

        float scaleFactor = offset / 800f;
        Vector3 localScale = mCacheObject.localScale;
        Vector3 scale = new Vector3(localScale.x + scaleFactor,
            localScale.y + scaleFactor,
            localScale.z + scaleFactor);
        if (scale.x >= 0.005f && scale.y >= 0.005f && scale.z >= 0.005f)
        {
            Debug.Log($"scale = {scale}");
            mCacheObject.localScale = scale;
        }

        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}