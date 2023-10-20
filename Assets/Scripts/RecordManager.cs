using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

using System.Text;
using System.Security.Cryptography;

public class RecordManager : MonoSingleton<RecordManager>
{
    public const string ES_RECORD_KEY = "RecordSaveing";
    public const string ES_RECORD_JID = "RecordJID";

    [SerializeField] private RecordUI recordUI;
    [SerializeField] private RecordJourneyShortTip m_ShortTipPage;
    [SerializeField] private GameObject m_SaveJourneyPage;
    [SerializeField] private Button m_BtnFinishJourney;
    [SerializeField] private GameObject m_TrackingJourneyPage;
    [SerializeField] private LoadingSchedule m_UploadPage;
    [SerializeField] private RecordFailPage m_FailPage;
    [SerializeField] private Camera m_MapCamera;
    [SerializeField] private ContinueJourney m_ContinueJourneyPage;
    [SerializeField] private List<Texture2D> m_MapPinIcon;
    [SerializeField] private Button m_ShareBtn;
    [SerializeField] private RecordShareLocationUI m_ShareUI;
    [SerializeField] private RawImage m_MapRawImage;
    [SerializeField] private LiveShareJourney m_LiveShareJourney;

    public OnlineMaps Maps;
    public OnlineMapsLocationService GPSLocationService;
    public bool IsQuickTest = false;
    private LocationInfo startLocalInfo;
    private OnlineMapsGPXObject gpxObject;
    private List<Vector2> cachePoints = new List<Vector2>();
    private OnlineMapsGPXObject.TrackSegment trackSegment = new OnlineMapsGPXObject.TrackSegment();
    private bool isLocationInitedDone = false;
    private double distance = 0;
    private string totalTime = "";
    private List<string> mUploadFile = new List<string>();
    private List<string> mUploadFileNames = new List<string>();
    private string tempPath;
    private bool mIsPause = false;
    private bool mRecording = false;
    private Color mPathColor;
    private double mCheckSaveDis = 0.05f;
    private double mCheckSaveDB = 0.1f;
    private RecordUpdateData mSDB = new RecordUpdateData();
    private RecordSQLData mRecordDBData;
    private Action mOnContinue;
    private bool isContinueJourney = false;
    private string mJourneyID = "";
    private string mShareCode = "";
    private OnlineMapsMarkerManager mOnlineMapsMarkerManager;
    private List<Vector2> mPhotoMapPoint = new List<Vector2>();
    private Vector2 mLastPoint = Vector2.zero;
    private bool mSaveBG = false;
    //public const string API_PATH = "https://localhost:44370/API";
    //public const string API_PATH = "http://i911record.ddns.net/API";
    //public const string API_PATH = "http://ec2-52-14-232-110.us-east-2.compute.amazonaws.com/URecordAPI/API";
    public const string API_PATH = "http://18.219.108.69/rest.php";
    const int COMMAND_TRY_MAX_COUNT = 5;
    int mCmdTryCount = 0;
    protected override void Awake()
    {
        base.Awake();

        GPSLocationService.OnLocationInited += () =>
        {
            isLocationInitedDone = true;
            mOnContinue?.Invoke();
        };

        UnDestroyData.journeyData.category = "Cycling";

        ColorUtility.TryParseHtmlString("#00AEEF", out mPathColor);
    }

