using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FfmpegUnity;
using TMPro;

public class Record : MonoSingleton<Record>
{
    public static Record instance;

    public Record()
    {
        instance = this;
    }

    public GameObject ActivityLabel;

    public GameObject JourneyDuration;
    public GameObject JourneyDistance;
    public GameObject Ffmpeg;
    public GameObject SchedulePanel;

    // 3D map thing & 2D map
    public OnlineMaps _2DMap;
    public OnlineMaps _3DMap;
    public Camera MapCamera;    

    private OnlineMapsMarker realtimeMarker;

    // 2D map route
    public List<Vector2> Route = new List<Vector2>();
    // 3D map route
    public List<Vector2> VideoRoute = new List<Vector2>();

    // Get time and videoname
    public string DateTime;
    //public string VideoName;

    // How many time do the video need to play
    [SerializeField]
    private float VideoTime;

    private FfmpegCommand ffmpeg;

    private void Start()
    {
        ffmpeg = Ffmpeg.GetComponent<FfmpegCaptureCommand>();
        UnDestroyData.journeyData.category = "Cycling";
    }

    // Load journey data from UI to database
    public void LoadJourneyData()
    {
        UnDestroyData.journeyData.duration = JourneyDuration.GetComponent<TMP_Text>().text;
        //UnDestroyData.journeyData.distance = JourneyDistance.GetComponent<TMP_Text>().text;
    }  

    /// <summary>
    /// Start record
    /// </summary>
    public void StartRecord()
    {
        OnlineMapUse.Instance.RecordStart();
        // Init 3D map
        _3DMap.SetPositionAndZoom(_2DMap.position.x, _2DMap.position.y, 13);
        Debug.Log(_2DMap.position.x);
        Debug.Log(_2DMap.position.y);
    }
  
    /// <summary>
    /// Stop record (Get GPS points mode)
    /// </summary>
    public void StopRecord()
    {
        VideoRoute.Clear();
        OnlineMapUse.Instance.RecordStop();

        // Open SchedulePanel to know the process schedule
        SchedulePanel.SetActive(true);
        // (Lagecy) If need anything about record, check this function.
        //StartMergeVideo();
    }  

    /*
    /// <summary>
    /// Stop record (Syncmap mode)
    /// </summary>
    public void StopRecord()
    {
        // Get cover
        ScreenShot.TakeCameraShot(MapCamera, new Rect(0, 0, Screen.width, Screen.height), Application.persistentDataPath + "/" + DateTime + "cover.png");

        Ffmpeg.GetComponent<FfmpegCommand>().StopFfmpeg();
        SchedulePanel.SetActive(true);
        Debug.Log(VideoName);
        MonoExpend.Instance.CheckFileRunDone(Application.persistentDataPath + "/" + VideoName, FinishVideo);
    }
    */

    /*
    public void RecordTest()
    {
        VideoRoute.Clear();
        _3DMap.GetComponent<OnlineMapsMarkerManager>().Create(Route[0], null, "originMarker");
        realtimeMarker = _3DMap.GetComponent<OnlineMapsMarkerManager>().Create(Route[0], null, "realtimeMarker");
        OnlineMapsDrawingElementManager.isMainMap = false;
        OnlineMapsDrawingElementManager.Init();
    }
    int count = 0;
    public void RecordTest1()
    {
        Debug.Log(Route[count]);
        VideoRoute.Add(Route[count]);
        realtimeMarker.position = VideoRoute[count];

        OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(VideoRoute, Color.blue, 50));

        _3DMap.Redraw();
        _3DMap.SetPositionAndZoom(VideoRoute[count].x, VideoRoute[count].y, 13);
        count++;
    }
    public void RecordTest2()
    {
        _3DMap.GetComponent<OnlineMapsMarkerManager>().items.Clear();

        OnlineMapsDrawingElementManager.RemoveAllItems();
        OnlineMapsDrawingElementManager.isMainMap = true;
        OnlineMapsDrawingElementManager.Init();
        OnlineMaps.instance.Redraw();
        count = 0;
    }
    */
    
