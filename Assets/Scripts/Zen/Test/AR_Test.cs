using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DanielLochner.Assets.SimpleScrollSnap;

[RequireComponent(typeof(ARRaycastManager))]
public class AR_Test : MonoBehaviour
{
    public AR_Test Instance;

    public AR_Test()
    {
        Instance = this;
    }

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    [SerializeField]
    GameObject visualObj;

    public GameObject UnVisualObj;

    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject spawnedObj { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();

        if (placementUpdate == null)
        {
            placementUpdate = new UnityEvent();

            placementUpdate.AddListener(DisableVisual);
        }
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;          
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;

            foreach (var planes in gameObject.GetComponent<ARPlaneManager>().trackables)
            {
                planes.gameObject.SetActive(false);
            }

            gameObject.GetComponent<ARPlaneManager>().planePrefab = UnVisualObj;
            Debug.Log("Set UnVisualObj");

            if (spawnedObj == null)
            {
                spawnedObj = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObj.transform.position = hitPose.position;
            }

            placementUpdate.Invoke();
        }
    }

    public void DisableVisual()
    {
        visualObj.SetActive(false);
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;

    public void DestroySpawnPrefab()
    {
        Destroy(spawnedObj.gameObject);
    }
}