using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ChangeCamera : MonoSingleton<ChangeCamera>
{
    public ARSession Session;
    public ARCameraManager CameraManager;

    public ARFaceManager faceManager;
    public ARPlaneManager planeManager;
    //[SerializeField] private ARPointCloud pointCloud;
    //[SerializeField] private ARPointCloudManager pointCloudManager;
    public ARRaycastManager raycastManager;
    //[SerializeField] private ARAnchorManager anchorManager;
    public PlaceOnPlane placeOnPlane;

    private bool isUser = true;

    public void CameraSwitch()
    {
        Debug.Log("CameraSwitch");

        Session.Reset();

        isUser = !isUser;

        if (isUser)
        {
            OpenUserCamera();
        }
        else
        {
            OpenWorldCamera();
        }
    }

    public void OpenUserCamera()
    {
        isUser = true;

        try
        {
            CameraManager.requestedFacingDirection = CameraFacingDirection.User;
        }
        catch (System.Exception e)
        {
            ULog.Zen.Log("OpenUser-- " + e);
        }

        /// Closel AR planes
        planeManager.enabled = true;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        //faceManager.enabled = true;

        planeManager.enabled = false;
        //pointCloud.enabled = false;
        //pointCloudManager.enabled = false;
        raycastManager.enabled = false;
        //anchorManager.enabled = false;
        placeOnPlane.enabled = false;
    }

    public void OpenWorldCamera()
    {
        isUser = false;

        try
        {
            CameraManager.requestedFacingDirection = CameraFacingDirection.World;
        }
        catch (System.Exception e)
        {
            ULog.Zen.Log(e);
        }

        CloseComponents();

        if (AR_Controller.Instance.SwapCounterIndex == 0)
        {
            return;
        }

        OpenComponents();
    }

    void OpenComponents()
    {
        faceManager.enabled = false;

        planeManager.enabled = true;
        //pointCloud.enabled = true;
        //pointCloudManager.enabled = true;
        raycastManager.enabled = true;
        //anchorManager.enabled = true;
        placeOnPlane.enabled = true;
    }

    public void CloseComponents()
    {
        //faceManager.enabled = false;
        planeManager.enabled = false;
        //pointCloud.enabled = false;
        //pointCloudManager.enabled = false;
        raycastManager.enabled = false;
        //anchorManager.enabled = false;
        placeOnPlane.enabled = false;

        CloseAR_Plane();
    }

    public void CloseAR_Plane()
    {       
        try
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}