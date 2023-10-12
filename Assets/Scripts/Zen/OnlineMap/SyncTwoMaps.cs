using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncTwoMaps : MonoBehaviour
{
    private OnlineMaps mainMap;
    private OnlineMapsMarkerManager mainMarkManager;
    private OnlineMapsCameraOrbit mainMapsCameraOrbit;
    private OnlineMapUse mapUse;
    public RawImage mainMapImage;

    public GameObject secondMapObj;
    [SerializeField]
    private OnlineMaps secondMap;
    private OnlineMapsMarkerManager secondMarkManager;
    private OnlineMapsDrawingElementManager secondDrawingManager;
    private OnlineMapsCameraOrbit secondMapsCameraOrbit;
    private OnlineMapsMarker secondOriginMarker;
    private OnlineMapsMarker secondRealtimeMarker;
    private List<Vector2> route = new List<Vector2>();

    void Awake()
    {
        mainMap = GetComponent<OnlineMaps>();
        mainMarkManager = GetComponent<OnlineMapsMarkerManager>();
        mainMapsCameraOrbit = GetComponent<OnlineMapsCameraOrbit>();
        mapUse = GetComponent<OnlineMapUse>();

        secondMap = secondMapObj.GetComponent<OnlineMaps>();
        secondMarkManager = secondMapObj.GetComponent<OnlineMapsMarkerManager>();
        secondDrawingManager = secondMapObj.GetComponent<OnlineMapsDrawingElementManager>();
        secondMapsCameraOrbit = secondMapObj.GetComponent<OnlineMapsCameraOrbit>();

        secondMapsCameraOrbit.adjustTo = mainMapsCameraOrbit.adjustTo;
        secondMapsCameraOrbit.adjustToGameObject = mainMapsCameraOrbit.adjustToGameObject;
        secondMapsCameraOrbit.distance = mainMapsCameraOrbit.distance;
        secondMapsCameraOrbit.maxRotationX = mainMapsCameraOrbit.maxRotationX;
        secondMapsCameraOrbit.lockTilt = mainMapsCameraOrbit.lockTilt;
        secondMapsCameraOrbit.lockPan = mainMapsCameraOrbit.lockPan;
    }

    void OnEnable()
    {
        mainMap.OnChangePosition += syncPosition;
        mainMap.OnChangeZoom += syncZoom;
        mainMapsCameraOrbit.OnCameraControl += syncCameraOrbit;
        mapUse.OnPathRecodeStart += OnRecordStart;
        mapUse.OnPathRecodeEnd += OnRecordEnd;

        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;
        locationService.OnLocationChanged += OnLocationChanged;
    }

    void OnDisable()
    {
        mainMap.OnChangePosition -= syncPosition;
        mainMap.OnChangeZoom -= syncZoom;
        mainMapsCameraOrbit.OnCameraControl -= syncCameraOrbit;
        mapUse.OnPathRecodeStart -= OnRecordStart;
        mapUse.OnPathRecodeEnd -= OnRecordEnd;

        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;
        locationService.OnLocationChanged -= OnLocationChanged;
    }

    void Update()
    {
        // create marker in second map after main realtime marker was created in Start() function
        if (secondRealtimeMarker == null)
        {
            secondRealtimeMarker = secondMarkManager.Create(mainMarkManager.items[0].position, null, "realtimeMarker");
            secondRealtimeMarker.align = OnlineMapsAlign.Center;
        }
    }

    void syncPosition()
    {
        double lng, lat;
        mainMap.GetPosition(out lng, out lat);
        //secondMap.SetPositionAndZoom(lng, lat, mainMap.zoom);
        secondMap.SetPositionAndZoom(lng, lat, 13);
    }

    void syncZoom()
    {
        //secondMap.zoom = mainMap.zoom;
        secondMap.zoom = 13;
    }

    void syncCameraOrbit()
    {
        secondMapsCameraOrbit.rotation = mainMapsCameraOrbit.rotation;
        secondMapsCameraOrbit.speed = mainMapsCameraOrbit.speed;
    }

    void OnRecordStart(Vector2 startPosition)
    {
        secondOriginMarker = secondMarkManager.Create(startPosition, null, "originMarker");

        route.Add(secondOriginMarker.position);
        route.Add(secondRealtimeMarker.position);
    }

    void OnLocationChanged(Vector2 position)
    {
        // sync the position of the marker.
        secondRealtimeMarker.position = position;
        route.Add(position);

        secondDrawingManager.Add(new OnlineMapsDrawingLine(route, Color.blue, 2));

        // Redraw map.
        secondMap.Redraw();
    }

    void OnRecordEnd()
    {
        for (int i = 0; i < secondMarkManager.items.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            secondMarkManager.items.RemoveAt(i);
        }

        secondDrawingManager.RemoveAll();
        route.Clear();

        secondMap.Redraw();
    }

    public void SwitchMaps()
    {
        mainMapImage.enabled = !mainMapImage.enabled;
    }
}
