using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoSingleton<PlacementIndicator>
{
    private ARRaycastManager rayManager;
    [SerializeField]
    private GameObject visual;

    public bool isRay;

    void Start()
    {       
        visual.SetActive(false);
    }

    void Update()
    {
        if (isRay)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;

                if (!visual.activeInHierarchy)
                {
                    visual.SetActive(true);
                }
            }
        }
    }

    public void OpenVisual()
    {
        visual.SetActive(true);
    }

    public void SetRayManager()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        isRay = true;
    }

    public void ClearRayManager()
    {
        rayManager = null;
        isRay = false;
    }
}