    private void Start()
    {
        ULog.Penguin.Log("PGIN Record V1.01 202207101200");
        #region Is PenguinDebug
#if UNITY_EDITOR
        UnDestroyData.userData.user_name = "Penguin";
        UnDestroyData.userData.email = "cutesamllpgin@gmail.com";
#endif
        #endregion
        mSDB = new RecordUpdateData();
        m_BtnFinishJourney.onClick.AddListener(FinishJourneyCheck);
        m_ShareBtn.onClick.AddListener(ShareFriend);
        GPSLocationService.OnLocationChanged = (vec2) =>
        {
#if UNITY_EDITOR
#else
            if(GPSLocationService.IsLocationServiceRunning() == false)
                return;
#endif

            if (mIsPause)
                return;

            if (mRecording == false)
                return;

            int index = trackSegment.points.Count;
            var waypoint = new OnlineMapsGPXObject.Waypoint(vec2.x, vec2.y)
            {
                name = $"Point{index:0000}",
                time = DateTime.UtcNow, //UnixTimeStampToDateTime(startLocalInfo.timestamp),
            };

            trackSegment.points.Add(waypoint);
            //ULog.Penguin.Log($"trackSegment: ---- \n {gpxObject.tracks[0].segments[0].points.Count}");
            //Update distance
            distance += GPSLocationService.distance;                        //m
            recordUI.UpdateMiles((distance * 0.62137f).ToString("0.00"));   // 1,609.344 m => 1 mi 
            UpdateMapDrawItem(vec2);

            if (string.IsNullOrEmpty(mShareCode) == false && distance > mCheckSaveDB)
            {
                mCheckSaveDB = distance + 0.1f;
                SaveRecordData(true);
            }
            //else 
            //{
            //    ULog.Penguin.Log($"{string.IsNullOrEmpty(mShareCode)} : {distance} - {mCheckSaveDB}");
            //}

            if (distance > mCheckSaveDis)
            {
                mCheckSaveDis = distance + 0.01f;
                var json = GetRecordSQLDataJson("C");
                ES3.Save(ES_RECORD_KEY, json);
            }

        };

        //Inited Done Start Record
        //For Editor Quick Test
        Maps.OnChangePosition += () =>
        {
            if (mRecording == false)
                return;

            if (IsQuickTest)
            {
                var mapPos = new Vector2(Maps.position.x, Maps.position.y);
                GPSLocationService.emulatorPosition = mapPos;
                //ULog.Penguin.Log("Add Maps.position : " + Maps.position);
            }
        };

        //if (ES3.KeyExists(ES_RECORD_KEY))
        //{
        //    var root = JsonConvert.DeserializeObject<Root>(JsonConvert.DeserializeObject<string>(ES3.Load<string>(ES_RECORD_KEY)));
        //    if (root != null && root.Datas.Count > 0)
        //    {
        //        mRecordDBData = root.Datas[0];
        //        m_ContinueJourneyPage.OpenPage(ContinueJourney, DelSQLJourney);
        //    }
        //}
        DelJourney();
    }

    /*string POSTAPI(string apiCMD, string sqlCMD, Method method = Method.POST)
    {
        RestClient client = new RestClient(API_PATH);
        RestRequest request = new RestRequest($"Values/{apiCMD}", method);
        request.AddJsonBody(sqlCMD);
        var response = client.Execute(request);
        Debug.LogWarning($"apiCMD:{apiCMD}\nsqlCMD:{sqlCMD}\nStatusCode:{response.StatusCode}\nContent:{response.Content}");
        return response.Content != null && string.IsNullOrEmpty(response.Content) == false ? JsonConvert.DeserializeObject<string>(response.Content) : string.Empty;
    }*/
    string POSTAPI(string apiCMD, string sqlCMD, Method method = Method.POST)
    {



        RestClient client = new RestClient(API_PATH);
        RestRequest request = new RestRequest($"Values/{apiCMD}", method);



        // Set content type to x-www-form-urlencoded
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");



        // Add parameters to the body
        request.AddParameter("sqlCMD", sqlCMD);
        request.AddParameter("fingerprint", GenerateFingerprint());



        // Execute the request
        var response = client.Execute(request);



        // Debugging and error handling
        Debug.LogWarning($"apiCMD:{apiCMD}\nsqlCMD:{sqlCMD}\nStatusCode:{response.StatusCode}\nContent:{response.Content}");



        // Deserialize the response content
        return response.Content != null && string.IsNullOrEmpty(response.Content) == false ? JsonConvert.DeserializeObject<string>(response.Content) : string.Empty;



        /*
        RestClient client = new RestClient(API_PATH);
        RestRequest request = new RestRequest($"Values/{apiCMD}", method);
        request.AddJsonBody(sqlCMD);
        var response = client.Execute(request);
        Debug.LogWarning($"apiCMD:{apiCMD}\nsqlCMD:{sqlCMD}\nStatusCode:{response.StatusCode}\nContent:{response.Content}");
        return response.Content != null && string.IsNullOrEmpty(response.Content) == false ? JsonConvert.DeserializeObject<string>(response.Content) : string.Empty;
        */
    }



    public string GenerateFingerprint()
    {
        string secretKey = "IMIEMWORN8882MMFMKAMDSKMK";
        long epochSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long hourlyEpoch = epochSeconds / 3600;



        using MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(secretKey + hourlyEpoch.ToString()));



        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
        {
            sb.Append(b.ToString("x2"));
        }



