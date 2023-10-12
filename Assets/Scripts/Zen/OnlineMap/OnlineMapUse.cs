using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnlineMapUse : MonoBehaviour
{
    public static OnlineMapUse Instance;

    public OnlineMapUse()
    {
        Instance = this;
    }

    public Camera MapCamera;
    public RenderTexture mapImage;

    //public Text debugText;
    public TextMeshProUGUI distanceText;

    private OnlineMapsMarker originMarker;
    private OnlineMapsMarker realtimeMarker;

    private List<Vector2> route = new List<Vector2>();

    private float moveDistance;

    public delegate void PathRecordStartHandler(Vector2 startPosition);
    public event PathRecordStartHandler OnPathRecodeStart;
    public delegate void PathRecordEndHandler();
    public event PathRecordEndHandler OnPathRecodeEnd;

    #region Timer Parameter

    public TextMeshProUGUI timerText;

    //private int day;
    private int hour;
    private int minute;
    private int second;
    private float realTime;
    private bool isTiming;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;

        if (locationService == null)
        {
            Debug.LogError(
                "Location Service not found.\nAdd Location Service Component (Component / Infinity Code / Online Maps / Plugins / Location Service).");
            return;
        }

        locationService.OnLocationChanged += OnLocationChanged;

        moveDistance = 0;
        distanceText.text = moveDistance.ToString("0.0");
        isTiming = false;

        //day = 0;
        hour = 0;
        minute = 0;
        second = 0;

        locationService.OnLocationChanged += OnLocationChanged;
    }

    /*
    void OnDestroy()
    {
        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;
        locationService.OnLocationChanged -= OnLocationChanged;
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            realTime += Time.deltaTime;

            second = (int)realTime % 60;

            minute = (int)realTime / 60;

            hour = (int)realTime / 3600;

            if (minute == 60)
            {
                minute = 0;
            }

            timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
        }
    }

    /// <summary>
    /// Record Start
    /// </summary>
    public void RecordInit()
    {
        try
        {
            realtimeMarker = gameObject.GetComponent<OnlineMapsMarkerManager>().items[0];
            realtimeMarker.label = "realtimeMarker";

            originMarker = gameObject.GetComponent<OnlineMapsMarkerManager>().Create(realtimeMarker.position, null, "originMarker");

            route.Add(originMarker.position);
            route.Add(realtimeMarker.position);

            Debug.Log("Route Count-- " + route.Count);

            for (int i = 0; i < route.Count; i++)
            {
                Debug.Log(route[i] + " -- " + i);
            }

            //OnPathRecodeStart?.Invoke(originMarker.position);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return;
        }
    }

    private void OnLocationChanged(Vector2 position)
    {
        // Change the position of the marker.
        realtimeMarker.position = position; 
        route.Add(position);
        OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(route, Color.blue, 2));

        // Redraw map.
        OnlineMaps.instance.Redraw();

        if (isTiming)
        {
            moveDistance += 0.0062137119f;
            distanceText.text = moveDistance.ToString("00.00");
        }
    }

    private void ValueInit()
    {
        //day = 0;
        hour = 0;
        minute = 0;
        second = 0;

        realTime = 0;
        timerText.text = "00:00:00";

        moveDistance = 0;
        distanceText.text = "00.00";
    }

    public void RecordStart()
    {
        RecordInit();
        ValueInit();
        isTiming = true;
    }

    public void RecordStop()
    {
        isTiming = false;

        //UnDestroyData.journeyData.distance = distanceText.text;
        UnDestroyData.journeyData.duration = timerText.text;
        UnDestroyData.journeyData.name = Record.instance.ActivityLabel.GetComponent<TMP_Text>().text;

        ValueInit();

        for (int i = 0; i < gameObject.GetComponent<OnlineMapsMarkerManager>().items.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            gameObject.GetComponent<OnlineMapsMarkerManager>().items.RemoveAt(i);
        }

        Record.instance.Route = new List<Vector2>(route);

        OnlineMapsDrawingElementManager.RemoveAllItems();
        route.Clear();

        OnlineMaps.instance.Redraw();

        //OnPathRecodeEnd?.Invoke();
    }
}