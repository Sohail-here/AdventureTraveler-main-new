using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using FfmpegUnity;

/// <summary>
/// Don't modify this script unless UIManager in Login.scene is move on Canvas.
/// </summary>
public class UItest : MonoBehaviour
{
    private ARRaycastManager rayManager;
    [SerializeField]
    private GameObject visual;

    void Start()
    {      
        rayManager = FindObjectOfType<ARRaycastManager>();

        visual.SetActive(false);
    }

    void Update()
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