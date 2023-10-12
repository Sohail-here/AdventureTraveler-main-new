using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoSingleton<PlaceOnPlane>
{
    public GameObject placePrefab;

    public GameObject PlanePrefab;
    public GameObject UnVisualPrefab;
    public GameObject SpawnedObject { get; private set; }

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public ARPlaneManager planeManager;

    protected override void Awake()
    {
        base.Awake();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool tryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;

            if (touchPosition.y > Screen.height / 5)
            {
                return true;
            }
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!tryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //Debug.Log("GetHitTouch!!!");

            var hitPose = hits[0].pose;

            planeManager.planePrefab = UnVisualPrefab;

            foreach (var planes in planeManager.trackables)
            {
                planes.gameObject.SetActive(false);
            }

            if (SpawnedObject == null)
            {
                SpawnedObject = Instantiate(placePrefab, hitPose.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                SpawnedObject.transform.position = hitPose.position;
            }
        }
    }

    public void SetPrefab(GameObject prefab)
    {
        placePrefab = prefab;
    }

    public void DestroyPrefab()
    {
        if (SpawnedObject != null)
        {
            Destroy(SpawnedObject);
        }
    }

    public void DestroyPlanes()
    {
        foreach (var planes in planeManager.trackables)
        {
            Destroy(planes.gameObject);
        }
    }
}