        return sb.ToString();
    }

    public void ReView(RecordSQLData recordData)
    {
        DeleteAllPathAndMarker();
        UnDestroyData.journeyData.category = recordData.RecordType;
        distance = double.Parse(recordData.Miles);
        recordUI.UpdateMiles((distance * 0.62137f).ToString("0.00"));   // 1,609.344 m => 1 mi 
        totalTime = recordData.TotalTime;
        recordUI.AddTime(recordData.TotalTime);
        XmlDocument doc = new XmlDocument();
        ULog.Penguin.Log(recordData.GPX);
        doc = JsonConvert.DeserializeXmlNode(recordData.GPX);
        ULog.Penguin.Log(doc.OuterXml);
        mShareCode = recordData.FriendShareID;
        ULog.Penguin.Log($"ShareID {mShareCode}");
        gpxObject = OnlineMapsGPXObject.Load(doc.OuterXml);
        cachePoints.Clear();
        Vector2 mappos = new Vector2();
        if (gpxObject.tracks.Count > 0 && gpxObject.tracks[0].segments.Count > 0)
        {
            foreach (var item in gpxObject.tracks[0].segments[0].points)
            {
                mappos = new Vector2(float.Parse(item.lon.ToString()), float.Parse(item.lat.ToString()));
                UpdateMapDrawItem(mappos);
            }

            trackSegment = gpxObject.tracks[0].segments[0];
        }

        Maps.SetPositionAndZoom(mappos.x, mappos.y, 16);
        GPSLocationService.updatePosition = true;
        if (string.IsNullOrEmpty(recordData.UploadFiles) == false)
        {
            var datas = recordData.UploadFiles.Split('&');
            foreach (var dataStr in datas)
            {
                var data = dataStr.Split('|');
                var dir = data[0];  //image or video
                var iconStr = data[1];
                var fileName = data[2];
                var vec2Str = data[3];
                var vec2 = vec2Str.Split(':');
                Vector2 pos = new Vector2(float.Parse(vec2[0]), float.Parse(vec2[1]));

                UploadFile(iconStr, fileName, pos);
            }
        }
    }

    void ContinueJourney()
    {
        isContinueJourney = true;
        StartCoroutine(Co_ContinueJourney());
    }

    IEnumerator Co_ContinueJourney()
    {
        yield return isLocationInitedDone;

        ULog.Penguin.Log("RecordContinue ");
        StartRecord();
        m_TrackingJourneyPage.SetActive(true);
        if (ES3.KeyExists(ES_RECORD_JID))
            mJourneyID = ES3.Load<string>(ES_RECORD_JID);
        ULog.Penguin.Log($"RecordContinue Get ES_RECORD_JID = {mJourneyID}");

        UnDestroyData.journeyData.category = mRecordDBData.RecordType;
        distance = double.Parse(mRecordDBData.Miles);
        recordUI.UpdateMiles((distance * 0.62137f).ToString("0.00"));   // 1,609.344 m => 1 mi 
        totalTime = mRecordDBData.TotalTime;
        recordUI.AddTime(mRecordDBData.TotalTime);
        XmlDocument doc = new XmlDocument();
        ULog.Penguin.Log(mRecordDBData.GPX);
        doc = JsonConvert.DeserializeXmlNode(mRecordDBData.GPX);
        ULog.Penguin.Log(doc.OuterXml);
        mShareCode = mRecordDBData.FriendShareID;
        ULog.Penguin.Log($"ShareID {mShareCode}");
        gpxObject = OnlineMapsGPXObject.Load(doc.OuterXml);
        cachePoints.Clear();

        if (gpxObject.tracks.Count > 0 && gpxObject.tracks[0].segments.Count > 0)
        {
            foreach (var item in gpxObject.tracks[0].segments[0].points)
            {
                UpdateMapDrawItem(new Vector2(float.Parse(item.lon.ToString()), float.Parse(item.lat.ToString())));
            }

            trackSegment = gpxObject.tracks[0].segments[0];
        }
        if (string.IsNullOrEmpty(mRecordDBData.UploadFiles) == false)
        {
            var datas = mRecordDBData.UploadFiles.Split('&');
            foreach (var dataStr in datas)
            {
                var data = dataStr.Split('|');
                var dir = data[0];  //image or video
                var iconStr = data[1];
                var fileName = data[2];
                var vec2Str = data[3];
                var vec2 = vec2Str.Split(':');
                Vector2 pos = new Vector2(float.Parse(vec2[0]), float.Parse(vec2[1]));

                UploadFile(iconStr, fileName, pos);
            }
        }
        ULog.Penguin.Log("cachePoints " + cachePoints.Count);
    }

    void DelSQLJourney()
    {
        if (ES3.KeyExists(ES_RECORD_JID))
        {
            mJourneyID = ES3.Load<string>(ES_RECORD_JID);
            if (string.IsNullOrEmpty(mJourneyID) == false)
            {
                DelJourney();
                //Reset Journey Page
                Home.Instance?.ClearAllJourney();
                Home.Instance?.Refresh();
            }
        }
        else if (ES3.KeyExists(ES_RECORD_KEY))
        {
            ES3.DeleteKey(ES_RECORD_KEY);
        }
    }

    async void DelJourney()
    {
        ULog.Penguin.Log($"DelJourney --> {mJourneyID}");
        var client = new RestClient("https://api.i911adventure.com");
        var request = new RestRequest($"/journey/{mJourneyID}/", Method.DELETE);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
        var response = await client.ExecuteAsync(request);
        Debug.Log("-------------- response" + response + "StatusCode-----" + response.StatusCode+"JOUrneyID"+ mJourneyID);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            mCmdTryCount = 0;
            new Thread(() =>
            {
                ES3.DeleteKey(ES_RECORD_KEY);
                ES3.DeleteKey(ES_RECORD_JID);
                mJourneyID = string.Empty;
                mShareCode = String.Empty;
                POSTAPI("", UnDestroyData.userData.email, Method.DELETE);
            }).Start();
            ULog.Penguin.Log($"DelJourney - {response.StatusCode} : {response.Content}");
        }
        else
        {
            mCmdTryCount++;
            if (mCmdTryCount > COMMAND_TRY_MAX_COUNT)
            {
                DelJourney();
            }
            else
            {
                ULog.Penguin.LogError($"DelJourney Try Cmd to MAX");
            }
        }
    }

    void FinishJourneyCheck()
    {
        //m_SaveJourneyPage.SetActive(true);
        if (trackSegment.points.Count < 5 || distance <= 0.05f)
        {
            ULog.Penguin.Log($"FinishJourney Fail");
            mIsPause = true;
            m_ShortTipPage.OpenUI(() => { mIsPause = false; }, () => { CancelRecord(); m_TrackingJourneyPage.SetActive(false); });
        }
        else
        {
            ULog.Penguin.Log($"FinishJourney OK!!");
            m_SaveJourneyPage.SetActive(true);
        }
    }

    public void PauseRecord(bool isPause)
    {
        CancelInvoke("BG_SaveRecord");
        mIsPause = isPause;
        m_MapRawImage.gameObject.SetActive(!isPause);
        Maps.gameObject.SetActive(!isPause);
        m_MapCamera.gameObject.SetActive(!isPause);

        if (mSaveBG && isPause == false)
        {
            mSaveBG = false;
            foreach (var point in mPhotoMapPoint)
            {
                int index = trackSegment.points.Count;
                var waypoint = new OnlineMapsGPXObject.Waypoint(point.x, point.y)
                {
                    name = $"Point{index:0000}",
                    time = DateTime.UtcNow, //UnixTimeStampToDateTime(startLocalInfo.timestamp),
                };

                trackSegment.points.Add(waypoint);

                UpdateMapDrawItem(point);
            }

            mPhotoMapPoint.Clear();

        }
        else if (isPause)
        {
            InvokeRepeating("BG_SaveRecord", 0, 1f);
        }

    }

    private void BG_SaveRecord()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            var pos = new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);
            if (pos != mLastPoint)
            {
                mSaveBG = true;
                //pppos = new Vector2(pppos.x, pppos.y + 0.001f);
                mLastPoint = pos;
                ULog.Penguin.Log($"Add Cache Point {mLastPoint}");
                cachePoints.Add(mLastPoint);
            }
        }
    }

    [ContextMenu("StartRecord")]
    public void StartRecord()
    {
        try
        {
            if (isLocationInitedDone
#if UNITY_EDITOR
#else
            || GPSLocationService.IsLocationServiceRunning()
#endif
            )
            {
                Maps.SetPositionAndZoom(GPSLocationService.position.x, GPSLocationService.position.y, 16);
                GPSLocationService.restoreAfter = 10;
                distance = 0;
                mIsPause = false;
                mUploadFile.Clear();
                mUploadFileNames.Clear();
                trackSegment.points.Clear();
                cachePoints.Clear();
                mCheckSaveDB = 0.1f;
                mCheckSaveDis = 0.05f;

#if UNITY_EDITOR
                IsQuickTest = true;
#endif

                recordUI.StartRecoad();
                CreateOrLoadGpx();

                mRecording = true;
                ULog.Penguin.Log($"GPS Run isLocationInitedDone = {isLocationInitedDone} GPSLocationService.IsLocationServiceRunning() = {GPSLocationService.IsLocationServiceRunning()}");
            }
            else
            {
                ULog.Penguin.Log($"GPS Fail isLocationInitedDone = {isLocationInitedDone} GPSLocationService.IsLocationServiceRunning() = {GPSLocationService.IsLocationServiceRunning()}");
            }
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log(ex.ToString());
        }
    }

    public void CancelRecord()
    {
        ULog.Penguin.Log("CancelRecord");
        m_FailPage.ClosePage();
        mRecording = false;
        IsQuickTest = false;
        recordUI.UpdateMiles("0.00");
        trackSegment.points.Clear();
        cachePoints.Clear();
        recordUI.StopInvoke();
        DeleteAllPathAndMarker();
        DelSQLJourney();
        m_LiveShareJourney.Close();
        GPSLocationService.restoreAfter = 0;
    }

    public void StopRecord()
    {
        ULog.Penguin.Log($"StopRecord");
        //m_UploadPage.gameObject.SetActive(true);
        mRecording = false;
        IsQuickTest = false;
        SendServer();
        recordUI.StopInvoke();
    }

    public void UploadFile(string icon, string fileName, Vector2 pos)
    {
        bool mapActiveCache = Maps.gameObject.activeInHierarchy;
        Maps.gameObject.SetActive(true);
        string dirName = fileName.Contains("mp4") ? "video" : "image";

#if UNITY_EDITOR_OSX
        string[] words = UIManager.Instance.Filename.Split('/');
        fileName = words[words.Length - 1];
#elif UNITY_IPHONE
        string[] words = UIManager.Instance.Filename.Split('/');
        fileName = words[words.Length - 1];
#endif
        mUploadFile.Add($"{dirName}|{icon}|{fileName}|{pos.x}:{pos.y}");
        mUploadFileNames.Add(fileName);
        var waypoint = new OnlineMapsGPXObject.Waypoint(pos.x, pos.y)
        {
            time = DateTime.UtcNow,     //UnixTimeStampToDateTime(startLocalInfo.timestamp),
        };

        var pinIcon = m_MapPinIcon.Find(img => img.name == icon);
        var marker = OnlineMapsMarkerManager.CreateItem(pos, pinIcon);

        marker.scale = 0.5f;

        trackSegment.points.Add(waypoint);

        Maps.gameObject.SetActive(mapActiveCache);
        ULog.Penguin.Log("UploadFile-- " + icon + " -- " + fileName);
    }

    public void UploadFile(string icon, string fileName)
    {
        UploadFile(icon, fileName, GPSLocationService.position);
    }

    public void ShareFriend()
    {
        try
        {
            ULog.Penguin.Log($"ShareFriend {mShareCode}");
            if (string.IsNullOrEmpty(mShareCode))
            {
                ULog.Penguin.Log($"Send Get Code");
                RestClient client = new RestClient(API_PATH);
                RestRequest request = new RestRequest("Values/GetShareCode", Method.GET);
                var response = client.Execute(request);
                ULog.Penguin.Log($"{response.StatusCode} : {response.Content}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ULog.Penguin.Log(response.Content);
                    mShareCode = response.Content.Replace("\"", "");

                    if (string.IsNullOrEmpty(mShareCode) == false)
                    {
                        var json = GetRecordSQLDataJson("C");
                        ES3.Save(ES_RECORD_KEY, json);
                        if (SendRecordToDB(json))
                        {
                            m_ShareUI.Open(mShareCode);
                        }
                        else
                        {
                            ULog.Penguin.Log($"Send DB Fail");
                        }
                    }
                }
            }
            else
            {
                m_ShareUI.Open(mShareCode);
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    void SendServer()
    {
        try
        {
            m_UploadPage.LoadingBar.SetActive(true);
            WaitReordVideo();
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log($"==== Record Error ====\n{ex.ToString()}");
        }
    }

    public bool SaveBuildRecordData()
    {
        try
        {
            var json = GetRecordSQLDataJson("B");

            ES3.Save(ES_RECORD_KEY, json);

            var client = new RestClient(API_PATH);
            var request = new RestRequest($"Values", Method.POST);
            request.AddJsonBody(json);
            var response = client.ExecuteAsync(request);
            response.Wait();
            ULog.Penguin.Log($"SaveBuildRecordData To DB\nStatusCode:{response.Result.StatusCode}\nContent:{response.Result.Content}");
            ULog.Penguin.Log($"sqlCMD:{json}");
            return response.Result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log(ex.ToString());
        }

        return false;
    }

    void SaveRecordData(bool sendDB = false)
    {
        try
        {
            var json = GetRecordSQLDataJson("C");
            ES3.Save(ES_RECORD_KEY, json);
            if (sendDB)
            {
                var t = new Timer((obj) =>
                {
                    SendRecordToDB(json);
                    GC.Collect();
                }, null, 0, 0);
            }
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log(ex.ToString());
        }
    }

    bool SendRecordToDB(string json)
    {
        try
        {
            Debug.Log($"Send DB Test");
            var client = new RestClient(API_PATH);
            var request = new RestRequest($"Values", Method.POST);
            request.AddJsonBody(json);
            var response = client.Execute(request);
            Debug.Log($"SendToDB - \nStatusCode:{response.StatusCode}\nContent:{response.Content}\nsqlCMD:{json}");
            return response.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log(ex.ToString());
        }
        return false;
    }

    string GetRecordSQLDataJson(string status)
    {
        RefreshRecordUpdateData();
        var root = new Root();
        root.Datas = new List<RecordSQLData>();
        string journeyIDStr = string.IsNullOrEmpty(mSDB.journeyid) ? UnDestroyData.userData.email : mSDB.journeyid;
        string whereKey = journeyIDStr;
        var recordData = new RecordSQLData()
        {
            JourneyID = journeyIDStr,
            GPX = mSDB.gpx,
            Email = mSDB.email,
            Miles = mSDB.miles,
            TotalTime = mSDB.totalTime,
            UploadFiles = mSDB.uploadFiles,
            Status = status,
            StatusMsg = whereKey,
            RecordType = mSDB.recordType,
            CreateTime = mSDB.nowTime,
            UserName = UnDestroyData.userData.user_name,
            UserToken = mSDB.userToken,
            FriendShareID = status == "B" ? "" : mShareCode,
        };

        root.Datas.Add(recordData);
        var json = JsonConvert.SerializeObject(root);
        json = JsonConvert.SerializeObject(json);
        return json;
    }

    void UpdateMapDrawItem(Vector2 pos)
    {
        cachePoints.Add(pos);
        OnlineMapsDrawingElementManager.RemoveAllItems();
        OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(cachePoints, mPathColor, 2f));
    }

    void WaitReordVideo()
    {
        ULog.Penguin.Log($"Check Key : {ES3.KeyExists(ES_RECORD_JID)} | mJourneyID : {mJourneyID}");
        if (ES3.KeyExists(ES_RECORD_JID) == false || string.IsNullOrEmpty(mJourneyID))
        {
            var client = new RestClient("https://api.i911adventure.com");
            var request = new RestRequest($"/new_journey/", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "JWT " + UnDestroyData.token);
            var jsonData = new UnDestroyData.JourneyData()
            {
                category = UnDestroyData.journeyData.category,
                distance = double.Parse(distance.ToString("0.00")),
                duration = totalTime,
                name = UnDestroyData.userData.user_name
            };

            string jsonStringResult = JsonConvert.SerializeObject(jsonData);
            request.AddJsonBody(jsonStringResult);
            var response = client.Execute(request);
            ULog.Penguin.Log($"response {response.StatusCode} -- {response.Content}");
            if (response.Content != null && response.StatusCode == HttpStatusCode.Created)
            {
                var newJourneyData = JsonConvert.DeserializeObject<GetJourneyData>(response.Content);
                mJourneyID = newJourneyData.journey_id;
                ES3.Save<string>(ES_RECORD_JID, mJourneyID);
                ULog.Penguin.Log($"ES_RECORD_JID Save ID = {ES3.Load<string>(ES_RECORD_JID)}");
            }
            else
            {
                m_FailPage.OpenPage("Create Journey Fail", "Failed to create video multiple times", SendServer, CancelRecord, "TryAgain");
                ULog.Penguin.Log($"Create Journey Fail {response.StatusCode} -- {response.Content}");
            }
        }

        //ReCheck
        if (ES3.KeyExists(ES_RECORD_JID) == false || string.IsNullOrEmpty(mJourneyID))
        {
            m_FailPage.OpenPage("Create Journey Fail", "Failed to create video multiple times", WaitReordVideo, CancelRecord, "TryAgain");
        }
        else
        {
            UploadAllFile();
        }
    }

    void UploadAllFile()
    {
        m_UploadPage.StartUploadAllFile(mUploadFileNames, mJourneyID, UploadDone, OnUploadFileFail);
    }

    void OnUploadFileFail(string errorStr)
    {
        m_UploadPage.ClosePage();
        m_FailPage.OpenPage($"UpLoadFile Fail", $"File Name {errorStr} Failed to create video multiple times", UploadAllFile, DeleteJourney, "TryAgain");
    }

    void UploadDone()
    {
        var journeyMsg = new UnDestroyData.JourneyMsg();
        journeyMsg.distance = distance.ToString("00.00");
        journeyMsg.duration = totalTime;
        journeyMsg.category = UnDestroyData.journeyData.category;

        Home.Instance.VideoDetailItem.ShowDetail(ref journeyMsg);

        ClearCacheData();
        //try
        //{
        //    //ResetJourney
        //    Home.Instance.ClearAllJourney();
        //    Home.Instance.Refresh();
        //    Debug.Log($"Wait Record Video Make!");

        //    //if (SaveBuildRecordData() == false)
        //    //{
        //    //    m_FailPage.OpenPage($"SEND RECORD DATA ERROR", "Send RecordData to SQL Fail", UploadDone, DeleteJourney, "TryAgain");
        //    //    return;
        //    //}

        //    var journeyMsg = new UnDestroyData.JourneyMsg();
        //    journeyMsg.distance = distance.ToString("00.00");
        //    journeyMsg.duration = totalTime;
        //    journeyMsg.category = UnDestroyData.journeyData.category;

        //    var data = new RecordUpdateData()
        //    {
        //        journeyid = mJourneyID,
        //        email = UnDestroyData.userData.email,
        //        gpx = JsonConvert.SerializeXmlNode(gpxObject.ToXML().element),
        //        recordType = UnDestroyData.journeyData.category,
        //        miles = distance.ToString("00.00"),
        //        nowTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
        //        totalTime = totalTime,
        //        uploadFiles = string.Join("&", mUploadFile),
        //        userToken = UnDestroyData.token,
        //    };

        //    ClearCacheData();
        //    //ULog.Penguin.Log($"data {data.totalTime} - {totalTime}");
        //    var client = new RestClient("http://mipipi.ddns.net:8090/");
        //    var request = new RestRequest(@"BuildRecordII/", Method.GET);
        //    request.AddHeader("Content-Type", "application/json");
        //    var jsonStringResult = JsonConvert.SerializeObject(data);
        //    Debug.Log("jsonStringResult:" + jsonStringResult);
        //    request.AddJsonBody(jsonStringResult);
        //    //ULog.Penguin.Log(JsonUtility.ToJson(data));
        //    var response = await client.ExecuteAsync(request);
        //    ULog.Penguin.Log($"Send Build Cmd {response.StatusCode} response:{response.Content}");
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        m_UploadPage.ClosePage();
        //        ULog.Penguin.Log($"StartMakeRecord -- OK");
        //        ClearCacheData();
        //    }
        //    else
        //    {
        //        m_UploadPage.ClosePage();
        //        ULog.Penguin.Log("Send Build Cmd Error");
        //        m_FailPage.OpenPage($"UpLoadFile Fail", "Upload Make Record Data to Server is Fail", UploadDone, DeleteJourney, "TryAgain");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    m_UploadPage.ClosePage();
        //    m_FailPage.OpenPage($"UpLoadFile Exception", ex.ToString(), UploadDone, DeleteJourney, "TryAgain");
        //    ULog.Penguin.Log(ex.ToString());
        //}
    }

    public bool CallBuildRecord()
    {
        try
        {
            //ResetJourney
            Home.Instance.ClearAllJourney();
            Home.Instance.Refresh();
            Debug.Log($"Wait Record Video Make!");

            //if (SaveBuildRecordData() == false)
            //{
            //    m_FailPage.OpenPage($"SEND RECORD DATA ERROR", "Send RecordData to SQL Fail", UploadDone, DeleteJourney, "TryAgain");
            //    return;
            //}

            //var journeyMsg = new UnDestroyData.JourneyMsg();
            //journeyMsg.distance = distance.ToString("00.00");
            //journeyMsg.duration = totalTime;
            //journeyMsg.category = UnDestroyData.journeyData.category;

            //var data = new RecordUpdateData()
            //{
            //    journeyid = mJourneyID,
            //    email = UnDestroyData.userData.email,
            //    gpx = JsonConvert.SerializeXmlNode(gpxObject.ToXML().element),
            //    recordType = UnDestroyData.journeyData.category,
            //    miles = distance.ToString("00.00"),
            //    nowTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
            //    totalTime = totalTime,
            //    uploadFiles = string.Join("&", mUploadFile),
            //    userToken = UnDestroyData.token,
            //};

            //ULog.Penguin.Log($"data {data.totalTime} - {totalTime}");
            var client = new RestClient("http://mipipi.ddns.net:8090/");
            var request = new RestRequest(@"BuildRecordII/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //var jsonStringResult = JsonConvert.SerializeObject(data);
            //Debug.Log("jsonStringResult:" + jsonStringResult);
            //request.AddJsonBody(jsonStringResult);
            ////ULog.Penguin.Log(JsonUtility.ToJson(data));
            var response = client.Execute(request);
            ULog.Penguin.Log($"----------Send Build Cmd {response} response:{response.StatusCode}");
            return true;
        }
        catch (Exception ex)
        {
            ULog.Penguin.Log("--------------"+ex.ToString());
        }
        return false;
    }

    private void ClearCacheData()
    {
        ULog.Penguin.Log($"Record Clear Done");
        mJourneyID = string.Empty;
        ES3.DeleteKey(ES_RECORD_JID);
        ES3.DeleteKey(ES_RECORD_KEY);
    }

    private void DeleteJourney()
    {
        m_UploadPage.ClosePage();
        CancelRecord();
    }

    private void CreateOrLoadGpx()
    {
        gpxObject = new OnlineMapsGPXObject("I911-Record");

        // Creates a meta.
        OnlineMapsGPXObject.Meta meta = gpxObject.metadata = new OnlineMapsGPXObject.Meta();
        meta.author = new OnlineMapsGPXObject.Person
        {
            email = new OnlineMapsGPXObject.EMail("support", "i911.com"),
            name = "I911 Code"
        };

        // Creates a bounds
        meta.bounds = new OnlineMapsGPXObject.Bounds(30, 10, 40, 20);

        // Creates a copyright
        meta.copyright = new OnlineMapsGPXObject.Copyright("I911 Code")
        {
            year = 2022
        };

        // Creates a links
        meta.links.Add(new OnlineMapsGPXObject.Link("https://www.facebook.com/ICePenguin")
        {
            text = "Penguin Page"
        });

        // Creates a waypoints
        Vector2 mapPos = new Vector2();
#if UNITY_EDITOR
        mapPos = new Vector2(Maps.position.x, Maps.position.y);
#else
        var location = Input.location.lastData;
        mapPos = new Vector2(location.longitude, location.latitude);
#endif
        var track = new OnlineMapsGPXObject.Track();
        trackSegment = new OnlineMapsGPXObject.TrackSegment();
        track.segments.Add(trackSegment);
        gpxObject.tracks.Add(track);

        if (isContinueJourney == false)
        {
            var waypoint = new OnlineMapsGPXObject.Waypoint(mapPos.x, mapPos.y)
            {
                name = $"Start Pos!!",
                time = DateTime.UtcNow,
            };

            trackSegment.points.Add(waypoint);
        }
        cachePoints.Add(mapPos);
    }

    public void UpdateTotalTime(string time_Str)
    {
        totalTime = time_Str;
    }

    public void DeleteAllPathAndMarker()
    {
        if (mOnlineMapsMarkerManager == null)
        {
            mOnlineMapsMarkerManager = Maps.GetComponent<OnlineMapsMarkerManager>();
        }

        mOnlineMapsMarkerManager.items.RemoveAll((i) => { return i.texture.name != "Ellipse 14"; });

        //if (mOnlineMapsMarkerManager.items.Count > 0)
        //{
        //    mOnlineMapsMarkerManager.items.Remove()
        //    mOnlineMapsMarkerManager.items.RemoveRange(1, mOnlineMapsMarkerManager.items.Count - 1);
        //}
        // Remove path draw.
        OnlineMapsDrawingElementManager.RemoveAllItems();

        Maps.Redraw();
    }

    Texture2D GetMapPinIcon(string iconStr)
    {
        return m_MapPinIcon.Find(img => img.name == iconStr);
    }

    UnDestroyData.JourneyData mJourneyData;

    RecordUpdateData RefreshRecordUpdateData()
    {
        mSDB.journeyid = !string.IsNullOrEmpty(mJourneyID) ? mJourneyID : UnDestroyData.userData.email;
        mSDB.email = UnDestroyData.userData.email;
        mSDB.gpx = JsonConvert.SerializeXmlNode(gpxObject.ToXML().element);
        mSDB.recordType = UnDestroyData.journeyData.category;
        mSDB.miles = distance.ToString("00.00");
        mSDB.nowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        mSDB.uploadFiles = string.Join("&", mUploadFile);
        mSDB.userToken = UnDestroyData.token;
        mSDB.totalTime = totalTime;

        return mSDB;
    }

    public class GetJourneyData
    {
        public string journey_id { get; set; }
        public string name { get; set; }
        public string duration { get; set; }
        public string distance { get; set; }
        public int created { get; set; }
        public List<object> resources { get; set; }
        public string category { get; set; }
    }

    public class RecordUpdateData
    {
        public string journeyid;
        public string email;
        public string gpx;
        public string recordType;
        public string totalTime;
        public string miles;
        public string nowTime;
        public string uploadFiles;
        public string userToken;
    }

    public class RecordSQLData
    {
        public string JourneyID { get; set; }
        public string JourneyTitle { get; set; }
        public string Email { get; set; }
        public string GPX { get; set; }
        public string RecordType { get; set; }
        public string Miles { get; set; }
        public string CreateTime { get; set; }
        public string TotalTime { get; set; }
        public string UploadFiles { get; set; }
        public string UserName { get; set; }
        public string UserToken { get; set; }
        public string Status { get; set; }
        public string StatusMsg { get; set; }
        public string FriendShareID { get; set; }
        public string FriendShareStatus { get; set; }
    }

    public class Root
    {
        public List<RecordSQLData> Datas { get; set; }
    }
}