    public void StartMergeVideo()
    {
        // Get time when video is finished
        DateTime = System.DateTime.UtcNow.ToString("ddMMyyyyHHmm");

        // Open 3D camera
        MapCamera.gameObject.SetActive(true);

        // Create marker
        _3DMap.GetComponent<OnlineMapsMarkerManager>().Create(Route[0], null, "originMarker");
        realtimeMarker = _3DMap.GetComponent<OnlineMapsMarkerManager>().Create(Route[0], null, "realtimeMarker");

        // Check if already get "DateTime" to start ffmpeg
        ffmpeg.StartFfmpeg();

        //Init 3D map
        OnlineMapsDrawingElementManager.isMainMap = false;
        OnlineMapsDrawingElementManager.Init();
        
        // Read first route
        VideoRoute.Add(Route[0]);
        // Start merge video
        StartCoroutine(Init3DMap());

        //StartCoroutine(DrawPath());
    }

    IEnumerator DrawPath()
    {
        Debug.Log("DrawPath");


        // How many points be played per second
        float pointsPerSecond = Route.Count / VideoTime;

        Debug.Log("Time-- " + VideoTime);
        Debug.Log(pointsPerSecond);
        Debug.Log(Route.Count);

        // Move with the route in 3D map
        for (int i = 1; i < Route.Count; i++)
        {
            VideoRoute.Add(Route[i]);
            Debug.Log(VideoRoute[i]);
            realtimeMarker.position = VideoRoute[i];

            // Open 3D line
            OnlineMapsDrawingLine line = new OnlineMapsDrawingLine(VideoRoute);
            line.followRelief = true;

            OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(line.points, Color.blue, 15));
            _3DMap.Redraw();
            //_3DMap.SetPositionAndZoom(VideoRoute[i].x, VideoRoute[i].y, 13);
            _3DMap.gameObject.GetComponent<SmoothMveExample>().PushToMove(pointsPerSecond, VideoRoute[i - 1], VideoRoute[i], 13);

            yield return new WaitUntil(() => !_3DMap.gameObject.GetComponent<SmoothMveExample>().isMovement);
        }
    }

    IEnumerator StartFfmpeg()
    {
        yield return StartCoroutine(DrawPath());
       
        //Ffmpeg.GetComponent<FfmpegUse>().FfmpegImageToVideo(Application.persistentDataPath + "/", "video", Application.persistentDataPath + "/", UIManager.instance.GetUUID() + DateTime);
        //VideoName = UIManager.instance.GetUUID() + DateTime;

        ScreenShot.TakeCameraShot(MapCamera, new Rect(0, 0, Screen.width, Screen.height), Application.persistentDataPath + "/" + DateTime + ".png");
    }

    IEnumerator Init3DMap()
    {
        //yield return StartCoroutine(StartFfmpeg());
        yield return StartCoroutine(DrawPath());
        // Get cover img
        ScreenShot.TakeCameraShot(MapCamera, new Rect(0, 0, Screen.width, Screen.height), Application.persistentDataPath + "/" + DateTime + ".png");

        Debug.Log("StopFfmpeg");
        ffmpeg.StopFfmpeg();

        MonoExpend.Instance.CheckFileRunDone(Application.persistentDataPath + "/" + DateTime + ".mp4", FinishVideo);
    }

    // Upload the files
    public void FinishVideo()
    {
        Debug.Log("FinishVideo");

        //urlNewJourney.newJourney.NewJourney();
        StartCoroutine(UploadMainFiles());       

        // Reset 3D map
        _3DMap.GetComponent<OnlineMapsMarkerManager>().items.Clear();
        OnlineMapsDrawingElementManager.RemoveAllItems();
        OnlineMapsDrawingElementManager.isMainMap = true;
        OnlineMapsDrawingElementManager.Init();
        OnlineMaps.instance.Redraw();
    }
   
    public void DeleteTemp()
    {
        /*
        for (int i = 0; i < ImageNums; i++)
        {
            System.IO.File.Delete(Application.persistentDataPath + "/" + "video" + i.ToString("00000") + ".png");
        }
        */
    }

    IEnumerator UploadMainFiles()
    {
        // Create journey data to get journey ID
        yield return StartCoroutine(urlNewJourney.newJourney.urlPOST_NewJourney());

        // Upload cover
        urlUserUpload.Instance.Filename = DateTime + ".png";
        yield return StartCoroutine(urlUserUpload.Instance.urlPost_UserUpload());
        // Upload main video
        urlUserUpload.Instance.Filename = DateTime + ".mp4";
        yield return StartCoroutine(urlUserUpload.Instance.urlPost_UserUpload());
    }
}