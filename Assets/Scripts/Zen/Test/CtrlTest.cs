using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CtrlTest : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARPointCloud pointCloud;
    public ARPointCloudManager pointCloudManager;
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;
    public AR_Test arTest;

    public GameObject PlanePrefab;

    private bool isAR = true;

    public void SwitchAR_Status()
    {
        if (isAR)
        {
            planeManager.enabled = false;
            pointCloud.enabled = false;
            pointCloudManager.enabled = false;
            raycastManager.enabled = false;
            anchorManager.enabled = false;
            arTest.enabled = false;

            if (arTest.Instance.spawnedObj != null)
            {
                arTest.Instance.DestroySpawnPrefab();
            }

            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
        else
        {
            planeManager.enabled = true;
            pointCloud.enabled = true;
            pointCloudManager.enabled = true;
            raycastManager.enabled = true;
            anchorManager.enabled = true;
            arTest.enabled = true;

            planeManager.planePrefab = PlanePrefab;
            Debug.Log("SetPlanePrefab");

            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
        }

        isAR = !isAR;
    }
}
