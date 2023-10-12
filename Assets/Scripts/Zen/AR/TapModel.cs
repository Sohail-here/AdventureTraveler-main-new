using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapModel : MonoBehaviour
{
    public GameObject AR_Panel;
    public GameObject ControllerPanel;

    private ARRaycastManager rayCastMgr;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // Start is called before the first frame update
    void Start()
    {
        rayCastMgr = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateAR_Panel();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        rayCastMgr.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdateAR_Panel()
    {
        if (placementPoseIsValid)
        {
            AR_Panel.SetActive(true);
            ControllerPanel.SetActive(true);
            AR_Panel.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            AR_Panel.SetActive(false);
            ControllerPanel.SetActive(false);
        }
    }  
}