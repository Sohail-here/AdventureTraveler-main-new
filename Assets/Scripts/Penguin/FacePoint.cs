using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FacePoint : MonoBehaviour
{
    public ARFace AFace;

    public Transform FaceRoot;
    public Transform TLPoint;
    public Transform CPoint;
    public Transform TRPoint;
    public Transform BLPoint;
    public Transform BRPoint;

    private TrackingState mCacheTrackStatus = TrackingState.None;

    private void Start()
    {
        if (PenguinDebugPanel.Instance != null)
        {
            PenguinDebugPanel.Instance.RegisterFaceSize(FaceRoot);
        }
    }

    private void Update()
    {
        if (AR_Controller.Instance != null)
        {
            if (mCacheTrackStatus != AFace.trackingState)
            {
                mCacheTrackStatus = AFace.trackingState;
                AR_Controller.Instance.FaceTipControl(AFace.trackingState == TrackingState.Tracking);
            }
            if (AFace.trackingState == TrackingState.Tracking)
            {
                AR_Controller.Instance.UpdateFacePos(this);

                var index = AR_Controller.Instance.SwapCounterIndex;

                switch (index)
                {
                    case 1:
                        FaceRoot.localScale = Vector3.one * 0.175f;
                        break;
                    case 2:
                        FaceRoot.localScale = Vector3.one * 0.276f;
                        break;
                    case 3:
                        FaceRoot.localScale = Vector3.one * 0.281f;
                        break;
                }
            }
        }
    }

    private void OnDisable()
    {
        AR_Controller.Instance.FaceTipControl(false);
    }
}
